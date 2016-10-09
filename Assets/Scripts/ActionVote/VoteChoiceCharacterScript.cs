using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VoteChoiceCharacterScript : MonoBehaviour
{
    #region Public Attributes
    public string _Majorite = "Fire4";
    public string _Aleatoire = "Fire1";
    public string _AleatoirePondere = "Fire2";
    public string _AleatoireElective = "Fire3";
    public Image _EmplacementBulle;
    public Sprite _SpriteWait;
    public Sprite _SpriteMajorite;
    public Sprite _SpriteAleatoire;
    public Sprite _SpriteAleatoirePondere;
    public Sprite _SpriteAleatoireElective;
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
            {
                _CurrentChoice = Const.TYPE_VOTE.MAJORITE;
                _EmplacementBulle.sprite = _SpriteMajorite;
            }
            if (Input.GetButtonDown(_Aleatoire + "_" + _Character._IDJoystick))
            {
                _CurrentChoice = Const.TYPE_VOTE.ALEATOIRE;
                _EmplacementBulle.sprite = _SpriteAleatoire;
            }
            if (Input.GetButtonDown(_AleatoirePondere + "_" + _Character._IDJoystick))
            {
                _CurrentChoice = Const.TYPE_VOTE.ALEATOIRE_PONDERE;
                _EmplacementBulle.sprite = _SpriteAleatoirePondere;
            }
            if (Input.GetButtonDown(_AleatoireElective + "_" + _Character._IDJoystick))
            {
                _CurrentChoice = Const.TYPE_VOTE.ALEATOIRE_ELECTIVE;
                _EmplacementBulle.sprite = _SpriteAleatoireElective;
            }
        }
    }

    public void SetActive(bool parState)
    {
        enabled = parState;
        _CurrentChoice = Const.TYPE_VOTE.ABSTENTION;
        _EmplacementBulle.gameObject.SetActive(parState);
        _EmplacementBulle.sprite = _SpriteWait;
    }

    public Const.TYPE_VOTE GetCurrentChoice()
    {
        return _CurrentChoice;
    }
}
