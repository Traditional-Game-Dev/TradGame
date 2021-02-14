using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerInput : MonoBehaviour
{

    void OnGUI()
    {
        GUIStyle style = new GUIStyle("Box");
        style.alignment = TextAnchor.UpperLeft;

        string ControllerData = "";
        var gamepad = Gamepad.current;

        if(gamepad == null)
        {
            return;
        }

        ControllerData += "LStick:" + gamepad.leftStick.ReadValue() + "\n";
        ControllerData += "LStickClick:" + gamepad.leftStickButton.ReadValue() + "\n";
        ControllerData += "RStick: " + gamepad.rightStick.ReadValue() + "\n";
        ControllerData += "RStickClick:" + gamepad.rightStickButton.ReadValue() + "\n";
        ControllerData += "A: " + gamepad.aButton.ReadValue() + "\n";
        ControllerData += "B: " + gamepad.bButton.ReadValue() + "\n";
        ControllerData += "X: " + gamepad.xButton.ReadValue() + "\n";
        ControllerData += "Y: " + gamepad.yButton.ReadValue() + "\n";
        ControllerData += "DPad: " + gamepad.dpad.ReadValue() + "\n";
        ControllerData += "LB: " + gamepad.leftShoulder.ReadValue() + "\n";
        ControllerData += "RB: " + gamepad.rightShoulder.ReadValue() + "\n";
        ControllerData += "LT: " + gamepad.leftTrigger.ReadValue() + "\n";
        ControllerData += "RT: " + gamepad.rightTrigger.ReadValue() + "\n";
        ControllerData += "Start: " + gamepad.startButton.ReadValue() + "\n";
        ControllerData += "Select: " + gamepad.selectButton.ReadValue();

        GUI.Box(new Rect(10, 10, 150, 230), ControllerData, style);       
    }

}
