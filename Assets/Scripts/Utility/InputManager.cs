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
            if (Instance == null) Instance = new InputManager();
            return Instance.InputSystem;
        }
    }
    public static void Activate()
    {
        if (Instance == null) Instance = new InputManager();
        Instance.InputSystem.Input.Enable();
    }

    public static void Deactivate()
    {
        if (Instance == null) Instance = new InputManager();
        Instance.InputSystem.Input.Disable();
    }

    public static float LHInput
    {
        get
        {
            return Input.Input.Horizontal.ReadValue<float>();
        }
    }
    
    public static bool JumpPressed
    {
        get
        {
            return Input.Input.Jump.WasPressedThisFrame();
        }
    }

    public static bool RollPressed
    {
        get
        {
            return Input.Input.Roll.WasPressedThisFrame();
        }
    }

    public static bool AttackPressed
    {
        get
        {
            return Input.Input.Fire.WasPressedThisFrame();
        }
    }
}