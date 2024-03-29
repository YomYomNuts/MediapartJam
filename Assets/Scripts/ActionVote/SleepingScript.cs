﻿using UnityEngine;
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
    private float _StartingTimer;
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
            _Character.GetComponent<HungryScript>().AddTimer((_ValueAddByAction * Time.deltaTime) / _AudioClipAction.length);
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
        _Character._BlockMovement = true;
        _Character._Animator.SetBool("Sleeping", true);
        _StartingTimer = _Character.GetComponent<TiredScript>().GetCurrentTimer();
    }

    public override void EndAction()
    {
        _IsActivate = false;
        _Character._BlockMovement = false;
        _Character._Animator.SetBool("Sleeping", false);
        _Character.GetComponent<TiredScript>().SetCurrentTimer(_StartingTimer + _ValueAddByAction);
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
