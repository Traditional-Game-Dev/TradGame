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

    void Awake()
    {
        var planActionMap = playerControls.FindActionMap("Planning");

        place = planActionMap.FindAction("Place");
        place.performed += ctx => 
        {
            Instantiate(prefab, transform.position, transform.rotation);
        };

        place.Enable();
    }

    void Start()
    {
        place.Enable();

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
        
        if(Mouse.current.scroll.ReadValue().y < 0)
        {
            transform.Rotate(Vector3.up * 10f, Space.Self);
        }

        if(Mouse.current.scroll.ReadValue().y > 0)
        {
            transform.Rotate(Vector3.down * 10f, Space.Self);
        }
    }

    void OnDestroy()
    {
        place.Disable();
    }
}
