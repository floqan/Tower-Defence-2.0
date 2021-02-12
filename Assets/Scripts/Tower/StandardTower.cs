using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardTower : AbstractTower
{
    public GameObject gunTurret;
    public GameObject projectile;
    private float time;
    

    public override Transform target { get; set; }

    public override void Attack()
    {
        if(EnemiesInRange.Count == 0)
        {
            return;
        }
        time = 0;

        if (target == null)
        {
            UpdateTarget();
        }
        
        //gunTurret.transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        ProjectileController controller = Instantiate(projectile, gunTurret.transform.position, Quaternion.identity).GetComponent<ProjectileController>();
        controller.projectileDamage = buildingData.AttackDamage;
        controller.target = target;
        controller.projectileSpeed = buildingData.ProjectileSpeed;
        //throw new System.NotImplementedException();
    }

    public override void UpdateTarget()
    {
        if (EnemiesInRange.Count > 0)
        {
            target = EnemiesInRange[0];
        }
        //TODO Add more AttackArts
    }

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

        //TODO Wenn kein Gegner in Reichweite wird Angriff aufgeladen und ausgelöst, allerdings wird dann die Attacke wieder auf 0 zurück gesetzt
        if(target != null || (target == null && time < 1/buildingData.AttackSpeed))
        {
            time += Time.deltaTime;
        }

        if(time > 1/buildingData.AttackSpeed && !IsPlacement)
        {
            
            foreach (Transform enemy in EnemiesInRange)
            {
                if (!enemy) EnemiesInRange.Remove(enemy);
            }
            Attack();
        }
    }
}
