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
    }

    public override void LaunchAction(CharacterScript parCharacter, GameObject parZoneAction)
    {
        parCharacter._AudioSource.Stop();
        parCharacter._AudioSource.clip = _AudioClipAction;
        parCharacter._AudioSource.Play();
        BoatScript.Instance.RemoveDamage(parZoneAction);
    }
}
