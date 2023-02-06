using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private TextMeshProUGUI _tutorialText;
    [SerializeField] private GameObject _puppet;
    [SerializeField] private GameObject _dragon;
    [SerializeField] private GameObject _puppetEnemy;
    [SerializeField] private GameObject _dragonEnemy;
    [SerializeField] private GameObject _damageWall;
    [SerializeField] private GameObject _garantedDamageWall;
    [SerializeField] private Animator _animator;

    private Health _health;
    private bool[] triggers = new bool[] { false, false, false };
    public void SetTrigger(int index)
    {
        triggers[index] = true;
    }
    private void Start()
    {
        _health = GetComponent<Health>();
        _animator = GetComponent<Animator>();
        _startPosition = _player.transform.position;
        StartCoroutine(Tutorial());
    }

    Vector3 _startPosition;

    private void Update()
    {
        
    }

    IEnumerator Tutorial()
    {
        yield return new WaitForSeconds(2f);
        _tutorialText.text = "Use Red, Green, Yellow and Orange buttons to move";
        yield return new WaitUntil(() => Vector3.Distance(_startPosition, _player.transform.position) >= 2f);
        _tutorialText.text = "You can change the controls at any time, drag a key onto action to do that";
        yield return new WaitUntil(() => InputManager.Instance.changedControls);
        _puppet.SetActive(true);
        _tutorialText.text = "Use Blue key to attack";
        yield return new WaitUntil(() => triggers[0]);
        _tutorialText.text = "Hold blue key to charge powerful attack";
        _dragon.SetActive(true);
        yield return new WaitUntil(() => triggers[1]);
        _tutorialText.text = "Press purple key to use dash which gives you invincibility";
        _damageWall.SetActive(true);
        _puppet.GetComponent<Health>().RestoreHealth(2);
        _puppet.SetActive(true);
        triggers[0] = false;
        yield return new WaitUntil(() => triggers[0]);
        _damageWall.SetActive(false);
        _tutorialText.text = "Taking damage breaks one of your buttons";
        yield return new WaitForSeconds(2f);
        _garantedDamageWall.SetActive(true);
        yield return new WaitForSeconds(2f);
        _tutorialText.text = "Now...";
        _animator.SetBool("boss", true);
        yield return new WaitForSeconds(1f);
        _tutorialText.text = "Let's start the play";
        StartCoroutine(BossPhase());
    }

    IEnumerator BossPhase()
    {
        int wave = 0;
        while (_health.health > 0)
        {
            wave++;
            triggers[0] = false;
            triggers[1] = false;
            Vector3 dragonPos = GetRandomPosition();
            Vector3 puppetPos = GetRandomPosition();
            _dragonEnemy.transform.position = dragonPos;
            _puppetEnemy.transform.position = puppetPos;
            _dragonEnemy.GetComponent<Health>().RestoreHealth(3f, false);
            _puppetEnemy.GetComponent<Health>().RestoreHealth(2f, false);
            _dragonEnemy.SetActive(true);
            _puppetEnemy.SetActive(true);
            yield return new WaitUntil(() => triggers[0] && triggers[1]);
        }
    }

    Vector3 GetRandomPosition()
    {
        float x = Random.Range(-8f, 8f);
        float y = Random.Range(-3f, 4f);
        return new Vector3(x, y, 0);
    }
}
