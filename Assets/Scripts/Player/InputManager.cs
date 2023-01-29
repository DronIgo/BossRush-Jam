using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public List<KeyCode> keyCodes = new List<KeyCode>() { 
        KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y, KeyCode.U, KeyCode.I, KeyCode.O, KeyCode.P,
        KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.J, KeyCode.K, KeyCode.L,
        KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V, KeyCode.B, KeyCode.N, KeyCode.M, KeyCode.Comma
    };

    public Dictionary<KeyCode, Key> keysByCodes = new Dictionary<KeyCode, Key>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this; 
    }

    private void Update()
    {
        foreach (var key in keyCodes)
        {
            if (Input.GetKey(key))
            {
                if (keysByCodes.ContainsKey(key))
                {
                    keysByCodes[key].Invoke();
                }
            }
        }
    }

    public void SubscribeToButton(Key.KeyHandler func, KeyCode key)
    {
        if (keysByCodes.ContainsKey(key))
        {
            keysByCodes[key].OnPressed += func;
        } else
        {
            keysByCodes.Add(key, new Key());
            keysByCodes[key].OnPressed += func;
        }
    }

    public void UnsubscribeFromButton(Key.KeyHandler func, KeyCode key)
    {
        if (keysByCodes.ContainsKey(key))
        {
            keysByCodes[key].OnPressed -= func;
        }
    }

    public void ClearButton(KeyCode key)
    {
        if (keysByCodes.ContainsKey(key))
        {
            keysByCodes[key].ClearEvent();
        }
    }
}
