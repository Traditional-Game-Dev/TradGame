using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlueprintScript : MonoBehaviour
{
    RaycastHit hit;
    public GameObject prefab;
    public InputActionAsset playerControls;
    public Material blueprintMat;
    public Material blueprintError;
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

    void OnDisable()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        Vector3 tempLocation;


        //Place At Ground Controller
        if(Gamepad.current != null)
        {
            tempLocation = new Vector3 ((Gamepad.current.leftStick.x.ReadValue() * Time.deltaTime * 30) + transform.position.x, -0.25f, (Gamepad.current.leftStick.y.ReadValue() * Time.deltaTime * 30) + transform.position.z);
            tempLocation.y += GetComponent<MeshFilter>().mesh.bounds.extents.y*transform.localScale.y - 0.25f;
            transform.position = tempLocation;
        }
        //Place At Ground Mouse
        else if(Physics.Raycast(ray, out hit, 50000.0f))
        {   
            tempLocation = hit.point;
            tempLocation.y += GetComponent<MeshFilter>().mesh.bounds.extents.y*transform.localScale.y -0.25f;
            transform.position = tempLocation;
        }


        //Object Recoloration
        if(Vector3.Distance(transform.position, GameObject.Find("Boss").transform.position) < 25)
        {
            if(gameObject.GetComponent<MeshRenderer>().material.color != blueprintError.color)
            {
                gameObject.GetComponent<MeshRenderer>().material = blueprintError;
            }
        }
        else if(gameObject.GetComponent<MeshRenderer>().material.color != blueprintMat.color)
        {
            gameObject.GetComponent<MeshRenderer>().material = blueprintMat;
        }

        if(gameObject.name == "TeleporterBlueprint" && GameObject.FindGameObjectsWithTag("Teleporter").Length >= 2)
        {
            if(gameObject.GetComponent<MeshRenderer>().material.color != blueprintError.color)
            {
                gameObject.GetComponent<MeshRenderer>().material = blueprintError;
            }
        }

        //Object Deselection
        if(deselect.triggered)
        {
            gameObject.SetActive(false);
        }

        //Object Rotation
        if(rotate.triggered)
        {
            if(rotate.ReadValue<float>() > 0)
            {
                transform.Rotate(Vector3.down * 10f, Space.World);
            }
            else if(rotate.ReadValue<float>() < 0)
            {
              transform.Rotate(Vector3.up * 10f, Space.World);
            }
        }

        int cashAfterPurchase = planningUI.GetComponent<spawnObject>().mana - cost[gameObject];

        if(place.triggered && cashAfterPurchase >= 0 && gameObject.GetComponent<MeshRenderer>().material.color != blueprintError.color)
        {
            if(!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                if(gameObject.name == "reflectorBlueprint")
                {
                    Instantiate(prefab, transform.position, transform.rotation * Quaternion.Euler(90, 0, 0) );
                    planningUI.GetComponent<spawnObject>().mana = cashAfterPurchase;
                }
                else
                {
                    Instantiate(prefab, transform.position, transform.rotation);
                    planningUI.GetComponent<spawnObject>().mana = cashAfterPurchase;
                }
            }
        }
    }
}
