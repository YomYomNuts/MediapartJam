﻿using UnityEngine;
using System.Collections;

public class MovingBoatScript : ObjectActionPickScript
{
    #region Public Attributes
    public GameObject _ParentObjectsMoving;
    public float _Offset;
    public float _TimeForDeplacement = 3.0f;
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    #endregion

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void LaunchAction(CharacterScript parCharacter, GameObject parZoneAction)
    {
        MovingBoatZoneScript mbzs = parZoneAction.GetComponent<MovingBoatZoneScript>();
        if (mbzs)
        {
            parCharacter._AudioSource.Stop();
            parCharacter._AudioSource.clip = _AudioClipAction;
            parCharacter._AudioSource.Play();
            StartCoroutine(MoveBoat(mbzs._Direction * _Offset));
        }
    }

    IEnumerator MoveBoat(Vector3 parDirection)
    {
        float startMove = 0.0f;
        Vector3 normalizeDirectionBySecond = parDirection / _TimeForDeplacement;
        while (startMove < _TimeForDeplacement)
        {
            for (int i = 0; i < _ParentObjectsMoving.transform.childCount; ++i)
            {
                Transform child = _ParentObjectsMoving.transform.GetChild(i);
                child.position += normalizeDirectionBySecond * Time.deltaTime;
            }
            startMove += Time.deltaTime;
            yield return 0.0f;
        }
    }
}
