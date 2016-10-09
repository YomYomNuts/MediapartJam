using UnityEngine;
using System.Collections;

public abstract class ObjectActionPickScript : ObjectActionScript
{
    #region Public Attributes
    public Const.LAYER_ACTION_PICK _LayerZone;
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

    public abstract void LaunchAction(CharacterScript parCharacter, GameObject parZoneAction);
}
