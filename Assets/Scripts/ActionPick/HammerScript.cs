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

    void Start()
    {
    }

    void Update()
    {
    }

    public override void LaunchAction(CharacterScript parCharacter, GameObject parZoneAction)
    {
        BoatScript.Instance.RemoveDamage(parZoneAction);
    }
}
