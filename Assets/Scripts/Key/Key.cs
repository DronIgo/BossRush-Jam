using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key
{
    public delegate void KeyHandler();
    public event KeyHandler OnPressed;

    public void ClearEvent()
    {
        if (OnPressed == null)
            return;
        foreach (var func in OnPressed.GetInvocationList())
        {
            OnPressed -= (KeyHandler)func;
        }
    }

    public void Invoke()
    {
        if (OnPressed != null)
            OnPressed.Invoke();
    }
}
