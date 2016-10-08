using UnityEngine;
using System.Collections;

public abstract class ComportementScript : MonoBehaviour
{
    #region Public Attributes
    public float _Timer;
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    private float _CurrentTimer;
    #endregion

    void Start()
    {
        _CurrentTimer = Random.Range(_Timer / 2.0f, _Timer);
    }
	
	void Update()
    {
        AddTimer(-Time.deltaTime);
    }

    public void AddTimer(float parValue)
    {
        _CurrentTimer = Mathf.Clamp(_CurrentTimer + parValue, 0.0f, _Timer);
    }

    public float Ratio()
    {
        return _CurrentTimer / _Timer;
    }
}
