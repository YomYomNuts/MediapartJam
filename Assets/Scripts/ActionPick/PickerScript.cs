using UnityEngine;
using System.Collections;

public class PickerScript : MonoBehaviour
{
    #region Public Attributes
    public Const.LAYER_ACTION_VOTE _LayerObjectAction;
    public Transform _OffsetObject;
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    private CharacterScript _Character;
    private GameObject _ObjectCollide;
    private GameObject _ObjectPick;
    private GameObject _ZoneUsePick;
    private bool _CanBeExecute;
    private bool _CanLaunchAction;
    #endregion

    void Start()
    {
        _Character = this.GetComponent<CharacterScript>();
        _ObjectCollide = null;
        _ObjectPick = null;
        _ZoneUsePick = null;
        _CanBeExecute = false;
        _CanLaunchAction = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1_" + _Character._IDJoystick))
        {
            if (_ObjectPick != null)
            {
                if (_CanBeExecute && _ObjectCollide != _ObjectPick)
                {
                    _ObjectPick.GetComponent<Collider2D>().enabled = true;
                    _ObjectPick.GetComponent<ObjectActionPickScript>().UnUse();
                    _ObjectPick = _ObjectCollide;
                    _ObjectPick.GetComponent<ObjectActionPickScript>().Use();
                    _ObjectPick.GetComponent<Collider2D>().enabled = false;
                    _CanLaunchAction = false;
                }
                else
                {
                    if (_CanLaunchAction)
                        _ObjectPick.GetComponent<ObjectActionPickScript>().LaunchAction(_Character, _ZoneUsePick);
                    else
                    {
                        // Add sound fail action
                    }
                }
            }
            else
            {
                if (_CanBeExecute)
                {
                    _ObjectPick = _ObjectCollide;
                    _ObjectPick.GetComponent<ObjectActionPickScript>().Use();
                    _ObjectPick.GetComponent<Collider2D>().enabled = false;
                    _CanLaunchAction = false;
                    _ZoneUsePick = null;
                }
                else
                {
                    // Add sound fail action
                }
            }
        }
        else if (_ObjectPick != null && Input.GetButtonDown("Fire2_" + _Character._IDJoystick))
        {
            _ObjectPick.GetComponent<Collider2D>().enabled = true;
            _ObjectPick.GetComponent<ObjectActionPickScript>().UnUse();
            _ObjectPick = null;
            _CanLaunchAction = false;
            _ZoneUsePick = null;
        }

        if (_ObjectPick)
        {
            _ObjectPick.transform.position = _OffsetObject.position;
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
        if (_ObjectPick != null && parCollider.gameObject.layer == (int)_ObjectPick.GetComponent<ObjectActionPickScript>()._LayerZone)
        {
            _CanLaunchAction = true;
            _ZoneUsePick = parCollider.gameObject;
        }
    }
    void OnTriggerExit2D(Collider2D parCollider)
    {
        if (parCollider.gameObject.layer == (int)_LayerObjectAction)
        {
            _CanBeExecute = false;
            _ObjectCollide = null;
        }
        if (_ObjectPick != null && parCollider.gameObject.layer == (int)_ObjectPick.GetComponent<ObjectActionPickScript>()._LayerZone)
        {
            _CanLaunchAction = false;
            _ZoneUsePick = null;
        }
    }

    public bool IsPicking()
    {
        return _ObjectPick != null;
    }
}
