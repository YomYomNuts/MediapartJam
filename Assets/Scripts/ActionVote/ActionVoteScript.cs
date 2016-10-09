using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class ActionVoteScript : ActionScript
{
    #region Public Attributes
    public AudioClip _AudioClipAction;
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
        if (!GameScript.Instance.IsGamePause() && Input.GetButtonDown("Fire1_" + _Character._IDJoystick))
        {
            GameObject go = GetClosest();
            if (go != null)
            {
                if (go.GetComponent<ObjectActionScript>().CanBeUse() && GameScript.Instance.PlayerCanAction && !_Character.GetComponent<PickerScript>().IsPicking())
                {
                    LaunchAction();
                }
                else
                {
                    _AudioSource.Stop();
                    _AudioSource.clip = GameScript.Instance._AudioClipError;
                    _AudioSource.Play();
                }
            }
        }
    }

    public virtual void ValidateAction()
    {
        _AudioSource.Stop();
        _AudioSource.clip = _AudioClipAction;
        _AudioSource.Play();
    }

    protected abstract void LaunchAction();
    public abstract void EndAction();
    public abstract void DisplayAction();
    public abstract bool AleatoirePondere();
}
