using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        currentTime = AttackSpeed / 2;
    }

    public void RecalculatePathAfterPlacement(object sender, CoordinateEventArgs args)
    {
        if (Path.Any(field => field.Equals(args.changedCoordinate)))
        {
            Path = GameManager.instance.RecalculatePath(nextGoal.gridCoordinate);
        }
    }

    public void RecalculatePathAfterDestroy(object sender, CoordinateEventArgs args)
    {
        if(!Path.Any(field => field.Equals(args.changedCoordinate)))
        {
            Path = GameManager.instance.RecalculatePath(nextGoal.gridCoordinate);
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
        nextGoal = grid.grid[Path[0].X, Path[0].Z];
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
        //TODO Drop Loot
    }
}
