using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// KeyUIManager - класс синглтон, отвечающий за отображение UI элементов кнопок, метода типа UpdateUI, который бы самостоятельно полностью обновлял UI здесь нет,
/// так что каждое отдельное обновлениевызывается из других методов
/// </summary>
public class KeyUIManager : MonoBehaviour
{
    public static KeyUIManager Instance;
    private Dictionary<KeyCode, KeyUI> _keyUIs = new Dictionary<KeyCode, KeyUI>();
    public List<Color> ButtonColors = new List<Color>();
    public List<ButtonUI.Button> Buttons = new List<ButtonUI.Button>() { ButtonUI.Button.MoveUp, ButtonUI.Button.MoveDown, ButtonUI.Button.MoveRight, ButtonUI.Button.MoveLeft, ButtonUI.Button.None};

    public List<KeyCode> DeletedButtons = new List<KeyCode>();
    private void Awake()
    {
        Instance = this;
        var allKeyUIs = FindObjectsOfType<KeyUI>();
        foreach (var key in allKeyUIs)
        {
            _keyUIs.Add(key.Key, key);
        }
    }

    public void UpdateButtonUI(KeyCode key, ButtonUI.Button button)
    {
        int b_index = Buttons.FindIndex((x) => x == button);
        _keyUIs[key].gameObject.GetComponent<Image>().color = ButtonColors[b_index];
        _keyUIs[key].transform.Find("Image").GetComponent<Image>().color = ButtonColors[b_index];

    }

    public void DisableKey(KeyCode key)
    {
        InputManager.Instance.ClearButton(key);
        _keyUIs[key].gameObject.SetActive(false);
        DeletedButtons.Add(key);
    }

    public void EnableKey(KeyCode key)
    {
        DeletedButtons.Remove(key);
        _keyUIs[key].gameObject.SetActive(true);
    }
}
