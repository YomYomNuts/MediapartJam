using UnityEngine;
using System.Collections;

public class VoteChoiceCharacterScript : MonoBehaviour
{
    #region Public Attributes
    public string _Majorite = "Fire4";
    public string _Aleatoire = "Fire1";
    public string _AleatoirePondere = "Fire2";
    public string _AleatoireElective = "Fire3";
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
        if (_CurrentChoice == Const.TYPE_VOTE.ABSTENTION)
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
        _CurrentChoice = Const.TYPE_VOTE.ABSTENTION;
    }

    public Const.TYPE_VOTE GetCurrentChoice()
    {
        return _CurrentChoice;
    }
}
