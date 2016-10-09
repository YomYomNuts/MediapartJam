using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class ActionVoteScript : ActionScript
{
    #region Public Attributes
    public string _KeyActionLoc;
    public GameObject _PositionOwner;
    public GameObject _PositionAction;
    public GameObject _PositionCharacterReceiver;
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    #endregion

    protected override void Update()
    {
        base.Update();
        if (Input.GetButtonDown("Fire1_" + _Character._IDJoystick))
        {
            GameObject go = GetClosest();
            if (go != null)
            {
                if (go.GetComponent<ObjectActionScript>().CanBeUse() && GameScript.Instance.PlayerCanAction && !_Character.GetComponent<PickerScript>().IsPicking())
                    LaunchAction();
                else
                {
                    // Add sound fail action
                }
            }
        }
    }


    protected abstract void LaunchAction();
    public abstract void ValidateAction();
    public abstract void EndAction();
    public abstract void DisplayAction();
    public abstract bool AleatoirePondere();
}
