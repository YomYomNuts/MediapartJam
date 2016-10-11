using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class GettingOutScript : ActionVoteScript
{
    #region Public Attributes
    public GameObject _Kick;
    public float _TimerBlock;
    public Transform _PositionNoyed;
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    private bool _IsActivate;
    private float _CurrentTimer;
    private CharacterScript _CharacterReceiver;
    #endregion

    protected override void Update()
    {
        base.Update();
        if (_IsActivate)
        {
            _CurrentTimer += Time.deltaTime;
            if (!_Character._AudioSource.isPlaying || _CurrentTimer > _TimerBlock)
                EndAction();
        }
    }

    protected override void LaunchAction()
    {
        if (_Character.CurrentAction == null)
        {
            GameObject go = GetClosest();
            if (go != null)
                go.GetComponent<ObjectActionScript>().Use();
            ChooseVoteScript.Instance.ShowPancarte(this);
        }
    }

    public override void ValidateAction()
    {
        base.ValidateAction();
        _IsActivate = true;
        _Character._Animator.SetBool("Kicking", true);
        _Kick.SetActive(true);
        _CurrentTimer = 0.0f;
        GameScript.Instance.PlayerCanAction = false;
        GameObject go = GetClosest();
        if (go != null)
        {
            CharacterScript ch = go.GetComponentInParent<CharacterScript>();
            ch.gameObject.transform.position = new Vector3(ch.gameObject.transform.position.x, _PositionNoyed.position.y, ch.gameObject.transform.position.z);
            ch._Animator.SetBool("Noyed", true);
            ch.Dead();
            _CharacterReceiver = ch;
            _ObjectsCollide.Remove(go);
        }
    }

    public override void EndAction()
    {
        _IsActivate = false;
        _Character._Animator.SetBool("Kicking", false);
        _Kick.SetActive(false);
        GameScript.Instance.PlayerCanAction = true;
        if (_CharacterReceiver != null)
        {
            _CharacterReceiver.GetComponent<NoyedScript>().enabled = true;
            _CharacterReceiver.GetComponent<PickerScript>().UnPick();
            _CharacterReceiver = null;
        }
        else
        {
            GameObject go = GetClosest();
            if (go != null)
                go.GetComponent<ObjectActionScript>().UnUse();
        }
    }

    public override void DisplayAction()
    {
        _PositionOwner.SetActive(true);
        _PositionOwner.GetComponent<Image>().sprite = _Character._Face;
        _PositionAction.SetActive(true);
        _PositionAction.GetComponent<Text>().text = _KeyActionLoc;
        _PositionCharacterReceiver.SetActive(true);
        GameObject go = GetClosest();
        if (go != null)
            _PositionCharacterReceiver.GetComponent<Image>().sprite = go.transform.parent.GetComponent<CharacterScript>()._Face;
    }

    public override bool AleatoirePondere()
    {
        return UnityEngine.Random.Range(0, 2) == 1; // TODO
    }
}
