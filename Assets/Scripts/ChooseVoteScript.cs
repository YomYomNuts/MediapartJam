using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using System.Linq;

public class ChooseVoteScript : MonoBehaviour
{
    #region Public Attributes
    public GameObject _Background;
    public Slider _TimeSlider;
    public float _TimerForVote;
    public float _TimerForVoteElective;
    public float _TimerForVoteMajorite;
    public float _TimerForResult;
    public List<GameObject> _ChoiceVote;
    public List<GameObject> _VoteHUD;
    public bool _CanSelectOwnCharacterElective;
    public Image _ImageCharacterElective;
    public List<GameObject> _ScreenElective;
    public List<GameObject> _ScreenMajorite;
    public Text _ScoreYesMajorite;
    public Text _ScoreNoMajorite;
    public Text _ObjectResultVoteType;
    public Text _ObjectFinalResult;
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    private List<VoteChoiceCharacterScript> _VoteChoiceCharacters;
    private List<VoteBooleanCharacterScript> _VoteChoiceBooleanCharacters;
    private ActionVoteScript _CurrentActionVote;
    private Const.SCREEN _State;
    private Const.SCREEN _PreviousState;
    private float _CurrentTimerForVote;
    private CharacterScript _CharacterSelect;
    private Const.TYPE_VOTE _ResultType;
    private bool _Result;
    #endregion

    #region Static Attributs
    private static ChooseVoteScript _Instance;
    public static ChooseVoteScript Instance
    {
        get
        {
            if (ChooseVoteScript._Instance == null)
                ChooseVoteScript._Instance = new ChooseVoteScript();
            return ChooseVoteScript._Instance;
        }
    }
    #endregion

    void Awake()
    {
        if (ChooseVoteScript._Instance == null)
            ChooseVoteScript._Instance = this;
        else if (ChooseVoteScript._Instance != this)
            Destroy(this.gameObject);
    }

    void Start()
    {
        _VoteChoiceCharacters = new List<VoteChoiceCharacterScript>(GameObject.FindObjectsOfType<VoteChoiceCharacterScript>());
        _VoteChoiceBooleanCharacters = new List<VoteBooleanCharacterScript>(GameObject.FindObjectsOfType<VoteBooleanCharacterScript>());
        _State = Const.SCREEN.NONE;
        _TimeSlider.maxValue = _TimerForVote;
        HidePancarte();
    }
	
	void Update()
    {
        Const.SCREEN currentState = _State;
        switch (currentState)
        {
            case Const.SCREEN.CHOICE_VOTE:
                {
                    if (_PreviousState != _State)
                    {
                        _TimeSlider.gameObject.SetActive(true);
                        _CurrentTimerForVote = 0.0f;
                    }

                    _CurrentTimerForVote += Time.deltaTime;
                    _TimeSlider.value = _TimerForVote - _CurrentTimerForVote;
                    bool isFinish = true;
                    if (_CurrentTimerForVote < _TimerForVote)
                    {
                        foreach (VoteChoiceCharacterScript vccs in _VoteChoiceCharacters)
                        {
                            if (vccs.GetCurrentChoice() == Const.TYPE_VOTE.ABSTENTION)
                                isFinish = false;
                        }
                    }
                    else
                        _CurrentTimerForVote = _TimerForVote;
                    if (isFinish)
                        DetermineVote();
                } break;
            case Const.SCREEN.CHOICE_MAJORITE:
                {
                    if (_PreviousState != _State)
                    {
                        foreach (GameObject go in _ChoiceVote)
                            go.SetActive(false);
                        foreach (GameObject go in _ScreenElective)
                            go.SetActive(false);
                        foreach (GameObject go in _ScreenMajorite)
                            go.SetActive(true);

                        _ObjectResultVoteType.gameObject.SetActive(true);
                        _ObjectResultVoteType.text = _ResultType.ToString();

                        _TimeSlider.gameObject.SetActive(true);
                        _CurrentTimerForVote = 0.0f;
                    }

                    _CurrentTimerForVote += Time.deltaTime;
                    _TimeSlider.value = _TimerForVoteMajorite - _CurrentTimerForVote;
                    bool isFinish = true;
                    if (_CurrentTimerForVote < _TimerForVoteMajorite)
                    {
                        Dictionary<Const.BOOLEAN_VOTE, int> countVote = new Dictionary<Const.BOOLEAN_VOTE, int>();
                        foreach (VoteBooleanCharacterScript vbcs in _VoteChoiceBooleanCharacters)
                        {
                            if (!countVote.ContainsKey(vbcs.GetCurrentChoice()))
                                countVote.Add(vbcs.GetCurrentChoice(), 0);
                            ++countVote[vbcs.GetCurrentChoice()];
                            if (vbcs.GetCurrentChoice() == Const.BOOLEAN_VOTE.ABSTENTION)
                                isFinish = false;
                        }
                        _ScoreYesMajorite.text = countVote.ContainsKey(Const.BOOLEAN_VOTE.YES) ? countVote[Const.BOOLEAN_VOTE.YES].ToString() : "0";
                        _ScoreNoMajorite.text = countVote.ContainsKey(Const.BOOLEAN_VOTE.NO) ? countVote[Const.BOOLEAN_VOTE.NO].ToString() : "0";
                    }
                    else
                        _CurrentTimerForVote = _TimerForVoteMajorite;
                    if (isFinish)
                        EndChoiceMajorite();
                } break;
            case Const.SCREEN.CHOICE_ELECTIF:
                {
                    if (_PreviousState != _State)
                    {
                        foreach (GameObject go in _ChoiceVote)
                            go.SetActive(false);
                        foreach (GameObject go in _ScreenElective)
                            go.SetActive(true);

                        _ObjectResultVoteType.gameObject.SetActive(true);
                        _ObjectResultVoteType.text = _ResultType.ToString();
                        _ImageCharacterElective.gameObject.SetActive(true);
                        _ImageCharacterElective.sprite = _CharacterSelect._Face;

                        _TimeSlider.gameObject.SetActive(true);
                        _CurrentTimerForVote = 0.0f;
                    }

                    _CurrentTimerForVote += Time.deltaTime;
                    _TimeSlider.value = _TimerForVoteElective - _CurrentTimerForVote;
                    bool isFinish = true;
                    if (_CurrentTimerForVote < _TimerForVoteElective)
                    {
                        if (_CharacterSelect.GetComponent<VoteBooleanCharacterScript>().GetCurrentChoice() == Const.BOOLEAN_VOTE.ABSTENTION)
                            isFinish = false;
                    }
                    else
                        _CurrentTimerForVote = _TimerForVoteElective;
                    if (isFinish)
                        EndChoiceElective();
                } break;
            case Const.SCREEN.RESULT:
                {
                    if (_PreviousState != _State)
                    {
                        foreach (GameObject go in _ChoiceVote)
                            go.SetActive(false);
                        foreach (GameObject go in _ScreenElective)
                            go.SetActive(false);
                        foreach (GameObject go in _ScreenMajorite)
                            go.SetActive(false);

                        _ObjectResultVoteType.gameObject.SetActive(true);
                        _ObjectResultVoteType.text = _ResultType.ToString();
                        _ObjectFinalResult.gameObject.SetActive(true);
                        _ObjectFinalResult.text = _Result.ToString();

                        _CurrentTimerForVote = 0.0f;
                    }

                    _CurrentTimerForVote += Time.deltaTime;
                    _TimeSlider.value = _TimerForResult - _CurrentTimerForVote;
                    if (_CurrentTimerForVote >= _TimerForResult)
                        HidePancarte();
                } break;
        }
        _PreviousState = currentState;
    }

    public void ShowPancarte(ActionVoteScript parActionVote)
    {
        _CurrentActionVote = parActionVote;
        _CurrentActionVote.DisplayAction();
        GameScript.Instance.PlayerCanAction = false;
        _Background.SetActive(true);
        foreach (GameObject go in _ChoiceVote)
            go.SetActive(true);
        foreach (GameObject go in _ScreenElective)
            go.SetActive(false);

        _State = Const.SCREEN.CHOICE_VOTE;

        foreach (VoteChoiceCharacterScript vccs in _VoteChoiceCharacters)
            vccs.SetActive(true);
        foreach (VoteBooleanCharacterScript vbcs in _VoteChoiceBooleanCharacters)
            vbcs.SetActive(false);
    }

    public void HidePancarte()
    {
        GameScript.Instance.PlayerCanAction = true;
        _Background.SetActive(false);
        foreach (GameObject go in _ChoiceVote)
            go.SetActive(false);
        foreach (GameObject go in _VoteHUD)
            go.SetActive(false);
        foreach (GameObject go in _ScreenElective)
            go.SetActive(false);
        foreach (GameObject go in _ScreenMajorite)
            go.SetActive(false);

        _State = Const.SCREEN.NONE;

        foreach (VoteChoiceCharacterScript vccs in _VoteChoiceCharacters)
            vccs.SetActive(false);
        foreach (VoteBooleanCharacterScript vbcs in _VoteChoiceBooleanCharacters)
            vbcs.SetActive(false);
    }

    void DetermineVote()
    {
        Dictionary<Const.TYPE_VOTE, int> countVote = new Dictionary<Const.TYPE_VOTE, int>();
        foreach (VoteChoiceCharacterScript vccs in _VoteChoiceCharacters)
        {
            if (!countVote.ContainsKey(vccs.GetCurrentChoice()))
                countVote.Add(vccs.GetCurrentChoice(), 0);
            ++countVote[vccs.GetCurrentChoice()];
            vccs.SetActive(false);
        }

        var mapOrder = countVote.OrderByDescending(c => c.Value);
        bool equality = false;
        int previousValue = -1;
        _ResultType = Const.TYPE_VOTE.ABSTENTION;
        foreach (KeyValuePair<Const.TYPE_VOTE, int> pair in mapOrder)
        {
            if (previousValue == pair.Value)
            {
                equality = true;
                break;
            }
            if (pair.Key != Const.TYPE_VOTE.ABSTENTION && pair.Value > previousValue)
            {
                _ResultType = pair.Key;
                previousValue = pair.Value;
            }
        }

        if (!equality)
        {
            switch (_ResultType)
            {
                case Const.TYPE_VOTE.ABSTENTION:
                    _Result = Abstention();
                    _State = Const.SCREEN.RESULT;
                    break;
                case Const.TYPE_VOTE.MAJORITE:
                    ActiveMajorite();
                    _State = Const.SCREEN.CHOICE_MAJORITE;
                    break;
                case Const.TYPE_VOTE.ALEATOIRE:
                    _Result = Aleatoire();
                    _State = Const.SCREEN.RESULT;
                    break;
                case Const.TYPE_VOTE.ALEATOIRE_PONDERE:
                    _Result = _CurrentActionVote.AleatoirePondere();
                    _State = Const.SCREEN.RESULT;
                    break;
                case Const.TYPE_VOTE.ALEATOIRE_ELECTIVE:
                    AleatoireElective();
                    _State = Const.SCREEN.CHOICE_ELECTIF;
                    break;
            }
        }
    }

    bool Abstention()
    {
        return false;
    }

    void ActiveMajorite()
    {
        foreach (VoteBooleanCharacterScript vbcs in _VoteChoiceBooleanCharacters)
            vbcs.SetActive(true);
    }

    void EndChoiceMajorite()
    {
        Dictionary<Const.BOOLEAN_VOTE, int> countVote = new Dictionary<Const.BOOLEAN_VOTE, int>();
        foreach (VoteBooleanCharacterScript vbcs in _VoteChoiceBooleanCharacters)
        {
            if (!countVote.ContainsKey(vbcs.GetCurrentChoice()))
                countVote.Add(vbcs.GetCurrentChoice(), 0);
            ++countVote[vbcs.GetCurrentChoice()];
            vbcs.SetActive(false);
        }
        _ScoreYesMajorite.text = countVote.ContainsKey(Const.BOOLEAN_VOTE.YES) ? countVote[Const.BOOLEAN_VOTE.YES].ToString() : "0";
        _ScoreNoMajorite.text = countVote.ContainsKey(Const.BOOLEAN_VOTE.NO) ? countVote[Const.BOOLEAN_VOTE.NO].ToString() : "0";

        var mapOrder = countVote.OrderByDescending(c => c.Value);
        bool equality = false;
        int previousValue = -1;
        Const.BOOLEAN_VOTE resultType = Const.BOOLEAN_VOTE.ABSTENTION;
        foreach (KeyValuePair<Const.BOOLEAN_VOTE, int> pair in mapOrder)
        {
            if (previousValue == pair.Value)
            {
                equality = true;
                break;
            }
            if (pair.Key != Const.BOOLEAN_VOTE.ABSTENTION && pair.Value > previousValue)
            {
                resultType = pair.Key;
                previousValue = pair.Value;
            }
        }

        if (!equality)
        {
            _Result = (resultType == Const.BOOLEAN_VOTE.YES);
            _State = Const.SCREEN.RESULT;
        }
    }

    bool Aleatoire()
    {
        return UnityEngine.Random.Range(0, 2) == 1;
    }

    void AleatoireElective()
    {
        int index = UnityEngine.Random.Range(0, _VoteChoiceCharacters.Count);
        _CharacterSelect = _VoteChoiceCharacters[index].GetComponent<CharacterScript>();
        if (!_CanSelectOwnCharacterElective && _CurrentActionVote.gameObject == _CharacterSelect.gameObject)
        {
            index = (index + 1) % _VoteChoiceCharacters.Count;
            _CharacterSelect = _VoteChoiceCharacters[index].GetComponent<CharacterScript>();
        }
        _CharacterSelect.GetComponent<VoteBooleanCharacterScript>().SetActive(true);
    }

    void EndChoiceElective()
    {
        Const.BOOLEAN_VOTE choice = _CharacterSelect.GetComponent<VoteBooleanCharacterScript>().GetCurrentChoice();
        _Result = (choice == Const.BOOLEAN_VOTE.YES);
        _State = Const.SCREEN.RESULT;
    }
}
