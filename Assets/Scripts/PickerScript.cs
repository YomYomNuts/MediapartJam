using UnityEngine;
using System.Collections;

public class PickerScript : MonoBehaviour
{
    #region Public Attributes
    public Const.LAYER_ACTION_VOTE _LayerObjectAction;
    public Vector3 _OffsetObject;
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    private CharacterScript _Character;
    private GameObject _ObjectCollide;
    private GameObject _ObjectPick;
    private bool _CanBeExecute;
    #endregion

    void Start()
    {
        _Character = this.GetComponent<CharacterScript>();
        _ObjectCollide = null;
        _ObjectPick = null;
        _CanBeExecute = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1_" + _Character._IDJoystick))
        {
            if (_CanBeExecute && (_ObjectPick == null || _ObjectPick != _ObjectCollide))
            {
                _ObjectPick = _ObjectCollide;
                //LaunchAction();
            }
            else
            {
                // Add sound fail action
            }
        }
        else if (_ObjectPick != null && Input.GetButtonDown("Fire2_" + _Character._IDJoystick))
        {
            _ObjectPick = null;
        }

        if (_ObjectPick)
        {
            _ObjectPick.transform.position = this.transform.position + _OffsetObject;
        }
    }

    void OnTriggerEnter2D(Collider2D parCollider)
    {
        OnTriggerStay2D(parCollider);
    }
    void OnTriggerStay2D(Collider2D parCollider)
    {
        if (parCollider.gameObject.layer == (int)_LayerObjectAction)
        {
            _ObjectCollide = parCollider.gameObject;
            ObjectActionPickScript oavs = parCollider.GetComponent<ObjectActionPickScript>();
            _CanBeExecute = false;
            if (oavs)
                _CanBeExecute = oavs.CanBeUse();
        }
    }
    void OnTriggerExit2D(Collider2D parCollider)
    {
        if (parCollider.gameObject.layer == (int)_LayerObjectAction)
        {
            _CanBeExecute = false;
            _ObjectCollide = null;
        }
    }
}
