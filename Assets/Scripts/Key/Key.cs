using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// key - класс список которых хранитс€ в InputManager, экземпл€р класса отвечает за взаимодействие с конкретной кнопкой через OnPressed Event,
/// позже сюда же надо будет добавить различные параметры и методы св€занные с прочностью и поломкой кнопки
/// </summary>
public class Key
{
    public delegate void KeyHandler();
    public event KeyHandler OnPressed;
    public event KeyHandler OnPressedAnimation;
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
        if (OnPressedAnimation != null)
            OnPressedAnimation.Invoke();
    }
}
