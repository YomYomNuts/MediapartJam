using UnityEngine;
using System.Collections;
using System;

public class HammerScript : ObjectActionPickScript
{
    #region Public Attributes
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
        if (_IsActivate)
        {
            if (!_CharacterApplyAction._AudioSource.isPlaying)
            {
                _IsActivate = false;
                _CharacterApplyAction._BlockMovement = false;
                _CharacterApplyAction._Animator.SetBool("Repair", false);
            }
        }
    }

    public override void LaunchAction(CharacterScript parCharacter, GameObject parZoneAction)
    {
        _CharacterApplyAction = parCharacter;
        _CharacterApplyAction._AudioSource.Stop();
        _CharacterApplyAction._AudioSource.clip = _AudioClipAction;
        _CharacterApplyAction._AudioSource.Play();
        _CharacterApplyAction._BlockMovement = true;
        _IsActivate = true;
        BoatScript.Instance.RemoveDamage(parZoneAction);
        _CharacterApplyAction._Animator.SetBool("Repair", true);
    }
}
