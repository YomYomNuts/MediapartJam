﻿using UnityEngine;
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
    public string _TitleElective;
    public bool _CanSelectOwnCharacterElective;
    public Image _ImageCharacterElective;
    public List<GameObject> _ScreenElective;
    public string _TitleMajorite;
    public List<GameObject> _ScreenMajorite;
    public Text _ScoreYesMajorite;
    public Text _ScoreNoMajorite;
    public string _TitleAleatoire;
    public string _TitleAleatoirePondere;
    public string _TitleAbstention;
    public Text _ObjectResultVoteType;
    public Text _ObjectFinalResult;
    public AudioSource as1_BG_MainTheme;
    public AudioSource as2_BG_VoteSequence;
    public AudioSource as3_SFX_VoteSequence;
    public AudioClip ac_SFX_VoteSequence01;
    public AudioClip ac_SFX_VoteSequence02;
    public AudioClip ac_SFX_VoteSequence03;
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
    private bool _Result;
    private AudioSource _AudioSource;
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
                            if (vccs.gameObject.activeSelf && vccs.GetCurrentChoice() == Const.TYPE_VOTE.ABSTENTION)
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

                        _TimeSlider.gameObject.SetActive(true);
                        _CurrentTimerForVote = 0.0f;

                        // Play VoteSequence second SFX
                        as3_SFX_VoteSequence.clip = ac_SFX_VoteSequence02;
                        as3_SFX_VoteSequence.Play();
                    }

                    _CurrentTimerForVote += Time.deltaTime;
                    _TimeSlider.value = _TimerForVoteMajorite - _CurrentTimerForVote;
                    bool isFinish = true;
                    if (_CurrentTimerForVote < _TimerForVoteMajorite)
                    {
                        Dictionary<Const.BOOLEAN_VOTE, int> countVote = new Dictionary<Const.BOOLEAN_VOTE, int>();
                        foreach (VoteBooleanCharacterScript vbcs in _VoteChoiceBooleanCharacters)
                        {
                            if (vbcs.gameObject.activeSelf)
                            {
                                if (!countVote.ContainsKey(vbcs.GetCurrentChoice()))
                                    countVote.Add(vbcs.GetCurrentChoice(), 0);
                                ++countVote[vbcs.GetCurrentChoice()];
                                if (vbcs.GetCurrentChoice() == Const.BOOLEAN_VOTE.ABSTENTION)
                                    isFinish = false;
                            }
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
                        _ImageCharacterElective.gameObject.SetActive(true);
                        _ImageCharacterElective.sprite = _CharacterSelect._Face;

                        _TimeSlider.gameObject.SetActive(true);
                        _CurrentTimerForVote = 0.0f;

                        // Play VoteSequence second SFX
                        as3_SFX_VoteSequence.clip = ac_SFX_VoteSequence02;
                        as3_SFX_VoteSequence.Play();
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
                        _ObjectFinalResult.gameObject.SetActive(true);
                        _ObjectFinalResult.text = _Result ? "Oui." : "Non.";

                        _CurrentTimerForVote = 0.0f;

                        // Play VoteSequence third SFX
                        as3_SFX_VoteSequence.clip = ac_SFX_VoteSequence03;
                        as3_SFX_VoteSequence.Play();
                    }

                    _CurrentTimerForVote += Time.deltaTime;
                    _TimeSlider.value = _TimerForResult - _CurrentTimerForVote;
                    if (_CurrentTimerForVote >= _TimerForResult)
                    {
                        HidePancarte();

                        as2_BG_VoteSequence.Stop();
                        as1_BG_MainTheme.Play();

                        if (_Result)
                            _CurrentActionVote.ValidateAction();
                        else
                            _CurrentActionVote.EndAction();
                    }
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

        foreach (VoteBooleanCharacterScript vbcs in _VoteChoiceBooleanCharacters)
        {
            if (vbcs.gameObject.activeSelf)
                vbcs.SetActive(false);
        }
        foreach (VoteChoiceCharacterScript vccs in _VoteChoiceCharacters)
        {
            if (vccs.gameObject.activeSelf)
                vccs.SetActive(true);
        }

        // Pause MainTheme
        as1_BG_MainTheme.Pause();

        // Launch VoteSequenceLoop
        as2_BG_VoteSequence.Stop();
        as2_BG_VoteSequence.Play();

        // Play VoteSequence first SFX
        as3_SFX_VoteSequence.clip = ac_SFX_VoteSequence01;
        as3_SFX_VoteSequence.Play();
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
        {
            if (vccs.gameObject.activeSelf)
                vccs.SetActive(false);
        }
        foreach (VoteBooleanCharacterScript vbcs in _VoteChoiceBooleanCharacters)
        {
            if (vbcs.gameObject.activeSelf)
                vbcs.SetActive(false);
        }
    }

    void DetermineVote()
    {
        Dictionary<Const.TYPE_VOTE, int> countVote = new Dictionary<Const.TYPE_VOTE, int>();
        foreach (VoteChoiceCharacterScript vccs in _VoteChoiceCharacters)
        {
            if (vccs.gameObject.activeSelf)
            {
                if (!countVote.ContainsKey(vccs.GetCurrentChoice()))
                    countVote.Add(vccs.GetCurrentChoice(), 0);
                ++countVote[vccs.GetCurrentChoice()];
                vccs.SetActive(false);
            }
        }

        var mapOrder = countVote.OrderByDescending(c => c.Value);
        int previousValue = -1;
        List<Const.TYPE_VOTE> resultType = new List<Const.TYPE_VOTE>();
        resultType.Add(Const.TYPE_VOTE.ABSTENTION);
        foreach (KeyValuePair<Const.TYPE_VOTE, int> pair in mapOrder)
        {
            if (pair.Key != Const.TYPE_VOTE.ABSTENTION && pair.Value > previousValue)
            {
                if (previousValue != pair.Value)
                    resultType.Clear();
                resultType.Add(pair.Key);
                previousValue = pair.Value;
            }
        }

        if (resultType.Count > 1)
        {
            bool aleatoireAndPondere = true;
            bool majoriteAndElectif = true;

            foreach (Const.TYPE_VOTE tp in resultType)
            {
                if (!(tp == Const.TYPE_VOTE.ALEATOIRE || tp == Const.TYPE_VOTE.ALEATOIRE_PONDERE))
                    aleatoireAndPondere = false;
                if (!(tp == Const.TYPE_VOTE.MAJORITE || tp == Const.TYPE_VOTE.ALEATOIRE_ELECTIVE))
                    majoriteAndElectif = false;
            }

            Const.MaxVote mx = Const.MaxVote.EGALITE_AUTRES_COMBINAISONS;
            if (aleatoireAndPondere)
                mx = Const.MaxVote.EGALITE_ALEATOIRE_PONDERE;
            else if (majoriteAndElectif)
                mx = Const.MaxVote.EGALITE_MAJORITAIRE_ELECTIF;

            if (!GameScript.Instance._NbToTalVote.ContainsKey(mx))
                GameScript.Instance._NbToTalVote.Add(mx, 0);
            ++GameScript.Instance._NbToTalVote[mx];

            while (resultType.Count > 1)
            {
                int index = UnityEngine.Random.Range(0, resultType.Count);
                resultType.RemoveAt(index);
            }
        }
        else
        {
            Const.MaxVote mx = (Const.MaxVote)((int)resultType[0]);
            if (!GameScript.Instance._NbToTalVote.ContainsKey(mx))
                GameScript.Instance._NbToTalVote.Add(mx, 0);
            ++GameScript.Instance._NbToTalVote[mx];
        }

        string title = _TitleAbstention;
        switch (resultType[0])
        {
            case Const.TYPE_VOTE.ABSTENTION:
                title = _TitleAbstention;
                _Result = Abstention();
                _State = Const.SCREEN.RESULT;
                break;
            case Const.TYPE_VOTE.MAJORITE:
                title = _TitleMajorite;
                ActiveMajorite();
                _State = Const.SCREEN.CHOICE_MAJORITE;
                break;
            case Const.TYPE_VOTE.ALEATOIRE:
                title = _TitleAleatoire;
                _Result = Aleatoire();
                _State = Const.SCREEN.RESULT;
                break;
            case Const.TYPE_VOTE.ALEATOIRE_PONDERE:
                title = _TitleAleatoirePondere;
                _Result = _CurrentActionVote.AleatoirePondere();
                _State = Const.SCREEN.RESULT;
                break;
            case Const.TYPE_VOTE.ALEATOIRE_ELECTIVE:
                title = _TitleElective;
                AleatoireElective();
                _State = Const.SCREEN.CHOICE_ELECTIF;
                break;
        }
        _ObjectResultVoteType.text = title;
    }

    bool Abstention()
    {
        return false;
    }

    void ActiveMajorite()
    {
        foreach (VoteBooleanCharacterScript vbcs in _VoteChoiceBooleanCharacters)
        {
            if (vbcs.gameObject.activeSelf)
                vbcs.SetActive(true);
        }
    }

    void EndChoiceMajorite()
    {
        Dictionary<Const.BOOLEAN_VOTE, int> countVote = new Dictionary<Const.BOOLEAN_VOTE, int>();
        foreach (VoteBooleanCharacterScript vbcs in _VoteChoiceBooleanCharacters)
        {
            if (vbcs.gameObject.activeSelf)
            {
                if (!countVote.ContainsKey(vbcs.GetCurrentChoice()))
                    countVote.Add(vbcs.GetCurrentChoice(), 0);
                ++countVote[vbcs.GetCurrentChoice()];
                vbcs.SetActive(false);
            }
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
        List<VoteChoiceCharacterScript> voteChoiceCharacter = _VoteChoiceCharacters.Where(obj => obj.gameObject.activeSelf).ToList();
        int index = UnityEngine.Random.Range(0, voteChoiceCharacter.Count);
        _CharacterSelect = voteChoiceCharacter[index].GetComponent<CharacterScript>();
        if (!_CanSelectOwnCharacterElective && _CurrentActionVote.gameObject == _CharacterSelect.gameObject)
        {
            index = (index + 1) % voteChoiceCharacter.Count;
            _CharacterSelect = voteChoiceCharacter[index].GetComponent<CharacterScript>();
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
