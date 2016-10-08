using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class ActionVoteScript : MonoBehaviour
{
    #region Public Attributes
    public Const.LAYER_ACTION_VOTE _LayerObjectAction;
    public string _KeyActionLoc;
    public GameObject _PositionOwner;
    public GameObject _PositionAction;
    public GameObject _PositionCharacterReceiver;
    #endregion

    #region Protected Attributes
    protected CharacterScript _Character;
    #endregion

    #region Private Attributes
    private bool _CanBeExecute;
    #endregion

    void Start()
    {
        _Character = this.GetComponent<CharacterScript>();
        _CanBeExecute = false;
    }

    void Update()
    {
        if (_CanBeExecute && Input.GetButtonDown("Fire1_" + _Character._IDJoystick))
            LaunchAction();
    }

    void OnTriggerEnter2D(Collider2D parCollider)
    {
        OnTriggerStay2D(parCollider);
    }
    void OnTriggerStay2D(Collider2D parCollider)
    {
        if (parCollider.gameObject.layer == (int)_LayerObjectAction)
        {
            ObjectActionVoteScript oavs = parCollider.GetComponent<ObjectActionVoteScript>();
            _CanBeExecute = false;
            if (oavs)
                _CanBeExecute = oavs.CanBeUse();
        }
    }
    void OnTriggerExit2D(Collider2D parCollider)
    {
        if (parCollider.gameObject.layer == (int)_LayerObjectAction)
            _CanBeExecute = false;
    }


    protected abstract void LaunchAction();
    public abstract void DisplayAction();
    public abstract bool AleatoirePondere();
}
