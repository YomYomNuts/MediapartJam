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
    #endregion

    protected override void LaunchAction()
    {
        if (_Character.CurrentAction == null)
        {
            ChooseVoteScript.Instance.ShowPancarte(this);
        }
    }

    public override void ValidateAction()
    {

    }

    public override void DisplayAction()
    {
        _PositionOwner.SetActive(true);
        _PositionOwner.GetComponent<Image>().sprite = _Character._Face;
        _PositionAction.SetActive(true);
        _PositionAction.GetComponent<Text>().text = _KeyActionLoc;
        _PositionCharacterReceiver.SetActive(true);
        _PositionCharacterReceiver.GetComponent<Image>().sprite = _ObjectCollide.transform.parent.GetComponent<CharacterScript>()._Face;
    }

    public override bool AleatoirePondere()
    {
        return UnityEngine.Random.Range(0, 2) == 1; // TODO
    }
}
