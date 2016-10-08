using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using System.Linq;

public class ChooseVoteScript : MonoBehaviour
{
    #region Public Attributes
    public List<GameObject> _ChoiceVote;
    public List<GameObject> _VoteHUD;
    public Slider _TimeSlider;
    public float _TimerForVote;
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    private List<VoteChoiceCharacterScript> _VoteChoiceCharacters;
    private ActionVoteScript _CurrentActionVote;
    private Const.SCREEN _State;
    private float _CurrentTimerForVote;
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
        _State = Const.SCREEN.NONE;
        _TimeSlider.maxValue = _TimerForVote;
        HidePancarte();
    }
	
	void Update()
    {
        switch (_State)
        {
            case Const.SCREEN.CHOICE_VOTE:
                {
                    _CurrentTimerForVote += Time.deltaTime;
                    _TimeSlider.value = _TimerForVote - _CurrentTimerForVote;
                    bool isFinish = true;
                    if (_CurrentTimerForVote < _TimerForVote)
                    {
                        foreach (VoteChoiceCharacterScript vccs in _VoteChoiceCharacters)
                        {
                            if (vccs.GetCurrentChoice() == Const.TYPE_VOTE.NONE)
                                isFinish = false;
                        }
                    }
                    else
                    {
                        _CurrentTimerForVote = _TimerForVote;
                    }
                    if (isFinish)
                        DetermineVote();
                } break;
            case Const.SCREEN.CHOICE_MAJORITE:
                {

                } break;
            case Const.SCREEN.CHOICE_ELECTIF:
                {

                } break;
            case Const.SCREEN.RESULT:
                {

                } break;
        }
    }

    internal void ShowPancarte(ActionVoteScript parActionVote)
    {
        _CurrentActionVote = parActionVote;
        GameScript.Instance.PlayerCanAction = false;
        foreach (GameObject go in _ChoiceVote)
            go.SetActive(true);
        _CurrentActionVote.DisplayAction();

        _State = Const.SCREEN.CHOICE_VOTE;
        _TimeSlider.gameObject.SetActive(true);
        _CurrentTimerForVote = 0.0f;

        foreach (VoteChoiceCharacterScript vccs in _VoteChoiceCharacters)
        {
            vccs.SetActive(true);
        }
    }

    public void HidePancarte()
    {
        GameScript.Instance.PlayerCanAction = true;
        foreach (GameObject go in _ChoiceVote)
            go.SetActive(false);
        foreach (GameObject go in _VoteHUD)
            go.SetActive(false);
        _State = Const.SCREEN.NONE;
        foreach (VoteChoiceCharacterScript vccs in _VoteChoiceCharacters)
            vccs.SetActive(false);
    }

    void DetermineVote()
    {
        Dictionary<Const.TYPE_VOTE, int> countVote = new Dictionary<Const.TYPE_VOTE, int>();
        foreach (VoteChoiceCharacterScript vccs in _VoteChoiceCharacters)
        {
            if (!countVote.ContainsKey(vccs.GetCurrentChoice()))
                countVote.Add(vccs.GetCurrentChoice(), 0);
            ++countVote[vccs.GetCurrentChoice()];
        }

        var mapOrder = countVote.OrderByDescending(c => c.Value);
        bool equality = false;
        int previousValue = -1;
        Const.TYPE_VOTE voteSelected = Const.TYPE_VOTE.NONE;
        foreach (KeyValuePair<Const.TYPE_VOTE, int> pair in mapOrder)
        {
            if (previousValue == pair.Value)
            {
                equality = true;
                break;
            }
            if (pair.Value > previousValue)
            {
                voteSelected = pair.Key;
                previousValue = pair.Value;
            }
        }

        if (!equality)
        {
            switch (voteSelected)
            {
                case Const.TYPE_VOTE.NONE:
                    Abstention();
                    _State = Const.SCREEN.RESULT;
                    break;
                case Const.TYPE_VOTE.MAJORITE:
                    _State = Const.SCREEN.CHOICE_MAJORITE;
                    break;
                case Const.TYPE_VOTE.ALEATOIRE:
                    Aleatoire();
                    _State = Const.SCREEN.RESULT;
                    break;
                case Const.TYPE_VOTE.ALEATOIRE_PONDERE:
                    _Result = _CurrentActionVote.AleatoirePondere();
                    _State = Const.SCREEN.RESULT;
                    break;
                case Const.TYPE_VOTE.ALEATOIRE_ELECTIVE:
                    _State = Const.SCREEN.CHOICE_ELECTIF;
                    break;
            }
        }
    }

    void Abstention()
    {
        _Result = false;
    }

    void Aleatoire()
    {
        _Result = UnityEngine.Random.Range(0, 2) == 1;
    }
}
