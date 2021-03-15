using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public abstract class AbstractEnemy : MonoBehaviour
{
    Field nextGoal;
    Field lastGoal;
    bool AttackMode = false;
    public GridComponent grid { get; set; }
    public List<FieldGridCoordinate> Path { get; set; }
    public float MovementSpeed;
    public float AttackSpeed;
    private float currentTime;
    public int AttackDamage;
    public int HitPoints;
    //Loot
    public int lootCoins;
    // Key itemId Value amount of dropped loot
    protected Dictionary<int, int> lootItems;

    private void Awake()
    {
        currentTime = AttackSpeed / 2;
    }

    public void RecalculatePathAfterPlacement(object sender, CoordinateEventArgs args)
    {
        if (Path.Any(field => field.Equals(args.changedCoordinate)))
        {
            Path = GameManager.instance.RecalculatePath(nextGoal.GridCoordinate);
        }
    }

    public void RecalculatePathAfterDestroy(object sender, CoordinateEventArgs args)
    {
        if(!Path.Any(field => field.Equals(args.changedCoordinate)))
        {
            Path = GameManager.instance.RecalculatePath(nextGoal.GridCoordinate);
        }
    }

    // Update is called once per frame
    public virtual void Move()
    {
        if (IsGoalReached())
        {
            AttackMode = true;
        }
        if (!AttackMode)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextGoal.GetMiddlePoint(), MovementSpeed * Time.deltaTime);
            transform.LookAt(nextGoal.GetMiddlePoint());
            if (IsNextGoalReached())
            {
                SetNextGoal();
            }
        }
        else
        {
            currentTime += Time.deltaTime;
            if (currentTime >= AttackSpeed)
            {
                currentTime = 0;
                Attack();
                if(nextGoal.building == null)
                {
                    AttackMode = false;
                    currentTime = 0;
                }
            }
        }
    }

    public void SetNextGoal()
    {
        // Set LastGoal to reached Goal
        if (lastGoal != null) { 
            lastGoal.UnlockField();
        }
        lastGoal = nextGoal;
        // Set new nextGoal
        nextGoal = grid.Grid[Path[0].X, Path[0].Z];
        nextGoal.LockField();
        Path.RemoveAt(0);
    }
    bool IsNextGoalReached()
    {
        return Vector3.Distance(transform.position, nextGoal.GetMiddlePoint()) < 0.001f;
    }
    bool IsGoalReached()
    {
        return Path.Count == 0;
    }
    public abstract void Attack();
    public void DestoryEnemy()
    {
        nextGoal.UnlockField();
        Destroy(gameObject);
    }

    public void OnDamage(int damage)
    {
        HitPoints -= damage;
        //TODO Add Damage Type
        //TODO Add Damage Reduction
        if(HitPoints <= 0)
        {
            DropLoot();
            GameManager.instance.DestoryEnemy(this.gameObject);
        }
    }

    public void DropLoot()
    {
        //Drop Coins
        for(int i = 0; i < lootCoins; i++)
        {
            GameObject coin = Instantiate(Resources.Load<GameObject>("Prefab/Items/Coin"),transform.position,Quaternion.identity);
            coin.GetComponent<ItemController>().ItemId = -1;
            coin.GetComponent<Rigidbody>().AddForce(GetRandomDirection(100), ForceMode.Impulse);
        }

        foreach (int itemId in lootItems.Keys)
        {
            for (int i = 0; i < lootItems[itemId]; i++) {
                Item item = Inventory.instance.GetItemByItemId(itemId);
                if (Random.Range(0, 101) < item.dropChance)
                {
                    GameObject itemObject = Instantiate<GameObject>(item.itemModel, transform.position, Quaternion.identity);
                    itemObject.GetComponent<ItemController>().ItemId = itemId;
                    itemObject.GetComponent<Rigidbody>().AddForce(GetRandomDirection(100), ForceMode.Impulse);
                } 
            }
        }
    }

    private Vector3 GetRandomDirection(int force)
    {
        float directionX = Random.Range(-100f, 100f);
        float directionZ = Random.Range(-100f, 100f);
        return new Vector3(directionX, force, directionZ);
    }
}
