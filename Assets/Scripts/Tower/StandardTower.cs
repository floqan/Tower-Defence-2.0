using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardTower : AbstractTower
{
    public GameObject gunTurret;
    public GameObject projectile;
    private List<Transform> EnemiesInRange;
    private float time;

    public override Transform target { get; set; }

    public override void Attack()
    {
        if(EnemiesInRange.Count == 0)
        {
            return;
        }

        if(target == null)
        {
            UpdateTarget();
        }
        Instantiate(projectile, gunTurret.transform.position, Quaternion.identity);
        throw new System.NotImplementedException();
    }

    public override void UpdateTarget()
    {
        if (EnemiesInRange.Count > 0)
        {
            target = EnemiesInRange[0];
        }
    }

    public override void Upgrade()
    {
        throw new System.NotImplementedException();
    }


    // Start is called before the first frame update
    void Start()
    {
        EnemiesInRange = new List<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > buildingData.AttackSpeed)
        {
            time = 0;
            Attack();
        }
        if(target != null)
        {
            gunTurret.transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            Debug.Log("One more Enemy");
            EnemiesInRange.Add(collision.transform);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            Debug.Log("One Enemy less");
            EnemiesInRange.Remove(collision.transform);
        }
    }

}
