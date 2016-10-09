using UnityEngine;
using System.Collections;

public class PickerScript : ActionScript
{
    #region Public Attributes
    public Transform _OffsetObject;
    #endregion

    #region Protected Attributes
    #endregion

    #region Prisvate Attributes
    private GameObject _ObjectPick;
    private GameObject _ZoneUsePick;
    private bool _CanLaunchAction;
    #endregion

    protected override void Start()
    {
        base.Start();
        _ObjectPick = null;
        _ZoneUsePick = null;
        _CanLaunchAction = false;
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetButtonDown("Fire1_" + _Character._IDJoystick))
        {
            GameObject go = GetClosest();
            if (go != null)
            {
                if (_ObjectPick != null)
                {
                    if (go.GetComponent<ObjectActionScript>().CanBeUse() && GameScript.Instance.PlayerCanAction && go != _ObjectPick)
                    {
                        _ObjectPick.GetComponent<Collider2D>().enabled = true;
                        _ObjectPick.GetComponent<ObjectActionPickScript>().UnUse();
                        _ObjectPick = go;
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
                    if (go.GetComponent<ObjectActionScript>().CanBeUse() && GameScript.Instance.PlayerCanAction)
                    {
                        _ObjectPick = go;
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

    protected override void OnTriggerStay2D(Collider2D parCollider)
    {
        base.OnTriggerStay2D(parCollider);
        if (_ObjectPick != null && parCollider.gameObject.layer == (int)_ObjectPick.GetComponent<ObjectActionPickScript>()._LayerZone)
        {
            _CanLaunchAction = true;
            _ZoneUsePick = parCollider.gameObject;
        }
    }
    protected override void OnTriggerExit2D(Collider2D parCollider)
    {
        base.OnTriggerExit2D(parCollider);
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
