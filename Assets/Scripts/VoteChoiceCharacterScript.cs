using UnityEngine;
using System.Collections;

public class VoteChoiceCharacterScript : MonoBehaviour
{
    #region Public Attributes
    public string _Majorite = "Fire1";
    public string _Aleatoire = "Fire2";
    public string _AleatoirePondere = "Fire3";
    public string _AleatoireElective = "Fire4";
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    private CharacterScript _Character;
    private Const.TYPE_VOTE _CurrentChoice;
    #endregion

    void Start ()
    {
        _Character = this.GetComponent<CharacterScript>();
    }
	
	void Update ()
    {
        if (_CurrentChoice == Const.TYPE_VOTE.NONE)
        {
            if (Input.GetButtonDown(_Majorite + "_" + _Character._IDJoystick))
                _CurrentChoice = Const.TYPE_VOTE.MAJORITE;
            if (Input.GetButtonDown(_Aleatoire + "_" + _Character._IDJoystick))
                _CurrentChoice = Const.TYPE_VOTE.ALEATOIRE;
            if (Input.GetButtonDown(_AleatoirePondere + "_" + _Character._IDJoystick))
                _CurrentChoice = Const.TYPE_VOTE.ALEATOIRE_PONDERE;
            if (Input.GetButtonDown(_AleatoireElective + "_" + _Character._IDJoystick))
                _CurrentChoice = Const.TYPE_VOTE.ALEATOIRE_ELECTIVE;
        }
    }

    public void SetActive(bool parState)
    {
        enabled = parState;
        _CurrentChoice = Const.TYPE_VOTE.NONE;
    }

    public Const.TYPE_VOTE GetCurrentChoice()
    {
        return _CurrentChoice;
    }
}
