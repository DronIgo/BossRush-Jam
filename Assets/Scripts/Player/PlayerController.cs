using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public enum State
    {
        Move,
        Roll,
        Attack
    }

    public State CurrentState;

    public Dictionary<ButtonUI.Button, KeyCode> settings;

    public float speed = 3.0f;
    private void MoveUp()
    {
        transform.position += new Vector3(0.0f, 1.0f, 0.0f) * Time.deltaTime * speed;
    }
    private void MoveDown()
    {
        transform.position += new Vector3(0.0f, -1.0f, 0.0f) * Time.deltaTime * speed;
    }
    private void MoveRight()
    {
        transform.position += new Vector3(1.0f, 0.0f, 0.0f) * Time.deltaTime * speed;
    }
    private void MoveLeft()
    {
        transform.position += new Vector3(-1.0f, 0.0f, 0.0f) * Time.deltaTime * speed;
    }

    private void Awake()
    {
        Instance = this;
        InputManager.Instance.SubscribeToButton(MoveUp, KeyCode.W);
        InputManager.Instance.SubscribeToButton(MoveDown, KeyCode.S);
        InputManager.Instance.SubscribeToButton(MoveLeft, KeyCode.A);
        InputManager.Instance.SubscribeToButton(MoveRight, KeyCode.D);

        settings = new Dictionary<ButtonUI.Button, KeyCode>();

        settings.Add(ButtonUI.Button.MoveUp, KeyCode.W);
        settings.Add(ButtonUI.Button.MoveDown, KeyCode.S);
        settings.Add(ButtonUI.Button.MoveLeft, KeyCode.A);
        settings.Add(ButtonUI.Button.MoveRight, KeyCode.D);
    }

    public void SetAction(ButtonUI.Button button, KeyCode key)
    {
        Debug.Log(button + " " + key);
        switch (button)
        {
            case ButtonUI.Button.MoveUp:
                InputManager.Instance.ClearButton(key);
                if (settings.ContainsKey(ButtonUI.Button.MoveUp))
                {
                    InputManager.Instance.ClearButton(settings[ButtonUI.Button.MoveUp]);
                    settings.Remove(ButtonUI.Button.MoveUp);
                }
                settings.Add(ButtonUI.Button.MoveUp, key);
                InputManager.Instance.SubscribeToButton(MoveUp, key);
                break;

            case ButtonUI.Button.MoveDown:
                InputManager.Instance.ClearButton(key);
                if (settings.ContainsKey(ButtonUI.Button.MoveDown))
                {
                    InputManager.Instance.ClearButton(settings[ButtonUI.Button.MoveDown]);
                    settings.Remove(ButtonUI.Button.MoveDown);
                }
                settings.Add(ButtonUI.Button.MoveDown, key);
                InputManager.Instance.SubscribeToButton(MoveDown, key);
                break;

            case ButtonUI.Button.MoveLeft:
                InputManager.Instance.ClearButton(key);
                if (settings.ContainsKey(ButtonUI.Button.MoveLeft))
                {
                    InputManager.Instance.ClearButton(settings[ButtonUI.Button.MoveLeft]);
                    settings.Remove(ButtonUI.Button.MoveLeft);
                }
                settings.Add(ButtonUI.Button.MoveLeft, key);
                InputManager.Instance.SubscribeToButton(MoveLeft, key);
                break;

            case ButtonUI.Button.MoveRight:
                InputManager.Instance.ClearButton(key);
                if (settings.ContainsKey(ButtonUI.Button.MoveRight))
                {
                    InputManager.Instance.ClearButton(settings[ButtonUI.Button.MoveRight]);
                    settings.Remove(ButtonUI.Button.MoveRight);
                }
                settings.Add(ButtonUI.Button.MoveRight, key);
                InputManager.Instance.SubscribeToButton(MoveRight, key);
                break;
        }
    }
}
