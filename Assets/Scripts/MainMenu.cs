using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{

    public GameObject initSelection;
    private EventSystem eventSystem;


    void Awake()
    {
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    void OnEnable()
    {
        if(Gamepad.current != null)
        {
            eventSystem.SetSelectedGameObject(initSelection);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("levelOne");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
