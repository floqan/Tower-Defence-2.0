using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardTower : AbstractTower
{
    public GameObject gunTurret;
    public GameObject projectile;
    private float time;
    public override Transform Target { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        IsPlacement = true;
        EnemiesInRange = new List<Transform>();
        enemyDetector.SetRadius(buildingData.AttackRadius);
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateEnemies();
        if(Target != null || (Target == null && time < 1/buildingData.AttackSpeed))
        {
            time += Time.deltaTime;
        }

        if(time > 1/buildingData.AttackSpeed && !IsPlacement)
        {
            
            foreach (Transform enemy in EnemiesInRange.ToArray())
            {
                if (!enemy) EnemiesInRange.Remove(enemy);
            }
            Attack();
        }
    }

    public override void Attack()
    {
        if (EnemiesInRange.Count == 0)
        {
            return;
        }
        time = 0;

        if (Target == null)
        {
            UpdateTarget();
        }

        //gunTurret.transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        ProjectileController controller = Instantiate(projectile, gunTurret.transform.position, Quaternion.identity).GetComponent<ProjectileController>();
        controller.ProjectileDamage = buildingData.AttackDamage;
        controller.Target = Target;
        controller.ProjectileSpeed = buildingData.ProjectileSpeed;
    }

    public override void UpdateTarget()
    {
        if (EnemiesInRange.Count > 0)
        {
            Target = EnemiesInRange[0];
        }
        //TODO Add more AttackArts
    }
}
