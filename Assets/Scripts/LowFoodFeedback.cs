using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LowFoodFeedback : MonoBehaviour
{
    #region Public Attributes
    [HideInInspector]
    public bool _IsCharacterHungry;

    public Image _SliderBG;
    public float _FeedbackSpeed = 10.0f;
    public AudioSource _AudioSource;
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    private Slider _Slider;
    
    private float _BGColorModifier;
    private bool _Switch;
    #endregion


    void Start ()
    {
        _IsCharacterHungry = false;
        _Slider = this.gameObject.GetComponent<Slider>();
        _BGColorModifier = 0.0f;
        _Switch = false;
    }

    public void StartLowFoodFeedback()
    {
        _IsCharacterHungry = true;

        _AudioSource.Stop();
        _AudioSource.Play();

        StartCoroutine(RunLowFoodFeedback());
    }

    public void StopLowFoodFeedback()
    {
        _IsCharacterHungry = false;

        _AudioSource.Stop();
    }

    IEnumerator RunLowFoodFeedback()
    {
        while (_IsCharacterHungry)
        {
            // Modification de la couleur du background
            _SliderBG.color = new Color(0.02f + _BGColorModifier, 0.1137f, 0.1137f, 1.0f);

            if (!_Switch)
            {
                if (_BGColorModifier < 0.98f)
                    _BGColorModifier += Time.deltaTime * 0.98f * _FeedbackSpeed;
                else
                    _Switch = true;
            }
            else
            {
                if (_BGColorModifier > Mathf.Epsilon)
                    _BGColorModifier -= Time.deltaTime * 0.98f * _FeedbackSpeed;
                else
                    _Switch = false;
            }

            yield return null;
        }

        // Retour à la normal pour BG
        _SliderBG.material.color = new Color(0.02f, 0.1137f, 0.1137f, 1.0f);
    }
}
