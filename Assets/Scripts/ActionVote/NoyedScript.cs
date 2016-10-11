using UnityEngine;
using System.Collections;

public class NoyedScript : MonoBehaviour
{
    #region Public Attributes
    public float _SpeedX;
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
        Vector3 pos = this.transform.position;
        pos.x += _SpeedX;
        this.transform.position = pos;
    }
}
