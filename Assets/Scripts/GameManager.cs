using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
    public enum State { Idle, PlacingObject };
    public State GameState { get; set; }
    public GridComponent grid;

    private LevelData levelData;
    private int enemyCounter = 0;

    private float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameState = State.Idle;
        grid = GameObject.Find("Grid").GetComponent<GridComponent>();
        OnNewObjectPlaced += grid.RecalculatePathAfterPlacement;
        OnObjectDestroyed += grid.RecalculatePathAfterDestroy;
        levelData = LevelUtility.GetLevelData(level);
        LevelUtility.LoadLevel(grid, levelData);
        grid.CalculatePath();
        SpawnEnemy();
    }

    internal Vector3 GetNearestGridPosition(Vector3 mousePosition) 
    { 
        return grid.GetNearestGridPosition(mousePosition);
    }
   

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time < 500)
        {
            time = 0;
            SpawnEnemy();
        }
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
}
