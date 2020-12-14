using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    
    public GameObject Cube;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            gameManager.GameState = GameManager.State.PlacingObject;
        }
        else
        {
            gameManager.GameState = GameManager.State.Idle;
        }

        if (gameManager.GameState == GameManager.State.PlacingObject)
        {
            if (!IsMouseOverUI()) { 
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, float.PositiveInfinity, LayerMask.GetMask("Grid"))) {

                    Vector3 position = hit.point;
                    Cube.transform.position = gameManager.GetNearestGridPosition(position);
                }
            }
        }
        if(gameManager.GameState == GameManager.State.Idle || gameManager.GameState == GameManager.State.OpenBuildingMenu)
        {
            if (!IsMouseOverUI() && Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit))
                {
                    switch (hit.transform.tag)
                    {
                        case "Tower":
                            gameManager.CloseTowerMenu();
                            gameManager.OpenTowerMenu();
                            break;
                        case "Plant":
                            gameManager.ClosePlantMenu();
                            gameManager.OpenPlantMenu();
                            break;
                        case "Merchant":
                            gameManager.OpenMerchantMenu();
                            break;
                    }
                }
            }
        }
        if(gameManager.GameState == GameManager.State.OpenMerchantMenu)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                gameManager.CloseMerchantMenu();
            }
        }
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
