using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;


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
            GameObject.Find("BookToggle").GetComponent<Animator>().Play("Normal");
            EventSystem.current.SetSelectedGameObject(null);
            if(Gamepad.current != null)
            {
                EventSystem.current.SetSelectedGameObject(prevSelected);
            }
        }
    }

    void OnEnable()
    {
        prevSelected = initialSelection;
        EventSystem.current.SetSelectedGameObject(prevSelected);
        // if(Gamepad.current != null)
        // {
        //     GameObject.Find("FakeButton").SetActive(false);
        //     GameObject.Find("BookToggle").GetComponent<Image>().enabled = false;
        // }
        // else
        // {
        //     GameObject.Find("FakeButton").SetActive(true);
        //     GameObject.Find("BookToggle").GetComponent<Image>().enabled = true;
        // }
    }

    public void deselectButton()
    {
        prevSelected = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.SetSelectedGameObject(null);
        GameObject.Find("BookToggle").GetComponent<Animator>().Play("Move to Bot");
    }

    public void CameraStart()
    {
        Camera.main.GetComponent<CameraController>().swapMode();
    }
}
