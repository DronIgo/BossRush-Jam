using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyUIManager : MonoBehaviour
{
    public static KeyUIManager Instance;
    private Dictionary<KeyCode, KeyUI> _keyUIs = new Dictionary<KeyCode, KeyUI>();
    public List<Color> ButtonColors = new List<Color>();
    public List<ButtonUI.Button> Buttons = new List<ButtonUI.Button>() { ButtonUI.Button.MoveUp, ButtonUI.Button.MoveDown, ButtonUI.Button.MoveRight, ButtonUI.Button.MoveLeft, ButtonUI.Button.None};
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
        Debug.Log(b_index);
        _keyUIs[key].gameObject.GetComponent<Image>().color = ButtonColors[b_index];
        _keyUIs[key].transform.Find("Image").GetComponent<Image>().color = ButtonColors[b_index];

    }
}
