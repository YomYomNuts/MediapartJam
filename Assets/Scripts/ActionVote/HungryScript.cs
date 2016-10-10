using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HungryScript : ComportementScript
{
    #region Public Attributes
    public Slider _Slider;
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    #endregion

    protected override void Start()
    {
        base.Start();
        _Slider.maxValue = _Timer;
    }

    protected override void Update()
    {
        base.Update();
        _Slider.value = _CurrentTimer;

        if (_CurrentTimer <= 0.0f)
            _Character.Dead();
    }
}
