using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlueprintScript : MonoBehaviour
{
    RaycastHit hit;
    Vector3 createLocation;
    public GameObject prefab;
    public InputActionAsset playerControls;
    private InputAction place;
    private InputAction rotate;

    private InputAction deselect;
    private GameObject planningUI;
    private Dictionary<GameObject, int> cost;

    void Awake()
    {
        var planActionMap = playerControls.FindActionMap("Planning");
        planningUI = GameObject.Find("PlanningUI");
        
        cost = planningUI.GetComponent<spawnObject>().getCosts();

        place = planActionMap.FindAction("Place");
        place.Enable();

        rotate = planActionMap.FindAction("Rotation");
        rotate.Enable();

        deselect = planActionMap.FindAction("Deselect");
        deselect.Enable();
    }

    void Start()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if(Physics.Raycast(ray, out hit, 50000.0f, (1 << 8)))
        {
            transform.position = hit.point;
        }
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if(Physics.Raycast(ray, out hit, 50000.0f))
        {   
            Vector3 tempLocation = hit.point;
            tempLocation.y += GetComponent<MeshFilter>().mesh.bounds.extents.y*transform.localScale.y;
            transform.position = tempLocation;
        }

        if(deselect.triggered)
        {
            gameObject.SetActive(false);
        }

        if(rotate.triggered)
        {
            if(rotate.ReadValue<float>() > 0)
            {
                transform.Rotate(Vector3.down * 10f, Space.Self);
            }
            else if(rotate.ReadValue<float>() < 0)
            {
              transform.Rotate(Vector3.up * 10f, Space.Self);
            }
        }

        int cashAfterPurchase = planningUI.GetComponent<spawnObject>().mana - cost[gameObject];

        if(place.triggered && cashAfterPurchase >= 0)
        {
            if(!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                Instantiate(prefab, transform.position, transform.rotation);
                planningUI.GetComponent<spawnObject>().mana = cashAfterPurchase;
            }
        }
    }
}
