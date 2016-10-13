using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    #region Public Attributes
    public AudioClip _AudioClipError;
    public Slider _SliderGame;
    public float _TimerGame;
    public GameObject _BoatRender;
    public GameObject _Passerelle;
    public float _TimerStart;
    public GameObject _Phare;
    public float _TimeEnd;
    public List<GameObject> _ObjectsDeasactivateOnEnd;
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    private float _CurrentTimer;
    private bool _GameIsStart;
    #endregion

    #region Static Attributs
    private static GameScript _Instance;
    public static GameScript Instance
    {
        get
        {
            if (GameScript._Instance == null)
                GameScript._Instance = new GameScript();
            return GameScript._Instance;
        }
    }
    #endregion

    #region Properties
    private bool _PlayerCanAction;
    public bool PlayerCanAction
    {
        set { _PlayerCanAction = value; }
        get { return _PlayerCanAction; }
    }
    private bool _PlayerCanDoAction;
    public bool PlayerCanDoAction
    {
        set { _PlayerCanDoAction = value; }
        get { return _PlayerCanDoAction; }
    }
    #endregion

    void Awake()
    {
        if (GameScript._Instance == null)
            GameScript._Instance = this;
        else if (GameScript._Instance != this)
            Destroy(this.gameObject);
    }

    public bool isGameStillActive()
    {
        return true;
    }

    public bool IsGamePause()
    {
        return !PlayerCanAction || !_GameIsStart;
    }

    void Start()
    {
        _CurrentTimer = 0.0f;
        _SliderGame.value = 0.0f;
        _SliderGame.maxValue = _TimerGame;
        _GameIsStart = false;
        _PlayerCanAction = false;
        _PlayerCanDoAction = false;
        StartCoroutine(StartGame());
    }
	
	void Update()
    {
        if (PlayerCanAction)
        {
            if (_CurrentTimer < _TimerGame)
            {
                _CurrentTimer += Time.deltaTime;
                _SliderGame.value = _CurrentTimer;
                if (_CurrentTimer >= _TimerGame)
                {
                    _CurrentTimer = _TimerGame;
                    _SliderGame.value = _CurrentTimer;
                    _PlayerCanDoAction = false;
                    StartCoroutine(LaunchEnd());
                }
            }
        }
    }

    IEnumerator LaunchEnd()
    {
        foreach (GameObject go in _ObjectsDeasactivateOnEnd)
            go.SetActive(false);

        ObjectMoving om = _Phare.GetComponent<ObjectMoving>();
        om.enabled = true;

        while (om.transform.position != om._Goal.transform.position)
            yield return 0.0f;

        float time = 0.0f;
        while (time < _TimeEnd)
        {
            time += Time.deltaTime;
            yield return 0.0f;
        }

        SceneManager.LoadScene("EndVictoire");
    }

    IEnumerator StartGame()
    {
        foreach (GameObject go in _ObjectsDeasactivateOnEnd)
            go.SetActive(false);

        WayPointsScript[] wpss = FindObjectsOfType<WayPointsScript>();
        foreach (WayPointsScript wps in wpss)
        {
            wps.enabled = true;
            wps.GetComponent<Collider2D>().enabled = false;
        }

        bool goalDone = false;
        while (!goalDone)
        {
            goalDone = true;
            foreach (WayPointsScript wps in wpss)
            {
                if (!wps.GoalDone())
                {
                    goalDone = false;
                    break;
                }
            }

            yield return 0.0f;
        }

        foreach (WayPointsScript wps in wpss)
        {
            wps.enabled = false;
            wps.GetComponent<Collider2D>().enabled = true;
        }

        _GameIsStart = true;
        _PlayerCanAction = true;
        _Passerelle.GetComponent<ObjectMoving>().enabled = true;
        ObjectMoving omBoat = _BoatRender.GetComponent<ObjectMoving>();
        omBoat.enabled = true;

        while (omBoat.enabled)
            yield return 0.0f;

        foreach (GameObject go in _ObjectsDeasactivateOnEnd)
            go.SetActive(true);

        _PlayerCanDoAction = true;
    }
}
