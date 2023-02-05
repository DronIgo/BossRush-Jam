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



    public List<KeyCode> avialableButtons = new List<KeyCode>();
    public List<KeyCode> DeletedButtons = new List<KeyCode>();
    private void Awake()
    {
        Instance = this;
        
        var allKeyUIs = FindObjectsOfType<KeyUI>();
        foreach (var key in allKeyUIs)
        {
            avialableButtons.Add(key.Key);
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
        //_keyUIs[key].gameObject.SetActive(false);
        _keyUIs[key].Destroy();
        DeletedButtons.Add(key);
        avialableButtons.Remove(key);
    }

    public void EnableKey(KeyCode key)
    {
        DeletedButtons.Remove(key);
        avialableButtons.Add(key);
        //_keyUIs[key].gameObject.SetActive(true);
        _keyUIs[key].Revive();
    }

    public void EnableKey()
    {
        int r = Random.Range(0, DeletedButtons.Count);
        var key = DeletedButtons[r];
        EnableKey(key);
    }

    public void DestroyButtons(int amount)
    {
        int done = 0;
        List<KeyCode> avialableForDeletion = new List<KeyCode>();
        foreach (var key in avialableButtons)
        {
            if (!PlayerController.Instance.settings.ContainsValue(key))
            {
                avialableForDeletion.Add(key);
            }
        }
        if (avialableForDeletion.Count <= amount)
        {
            amount -= avialableForDeletion.Count;
            foreach (var key in avialableForDeletion)
            {
                DisableKey(key);
            }
            avialableForDeletion = new List<KeyCode>();
            foreach (var key in avialableButtons)
            {
                if (PlayerController.Instance.settings.ContainsValue(key))
                {
                    avialableForDeletion.Add(key);
                }
            }
        }
        for (int i = 0; i < amount; ++i)
        {
            int r = Random.Range(0, avialableForDeletion.Count);
            DisableKey(avialableForDeletion[r]);
            avialableForDeletion.RemoveAt(r);
        }
    }
}
