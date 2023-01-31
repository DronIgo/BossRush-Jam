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

    //State используется в первую очередь аниматором
    public State CurrentState;

    //settings определяют какая кнопка назначена на каждое из действий
    public Dictionary<ButtonUI.Button, KeyCode> settings;

    public float attackDuration = 0.8f;
    public float dashDuration = 0.5f;

    [Tooltip("время от момента завершения атаки до возможности атаковать снова")]
    public float attackCooldown = 0.8f;

    public float defaultSpeed = 3.0f;
    public float speedWhileAttacking = 0.8f;

    public float speed = 3.0f;


    private bool _attackAvialable = true;

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
    private void Attack()
    {
        if (!_attackAvialable) return;
        speed = speedWhileAttacking;
        CurrentState = State.Attack;
        _attackAvialable = false;
        StartCoroutine(ResetAttack());
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackDuration);
        speed = defaultSpeed;
        CurrentState = State.Move;
        yield return new WaitForSeconds(attackCooldown);
        _attackAvialable = true;
    }
    private void Awake()
    {
        Instance = this;
        InputManager.Instance.SubscribeToButton(MoveUp, KeyCode.W);
        InputManager.Instance.SubscribeToButton(MoveDown, KeyCode.S);
        InputManager.Instance.SubscribeToButton(MoveLeft, KeyCode.A);
        InputManager.Instance.SubscribeToButton(MoveRight, KeyCode.D);
        InputManager.Instance.SubscribeToButton(Attack, KeyCode.R);

        settings = new Dictionary<ButtonUI.Button, KeyCode>();

        UpdateSettings(ButtonUI.Button.MoveUp, KeyCode.W);
        UpdateSettings(ButtonUI.Button.MoveDown, KeyCode.S);
        UpdateSettings(ButtonUI.Button.MoveLeft, KeyCode.A);
        UpdateSettings(ButtonUI.Button.MoveRight, KeyCode.D);
        UpdateSettings(ButtonUI.Button.Attack, KeyCode.R);
    }

    //Добавляем соответствующую кнопку в настройки и меняем цвет кнопки
    private void UpdateSettings(ButtonUI.Button button, KeyCode key)
    {
        if (settings.ContainsKey(button))
        {
            KeyUIManager.Instance.UpdateButtonUI(settings[button], ButtonUI.Button.None);
            InputManager.Instance.ClearButton(settings[button]);
            settings.Remove(button);
        }
        settings.Add(button, key);
        KeyUIManager.Instance.UpdateButtonUI(key, button);
    }

    //Назначение определеленной кнопки на действие происходит в этой функции
    public void SetAction(ButtonUI.Button button, KeyCode key)
    {
        InputManager.Instance.ClearButton(key);
        UpdateSettings(button, key);
        switch (button)
        {
            case ButtonUI.Button.MoveUp:
                InputManager.Instance.SubscribeToButton(MoveUp, key);
                break;

            case ButtonUI.Button.MoveDown:
                InputManager.Instance.SubscribeToButton(MoveDown, key);
                break;

            case ButtonUI.Button.MoveLeft:
                InputManager.Instance.SubscribeToButton(MoveLeft, key);
                break;

            case ButtonUI.Button.MoveRight:
                
                InputManager.Instance.SubscribeToButton(MoveRight, key);
                break;
        }
    }
}
