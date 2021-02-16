using UnityEngine;
using UnityEngine.InputSystem;

public class DebugBox : MonoBehaviour
{

    void OnGUI()
    {
        GUIStyle style = new GUIStyle("Box");
        style.alignment = TextAnchor.UpperLeft;
       
        string ControllerData = "";
        string KeyboardData = "";
        var gamepad = Gamepad.current;
        var keyboard = Keyboard.current;
        var mouse = Mouse.current;
        Vector2 wasd = new Vector2(keyboard.dKey.ReadValue() - keyboard.aKey.ReadValue(), keyboard.wKey.ReadValue() - keyboard.sKey.ReadValue());
        wasd.Normalize();
        Vector2 arrowKey = new Vector2(keyboard.rightArrowKey.ReadValue() - keyboard.leftArrowKey.ReadValue(), keyboard.upArrowKey.ReadValue() - keyboard.downArrowKey.ReadValue());
        arrowKey.Normalize();

        KeyboardData += $"WASD: {wasd}\n";
        KeyboardData += $"Arrows: {arrowKey}\n";
        KeyboardData += $"Spacebar: {keyboard.spaceKey.ReadValue()}\n";
        KeyboardData += $"Mouse: {mouse.position.ReadValue()}\n";
        KeyboardData += $"LMB: {mouse.leftButton.ReadValue()}\n";
        KeyboardData += $"MMB: {mouse.middleButton.ReadValue()}\n";
        KeyboardData += $"RMB: {mouse.rightButton.ReadValue()}\n";

        GUI.Box(new Rect(10, 10, 150, 110), KeyboardData, style);

        if (gamepad != null)
        {
            ControllerData += $"LStick: {gamepad.leftStick.ReadValue()}\n";
            ControllerData += $"LStickClick: {gamepad.leftStickButton.ReadValue()}\n";
            ControllerData += $"RStick: {gamepad.rightStick.ReadValue()}\n";
            ControllerData += $"RStickClick: {gamepad.rightStickButton.ReadValue()}\n";
            ControllerData += $"A: {gamepad.aButton.ReadValue()}\n";
            ControllerData += $"B: {gamepad.bButton.ReadValue()}\n";
            ControllerData += $"X: {gamepad.xButton.ReadValue()}\n";
            ControllerData += $"Y: {gamepad.yButton.ReadValue()}\n";
            ControllerData += $"DPad: {gamepad.dpad.ReadValue()}\n";
            ControllerData += $"LB: {gamepad.leftShoulder.ReadValue()}\n";
            ControllerData += $"RB: {gamepad.rightShoulder.ReadValue()}\n";
            ControllerData += $"LT: {gamepad.leftTrigger.ReadValue()}\n";
            ControllerData += $"RT: {gamepad.rightTrigger.ReadValue()}\n";
            ControllerData += $"Start: {gamepad.startButton.ReadValue()}\n";
            ControllerData += $"Select: {gamepad.selectButton.ReadValue()}";

            GUI.Box(new Rect(10, 130, 150, 230), ControllerData, style);

        }

    }

}
