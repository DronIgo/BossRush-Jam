using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// key - ����� ������ ������� �������� � InputManager, ��������� ������ �������� �� �������������� � ���������� ������� ����� OnPressed Event,
/// ����� ���� �� ���� ����� �������� ��������� ��������� � ������ ��������� � ���������� � �������� ������
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
