using UnityEngine;
using System.Collections;

public abstract class ObjectActionPickScript : ObjectActionScript
{
    #region Public Attributes
    public Const.LAYER_ACTION_PICK _LayerZone;
    public AudioClip _AudioClipAction;
    public AudioClip _AudioClipDrop;
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

    public abstract void LaunchAction(CharacterScript parCharacter, GameObject parZoneAction);
}
