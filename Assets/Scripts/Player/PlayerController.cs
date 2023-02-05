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
        Attack,
        ChargedAttack
    }

    //State используется в первую очередь аниматором
    public State MovementState;
    public State CrossState;

    //settings определяют какая кнопка назначена на каждое из действий
    public Dictionary<ButtonUI.Button, KeyCode> settings;

    public float attackDuration = 0.5f;
    public float dashDuration = 0.5f;

    [Tooltip("время от момента завершения атаки до возможности атаковать снова")]
    public float attackCooldown = 0.3f;
    public float dashCooldown = 0.3f;

    public float defaultSpeed = 3.0f;
    public float speedWhileAttacking = 0.8f;
    public float speedWhileCharging = 1.6f;
    //public float dashSpeed = 5.0f;
    public float dashMultiplayer = 1.6f;


    public float speed = 3.0f;

    public float chargeHoldDuration = 2.0f;

    private Health _myHealth;

    private bool _actionAvialable = true;
    private bool _dashAvialable = true;
    private bool _attackAvialable = true;

    private Rigidbody2D _rigidbody2D = null;

    public Vector2 Direction = new Vector2(0, 0);

    private void MoveUp()
    {
        if (!_actionAvialable) return;
        Direction.y += 1;
        //transform.position += new Vector3(0.0f, 1.0f, 0.0f) * Time.deltaTime * speed;
        //_rigidbody2D.position += new Vector2(0.0f, 1.0f) * Time.deltaTime * speed;
    }
    private void MoveDown()
    {
        if (!_actionAvialable) return;
        Direction.y -= 1;
        //transform.position += new Vector3(0.0f, -1.0f, 0.0f) * Time.deltaTime * speed;
        //_rigidbody2D.position += new Vector2(0.0f, -1.0f) * Time.deltaTime * speed;
    }
    private void MoveRight()
    {
        if (!_actionAvialable) return;
        Direction.x += 1;
        //transform.position += new Vector3(1.0f, 0.0f, 0.0f) * Time.deltaTime * speed;
        //_rigidbody2D.position += new Vector2(1.0f, 0.0f) * Time.deltaTime * speed;
    }
    private void MoveLeft()
    {
        if (!_actionAvialable) return;
        Direction.x -= 1;
        //_rigidbody2D.position += new Vector2(-1.0f, 0.0f) * Time.deltaTime * speed;
    }

    private float _holdDuration = 0f;
    private bool _heldAttackPrevFrame = false;
    private bool _attackHeldThisFrame = false;
    private void Attack()
    {
        _attackHeldThisFrame = true;
        if (_heldAttackPrevFrame)
        {
            speed = speedWhileCharging;
            _holdDuration += Time.deltaTime;
        }
        else
        {
            if (!_actionAvialable) return;
            if (!_attackAvialable) return;
            speed = speedWhileAttacking;
            CrossState = State.Attack;
            _attackAvialable = false;
            _heldAttackPrevFrame = true;
            StartCoroutine(ResetAttack());
        }
    }
    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(attackDuration);
        speed = defaultSpeed;
        CrossState = State.Move;
        yield return new WaitForSeconds(attackCooldown);
        _attackAvialable = true;
    }

    private void ChargedAttack()
    {
        _holdDuration = 0;
        speed = speedWhileAttacking;
        CrossState = State.ChargedAttack;
        _attackAvialable = false;
        StartCoroutine(ResetChargedAttack());
    }

    IEnumerator ResetChargedAttack()
    {
        yield return new WaitForSeconds(attackDuration);
        speed = defaultSpeed;
        CrossState = State.Move;
        yield return new WaitForSeconds(attackCooldown);
        _attackAvialable = true;
    }
    private void Dash()
    {
        if (!_actionAvialable) return;
        if (!_dashAvialable) return;
        StartCoroutine(DashCoroutine());
    }

    private Vector3 dashDirection = new Vector3(); 

    IEnumerator DashCoroutine()
    {
        _dashAvialable = false;
        float dashSpeed = speed * dashMultiplayer;
        _actionAvialable = false;
        _myHealth.invulnrable = true;
        MovementState = State.Roll;
        dashDirection.x = Direction.normalized.x;
        dashDirection.y = Direction.normalized.y;
        float dashTime = 0;
        while (dashTime < dashDuration)
        {
            _rigidbody2D.position += (Vector2)dashDirection * dashSpeed * Time.deltaTime;
            dashTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        _myHealth.invulnrable = false;
        MovementState = State.Move;
        _actionAvialable = true;
        yield return new WaitForSeconds(dashCooldown);
        _dashAvialable = true;
    }

    public bool charged = false;
    private void LateUpdate()
    {
        if (_holdDuration > chargeHoldDuration)
        {
            charged = true;
        }
        else
            charged = false;
        _rigidbody2D.position += Direction.normalized * Time.deltaTime * speed;
        Direction.x = 0;
        Direction.y = 0;
        if (!_attackHeldThisFrame)
        {
            _heldAttackPrevFrame = false;
            if (_holdDuration > chargeHoldDuration)
            {
                ChargedAttack();
            }
        }
        _attackHeldThisFrame = false;
    }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        InputManager.Instance.SubscribeToButton(MoveUp, KeyCode.W);
        InputManager.Instance.SubscribeToButton(MoveDown, KeyCode.S);
        InputManager.Instance.SubscribeToButton(MoveLeft, KeyCode.A);
        InputManager.Instance.SubscribeToButton(MoveRight, KeyCode.D);
        InputManager.Instance.SubscribeToButton(Attack, KeyCode.R);
        InputManager.Instance.SubscribeToButton(Dash, KeyCode.G);
        settings = new Dictionary<ButtonUI.Button, KeyCode>();

        UpdateSettings(ButtonUI.Button.MoveUp, KeyCode.W);
        UpdateSettings(ButtonUI.Button.MoveDown, KeyCode.S);
        UpdateSettings(ButtonUI.Button.MoveLeft, KeyCode.A);
        UpdateSettings(ButtonUI.Button.MoveRight, KeyCode.D);
        UpdateSettings(ButtonUI.Button.Attack, KeyCode.R);
        UpdateSettings(ButtonUI.Button.Dash, KeyCode.G);
        _myHealth = GetComponent<Health>();
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
        //
        //
        //
        //секретная строка, которая выключает мультикнопочность
      ////// 
       ////
        //
        settings.Add(button, key);
        KeyUIManager.Instance.UpdateButtonUI(key, button);
    }

    //Назначение определеленной кнопки на действие происходит в этой функции
    public void SetAction(ButtonUI.Button button, KeyCode key)
    {
        InputManager.Instance.ClearButton(key);
        InputManager.Instance.changedControls = true;
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
