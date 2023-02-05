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
    [SerializeField] private GameObject _damageWall;
    private bool[] triggers = new bool[] { false, false, false };
    public void SetTrigger(int index)
    {
        triggers[index] = true;
    }
    private void Start()
    {
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
    }
}
