using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy1 : AbstractEnemy
{    
    // Start is called before the first frame update
    void Start()
    {
        lootItems = new Dictionary<int, int>();
        lootItems.Add(31, 3);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //transform.position += new Vector3(0.5f, 0,0) * Time.deltaTime; 
    }

    public override void Move()
    {
        base.Move();
    }

    public override void Attack()
    {
        Debug.Log("Not implemented: Enemy Attack");
    }
}
