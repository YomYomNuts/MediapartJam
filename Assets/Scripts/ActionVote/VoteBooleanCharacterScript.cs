using UnityEngine;
using System.Collections;

public class VoteBooleanCharacterScript : MonoBehaviour
{
    #region Public Attributes
    public string _Yes = "Fire1";
    public string _No = "Fire2";
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    private CharacterScript _Character;
    private Const.BOOLEAN_VOTE _CurrentChoice;
    #endregion

    void Start()
    {
        _Character = this.GetComponent<CharacterScript>();
    }

    void Update()
    {
        if (_CurrentChoice == Const.BOOLEAN_VOTE.ABSTENTION)
        {
            if (Input.GetButtonDown(_Yes + "_" + _Character._IDJoystick))
                _CurrentChoice = Const.BOOLEAN_VOTE.YES;
            if (Input.GetButtonDown(_No + "_" + _Character._IDJoystick))
                _CurrentChoice = Const.BOOLEAN_VOTE.NO;
        }
    }

    public void SetActive(bool parState)
    {
        enabled = parState;
        _CurrentChoice = Const.BOOLEAN_VOTE.ABSTENTION;
    }

    public Const.BOOLEAN_VOTE GetCurrentChoice()
    {
        return _CurrentChoice;
    }
}
