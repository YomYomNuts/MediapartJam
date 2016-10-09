using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class SleepingScript : ActionVoteScript
{
    #region Public Attributes
    public float _ValueAddByAction;
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    private bool _IsActivate;
    #endregion

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (_IsActivate)
        {
            _Character.GetComponent<TiredScript>().AddTimer(_ValueAddByAction);
            if (true) // Condition audio
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
        _IsActivate = true;
    }

    public override void EndAction()
    {
        _IsActivate = false;
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
    }

    public override bool AleatoirePondere()
    {
        float value = UnityEngine.Random.Range(0.0f, 1.0f);
        return value >= _Character.GetComponent<TiredScript>().Ratio();
    }
}
