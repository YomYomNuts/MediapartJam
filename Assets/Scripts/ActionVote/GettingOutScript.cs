using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class GettingOutScript : ActionVoteScript
{
    #region Public Attributes
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    private bool _IsActivate;
    #endregion

    protected override void Update()
    {
        base.Update();
        if (_IsActivate)
        {
            if (!_Character._AudioSource.isPlaying)
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
        GameScript.Instance.PlayerCanAction = false;
    }

    public override void EndAction()
    {
        _IsActivate = false;
        GameScript.Instance.PlayerCanAction = true;
        GameObject go = GetClosest();
        if (go != null)
            go.GetComponent<ObjectActionScript>().UnUse();
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
