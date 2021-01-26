using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    public enum State { Idle, PlacingObject, OpenMerchantMenu, OpenBuildingMenu };
    public State GameState { get; set; }

    public static event EventHandler<CoordinateEventArgs> OnNewObjectPlaced;
    public static event EventHandler<CoordinateEventArgs> OnObjectDestroyed;

    public List<GameObject> Towers;
    public List<GameObject> Plants;

    public int level;

    public CameraController cameraController;
    public PlayerController player;
    public UIController ui;
    private GridComponent grid;
    private Inventory inventory;

    private LevelData levelData;
    private int enemyCounter = 0;
    public float MoneyTime = 10; //TODO Change to private
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
        inventory.IncreaseMoney(10);//TODO Change to Variable
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

    internal void PlaceBuilding(GameObject selection)
    {
        if (IsPlacementAllowed(selection)) {
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

        if((selection.GetComponent<AbstractTower>() != null || selection.GetComponent<Cropland>() != null) && field.building == null)
        {
            return true;
        }
        if(selection.GetComponent<AbstractPlant>() != null)
        {
            if(field.building == null || field.building.ObjectType != DataObject.CROPLAND_TYPE)
            {
                return false;
            }
            return field.building.building.GetComponent<Cropland>().plant == null;
        }
        throw new MissingComponentException("No building component found");
    }
}
