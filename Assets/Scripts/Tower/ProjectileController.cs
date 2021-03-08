using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float ProjectileSpeed { get; set; }
    public Transform Target { get; set; }

    public int ProjectileDamage { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        transform.tag = "Projectile";
    }

    // Update is called once per frame
    void Update()
    {
        if (Target == null)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.position, ProjectileSpeed * Time.deltaTime);
            transform.LookAt(Target);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Enemy"))
        {
            if(Target.gameObject == other.gameObject)
            {
                other.GetComponent<AbstractEnemy>().OnDamage(ProjectileDamage);
                Destroy(this.gameObject);
            }
        }
    }
}
