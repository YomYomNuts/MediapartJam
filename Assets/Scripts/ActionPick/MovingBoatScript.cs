using UnityEngine;
using System.Collections;

public class MovingBoatScript : ObjectActionPickScript
{
    #region Public Attributes
    public GameObject _ParentObjectsMoving;
    public float _Offset;
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
        MovingBoatZoneScript mbzs = parZoneAction.GetComponent<MovingBoatZoneScript>();
        if (mbzs)
        {
            Vector3 direction = mbzs._Direction * _Offset;
            for (int i = 0; i < _ParentObjectsMoving.transform.childCount; ++i)
            {
                Transform child = _ParentObjectsMoving.transform.GetChild(i);
                child.position += direction;
            }
        }
    }
}
