using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;


public class PlanningMenuScript : MonoBehaviour
{
    public InputActionAsset playerControls;
    public GameObject initialSelection;
    private InputAction deselect;
    private GameObject prevSelected;

    void Start()
    {

        var planActionMap = playerControls.FindActionMap("Planning");        
        deselect = planActionMap.FindAction("Deselect");
        deselect.Enable();
        prevSelected = initialSelection;
    }

    // Update is called once per frame
    void Update()
    {
        if(deselect.triggered)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(prevSelected);
        }
    }

    void OnEnable()
    {
        prevSelected = initialSelection;
        EventSystem.current.SetSelectedGameObject(prevSelected);
    }

    public void deselectButton()
    {
        prevSelected = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.SetSelectedGameObject(null);
    }
}
