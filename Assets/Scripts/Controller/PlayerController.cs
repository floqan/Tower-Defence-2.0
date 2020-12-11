using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    
    public GameObject Cube;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            GameManager.instance.GameState = GameManager.State.PlacingObject;
        }
        else
        {
            GameManager.instance.GameState = GameManager.State.Idle;
        }

        if (GameManager.instance.GameState == GameManager.State.PlacingObject)
        {
            if (!IsMouseOverUI()) { 
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, float.PositiveInfinity, LayerMask.GetMask("Grid"))) {

                    Vector3 position = hit.point;
                    Cube.transform.position = GameManager.instance.GetNearestGridPosition(position);
                }
            }
        }
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
