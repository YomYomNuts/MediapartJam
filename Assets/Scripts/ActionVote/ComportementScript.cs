using UnityEngine;
using System.Collections;

public abstract class ComportementScript : MonoBehaviour
{
    #region Public Attributes
    public float _Timer;
    #endregion

    #region Protected Attributes
    protected CharacterScript _Character;
    protected float _CurrentTimer;
    #endregion

    #region Private Attributes
    #endregion

    protected virtual void Start()
    {
        _Character = this.GetComponent<CharacterScript>();
        _CurrentTimer = Random.Range(_Timer / 2.0f, _Timer);
    }
	
	protected virtual void Update()
    {
        if (!GameScript.Instance.IsGamePause())
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

    public bool IsFinish()
    {
        return _CurrentTimer == 0.0f;
    }
}
