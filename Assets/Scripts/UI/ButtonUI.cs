using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUI : MonoBehaviour
{
    public enum Button { 
        MoveUp,
        MoveDown,
        MoveRight,
        MoveLeft,
        Attack,
        Dash
    }

    public Button MyButton;
}
