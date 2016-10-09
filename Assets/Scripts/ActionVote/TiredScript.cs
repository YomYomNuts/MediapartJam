using UnityEngine;
using System.Collections;

public class TiredScript : ComportementScript
{
    #region Public Attributes
    public float _DivisorSpeed = 2.0f;
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    private float _InitialMaxSpeed;
    private float _TiredMaxSpeed;
    #endregion

    protected override void Start()
    {
        base.Start();
        _InitialMaxSpeed = _Character._MaxSpeed;
        _TiredMaxSpeed =_Character._MaxSpeed / _DivisorSpeed;
    }

    protected override void Update()
    {
        base.Update();
        if (IsFinish())
            _Character._MaxSpeed = _TiredMaxSpeed;
        else
            _Character._MaxSpeed = _InitialMaxSpeed;
    }
}
