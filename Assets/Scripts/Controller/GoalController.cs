using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalController : MonoBehaviour
{
    public Image healthBar;
    public Canvas healthCanvas;
    public int hitPoints;
    private int maxHitPoints;
    // Start is called before the first frame update
    void Start()
    {
        maxHitPoints = hitPoints;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = hitPoints / maxHitPoints;
        healthCanvas.transform.LookAt(2 * transform.position - Camera.main.transform.position);
    }

    public void OnDamage(int damage)
    {
        hitPoints -= damage;
        if(hitPoints <= 0)
        {
            GameManager.instance.GoalDestroyed();
        }
    }
}
