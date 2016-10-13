using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HungryScript : ComportementScript
{
    #region Public Attributes
    public Slider _Slider;
    public AudioClip _AudioClipStarving;

    public float _HungryStateThreshold = 12.0f;
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    private LowFoodFeedback _SliderFeedback;
    private AudioSource _AudioSource;
    #endregion

    protected override void Start()
    {
        base.Start();
        _Slider.maxValue = _Timer;
        _SliderFeedback = _Slider.GetComponent<LowFoodFeedback>();
        _AudioSource = GetComponent<AudioSource>();
    }

    protected override void Update()
    {
        base.Update();
        _Slider.value = _CurrentTimer;

        if (_CurrentTimer <= 0.0f)
        {
            _AudioSource.Stop();
            _AudioSource.clip = _AudioClipStarving;
            _AudioSource.Play();

            _Character._Animator.SetBool("Dead", true);
            _Character.Dead();
        }
        else if(!_SliderFeedback._IsCharacterHungry && _CurrentTimer <= _HungryStateThreshold)
        {
            _SliderFeedback._IsCharacterHungry = true;
            _SliderFeedback.StartLowFoodFeedback();
        }
    }
}
