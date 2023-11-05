using System.Collections;
using UnityEngine;

public class InputManager
{
    public static InputManager Instance;
    public InputSystem InputSystem;

    public InputManager()
    {
        InputSystem = new InputSystem();
        InputSystem.Enable();
    }

    public static InputSystem Input
    {
        get
        {
            if(Instance == null) Instance = new InputManager();
            return Instance.InputSystem;
        }
    }
    public static void Activate()
    {
        if (Instance == null) Instance = new InputManager();
        Instance.InputSystem.Enable();
    }

    public static void Deactivate()
    {
        if (Instance == null) Instance = new InputManager();
        Instance.InputSystem.Disable();
    }
}