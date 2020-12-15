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

    public static event EventHandler<CoordinateEventArgs> OnNewObjectPlaced;
    public static event EventHandler<CoordinateEventArgs> OnObjectDestroyed;

    public int level;
    public CameraController cameraController;
    public PlayerController player;
    public UIController ui;

    public enum State { Idle, PlacingObject, OpenMerchantMenu, OpenBuildingMenu};
    public State GameState { get; set; }
    public GridComponent grid;
    public Inventory inventory;

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
            inventory.IncreaseMoney(10);//TODO Change to Variable
        }
    }

    internal int GetTowerCount()
    {
        return levelData.Towers.Count;
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
        return levelData.Plants.Count;
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
        //TODO 
    }
    private void DestoryEnemy(GameObject enemy)
    {
        OnObjectDestroyed -= enemy.GetComponent<AbstractEnemy>().RecalculatePathAfterDestroy;
        OnNewObjectPlaced -= enemy.GetComponent<AbstractEnemy>().RecalculatePathAfterPlacement;
        enemy.GetComponent<AbstractEnemy>().DestoryEnemy();
    }

    public List<FieldGridCoordinate> RecalculatePath(FieldGridCoordinate startField)
    {
        return grid.CalculatePath(startField);
    }

    public void CreateBuilding(Building<BuildingData> building)
    {
        GameObject buildingObject = Instantiate(building.gameObject);
        building.transform.position = Vector3.zero;

    }
}
