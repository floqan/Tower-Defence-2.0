using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float projectileSpeed { get; set; }
    public Transform target { get; set; }

    public int projectileDamage { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        transform.tag = "Projectile";
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, projectileSpeed * Time.deltaTime);
            transform.LookAt(target);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Enemy")
        {
            if(target.gameObject == other.gameObject)
            {
                other.GetComponent<AbstractEnemy>().OnDamage(projectileDamage);
                Destroy(this.gameObject);
            }
        }
    }
}
