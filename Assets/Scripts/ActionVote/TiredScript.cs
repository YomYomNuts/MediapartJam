using UnityEngine;
using System.Collections;

public class TiredScript : ComportementScript
{
    #region Public Attributes
    public float _DivisorSpeed = 2.0f;
    public GameObject _ZZZ;
    public AudioClip _AudioClip;
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    private float _InitialMaxSpeed;
    private float _TiredMaxSpeed;
    private bool _PreviousFinish;
    #endregion

    protected override void Start()
    {
        base.Start();
        _InitialMaxSpeed = _Character._MaxSpeed;
        _TiredMaxSpeed =_Character._MaxSpeed / _DivisorSpeed;
        _PreviousFinish = false;
    }

    protected override void Update()
    {
        base.Update();
        if (IsFinish())
        {
            _Character._MaxSpeed = _TiredMaxSpeed;
            _ZZZ.SetActive(true);
            if (!_PreviousFinish)
            {
                _Character._AudioSource.Stop();
                _Character._AudioSource.clip = _AudioClip;
                _Character._AudioSource.Play();
            }
        }
        else
        {
            _Character._MaxSpeed = _InitialMaxSpeed;
            _ZZZ.SetActive(false);
        }
        _PreviousFinish = IsFinish();
    }
}
