using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class SleepingScript : ActionVoteScript
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

    public override void DisplayAction()
    {
        _PositionOwner.SetActive(true);
        _PositionOwner.GetComponent<Image>().sprite = _Character._Face;
        _PositionAction.SetActive(true);
        _PositionAction.GetComponent<Text>().text = _KeyActionLoc;
    }

    public override bool AleatoirePondere()
    {
        float value = UnityEngine.Random.Range(0.0f, 1.0f);
        return value >= _Character.GetComponent<TiredScript>().Ratio();
    }
}
