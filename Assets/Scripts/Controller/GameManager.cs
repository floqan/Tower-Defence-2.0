using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class GameManager : MonoBehaviour
{
    #region Singelton 
    public static GameManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Achtung, es wurden mehrere GameManager erstellt");
        }
    }

    #endregion

    public const int PLANT_TYPE = 1;
    public const int TOWER_TYPE = 2;
    public State GameState { get; set; }

    public enum State { Idle, PlacingObject, OpenMerchantMenu, OpenBuildingMenu };

    public static event EventHandler<CoordinateEventArgs> OnNewObjectPlaced;
    public static event EventHandler<CoordinateEventArgs> OnObjectDestroyed;

    public int level;

    public List<GameObject> Towers;
    public List<GameObject> Plants;

    public CameraController cameraController;
    public PlayerController player;
    public UIController ui;

    private GridComponent grid;
    private Inventory inventory;

    private LevelData levelData;
    private int enemyCounter = 0;
    public float MoneyTime = 2; //TODO Change to private
    private float moneyTimer;
    private float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameState = State.Idle;
        grid = GameObject.Find("Grid").GetComponent<GridComponent>();
        inventory = Inventory.instance;
        OnNewObjectPlaced += grid.RecalculatePathAfterPlacement;
        OnObjectDestroyed += grid.RecalculatePathAfterDestroy;
        levelData = LevelUtility.GetLevelData(level);
        LevelUtility.LoadLevel(grid, levelData);
        grid.CalculatePath();
        SpawnEnemy();
        CheckUniqueBuildingIds();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        moneyTimer += Time.deltaTime;
        if (time < 500)
        {
            time = 0;
            SpawnEnemy();
        }

        if (moneyTimer > MoneyTime)
        {
            GiveSalary();
        }
    }

    private void CheckUniqueBuildingIds()
    {
        List<int> plantIds = new List<int>();
        List<int> towerIds = new List<int>();
        
        foreach(GameObject plant in Plants)
        {
            plantIds.Add(plant.GetComponent<AbstractPlant>().buildingData.ObjectId);
        }
        foreach(GameObject tower in Towers)
        {
            towerIds.Add(tower.GetComponent<AbstractTower>().buildingData.ObjectId);
        }

        var doubleTowers = towerIds.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
        if(doubleTowers.Count > 0)
        {
            throw new BuildingException("Tower with same ID found " + doubleTowers[0]);
        }

        var doublePlants = plantIds.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
        if (doublePlants.Count > 0)
        {
            throw new BuildingException("Plant with same ID found " + doublePlants[0]);
        }

        for (int i = 0; i < plantIds.Count; i++)
        {
            for(int j = i; j < towerIds.Count; j++)
            {
                if(plantIds[i] == towerIds[j])
                    throw new BuildingException("Tower and Plant with same ID found " + plantIds[i]);
            }
        }
    }

    internal int GetTowerCount()
    {
        return Towers.Count;
    }

    internal Vector3 GetNearestGridPosition(Vector3 mousePosition) 
    { 
        return grid.GetNearestGridPosition(mousePosition);
    }

    internal void OpenTowerMenu()
    {
        GameState = State.OpenBuildingMenu; 
        ui.OpenTowerMenu();
    }

    internal int GetPlantCount()
    {
        return Plants.Count;
    }

    internal void CloseAll()
    {
        GameState = State.Idle;
        ui.ClosePlantMenu();
        ui.CloseTowerMenu();
    }

    internal void CloseTowerMenu()
    {
        GameState = State.Idle;
        ui.CloseTowerMenu();
    }
    internal void OpenPlantMenu()
    {
        GameState = State.OpenBuildingMenu;
        ui.OpenPlantMenu();
    }
    internal void ClosePlantMenu()
    {
        GameState = State.Idle;
        ui.ClosePlantMenu();
    }

    internal void OpenMerchantMenu()
    {
        GameState = State.OpenMerchantMenu;
        ui.OpenMerchantMenu();
    }

    internal void CloseMerchantMenu()
    {
        GameState = State.Idle;
        ui.CloseMerchantMenu();
    }

    private void SpawnEnemy()
    {
        if (enemyCounter < levelData.GetNumberOfEnemies()) {
            GameObject enemyGameObject = EnemyUtility.GetEnemyByID(levelData.Enemies[enemyCounter].Key);
            enemyGameObject.tag = "Enemy";
            AbstractEnemy enemy = enemyGameObject.GetComponent<AbstractEnemy>();
            OnNewObjectPlaced += enemy.RecalculatePathAfterPlacement;
            OnObjectDestroyed += enemy.RecalculatePathAfterDestroy;
            int spawnId = levelData.Enemies[enemyCounter].Value;
            enemy.transform.position = grid.Spawns[spawnId].Key.GetMiddlePoint();
            enemy.Path = grid.Spawns[spawnId].Value;
            enemy.grid = grid;
            enemy.SetNextGoal();
            enemyCounter++;
        }
    }

    private void GiveSalary()
    {
        inventory.IncreaseMoney(Inventory.Salary);//TODO Change to Variable
        moneyTimer = 0;
    }
    public void DestoryEnemy(GameObject enemy)
    {
        OnObjectDestroyed -= enemy.GetComponent<AbstractEnemy>().RecalculatePathAfterDestroy;
        OnNewObjectPlaced -= enemy.GetComponent<AbstractEnemy>().RecalculatePathAfterPlacement;
        enemy.GetComponent<AbstractEnemy>().DestoryEnemy();
    }

    public List<FieldGridCoordinate> RecalculatePath(FieldGridCoordinate startField)
    {
        return grid.CalculatePath(startField);
    }

    public TowerData GetTowerById(int id)
    {
        foreach (GameObject data in Towers)
        {
            if(data.GetComponent<AbstractTower>().buildingData.ObjectId == id)
            {
                return data.GetComponent<AbstractTower>().buildingData;
            }
        }
        return null;
    }

    public PlantData GetPlantById(int id)
    {
        foreach (GameObject data in Plants)
        {
            if (data.GetComponent<AbstractPlant>().buildingData.ObjectId == id)
            {
                return data.GetComponent<AbstractPlant>().buildingData;
            }
        }
        return null;
    }

    //buildingType: 1 = Plant / 2 = Tower
    public void CreateBuilding(int buildingId, int buildingType)
    {
        if (GameState != State.Idle)
        {
            return;
        }
        switch (buildingType)
        {
            case PLANT_TYPE:
                foreach(GameObject plant in Plants)
                {
                    if(plant.GetComponent<AbstractPlant>().buildingData.ObjectId == buildingId)
                    {
                        player.Selection = Instantiate(plant);
                        player.Selection.GetComponent<AbstractPlant>().buildingData = Instantiate(plant.GetComponent<AbstractPlant>().buildingData);
                        GameState = State.PlacingObject;
                        return;
                    }
                }
                Debug.LogError("Building can not be instantiated. No matching building was found for id" + buildingId);
                break;
            case TOWER_TYPE:
                foreach(GameObject tower in Towers)
                {
                    if(tower.GetComponent<AbstractTower>().buildingData.ObjectId == buildingId)
                    {
                        player.Selection = Instantiate(tower);
                        player.Selection.GetComponent<AbstractTower>().buildingData = Instantiate(tower.GetComponent<AbstractTower>().buildingData);
                        GameState = State.PlacingObject;
                        return;
                    }
                }
                Debug.LogError("Building can not be instantiated. No matching building was found for id" + buildingId);
                break;
            default:
                Debug.LogError("Building can not be instantiated. No matching type was found for type " + buildingType);
                break;
        }        
    }

    internal int GetBuildingID(GameObject building)
    {
        if(building.GetComponent<AbstractPlant>() != null)
        {
            return building.GetComponent<AbstractPlant>().buildingData.ObjectId;
        }
        if(building.GetComponent<AbstractTower>() != null)
        {
            return building.GetComponent<AbstractTower>().buildingData.ObjectId;
        }
        throw new BuildingException("Given GameObject is not a building");
    }

    internal BuildingData GetBuildingData(int buildingId)
    {
        foreach (GameObject plant in Plants)
        {
            if(plant.GetComponent<AbstractPlant>().buildingData.ObjectId == buildingId)
            {
                return plant.GetComponent<AbstractPlant>().buildingData;
            }
        }

        foreach (GameObject tower in Towers)
        {
            if(tower.GetComponent<AbstractTower>().buildingData.ObjectId == buildingId)
            {
                return tower.GetComponent<AbstractTower>().buildingData;
            }
        }
        throw new BuildingException("No Building found with ID " + buildingId);
    }

    internal void PlaceBuilding(GameObject selection)
    {
        if (IsPlacementAllowed(selection)) {
            // TODO Reduce stored Ressource
            BuildingData data = GetBuildingData(GetBuildingID(selection));
            inventory.DecreaseMoney(data.moneyCost);
            foreach(BuildingData.Resource resource in data.ResourcesCost)
            {
                inventory.DecreaseResource(resource.itemId, resource.itemAmount);
            }
            selection.GetComponent<IBuilding>().IsPlacement = false;
            CoordinateEventArgs args = new CoordinateEventArgs
            {
                changedCoordinate = grid.GetNearestField(selection.transform.position).PlaceBuilding(selection)
            };
            player.Selection = null;
            GameState = State.Idle;
            OnNewObjectPlaced(this, args);     
        }
    }

    internal bool IsPlacementAllowed(GameObject selection)
    {
        Field field = grid.GetNearestField(selection.transform.position);
        if(field.IsEnvironment || field.IsGoal || field.IsLocked())
        {
            return false;
        }

        foreach(KeyValuePair<Field, List<FieldGridCoordinate>> spawn in grid.Spawns)
        {
            if (spawn.Key.Equals(field))
            {
                return false;
            }
        }

        if(selection.GetComponent<AbstractTower>() != null || selection.GetComponent<Cropland>() != null)
        {
            return field.building == null;
        }
        if(selection.GetComponent<AbstractPlant>() != null)
        {
            if(field.building == null || field.building.ObjectType != DataObject.CROPLAND_TYPE)
            {
                return false;
            }
            return field.building.building.GetComponent<Cropland>().Plant == null;
        }
        throw new MissingComponentException("No building component found");
    }

    internal bool CheckResources(int buildingId)
    {
        BuildingData data = GetBuildingData(buildingId);
        if (!inventory.CheckMoney(data.moneyCost)) return false;
        foreach(BuildingData.Resource resource in data.ResourcesCost)
        {
            if (!inventory.CheckResource(resource.itemId, resource.itemAmount)) return false;
        }
        return true;
    }
}