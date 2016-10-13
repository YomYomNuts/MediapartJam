using UnityEngine;
using System.Collections;

public class ObjectMoving : MonoBehaviour
{
    #region Public Attributes
    public float _SpeedX;
    public GameObject _Goal;
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    #endregion

    void Start()
    {
    }

    void FixedUpdate()
    {
        Vector3 previousPos = this.transform.position;
        Vector3 pos = this.transform.position;
        pos.x += _SpeedX;
        this.transform.position = pos;

        if (_Goal != null)
        {
            float diffX = _Goal.transform.position.x - previousPos.x;
            float newDiffX = _Goal.transform.position.x - pos.x;
            if (Mathf.Sign(diffX) != Mathf.Sign(newDiffX) || newDiffX == 0.0f)
            {
                this.transform.position = _Goal.transform.position;
                this.enabled = false;
            }
        }
    }
}
