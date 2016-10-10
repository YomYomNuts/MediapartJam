using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    #region Public Attributes
    public AudioClip _AudioClipError;
    public Slider _SliderGame;
    public float _TimerGame;
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    private float _CurrentTimer;
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
    private bool _PlayerCanAction = true;
    public bool PlayerCanAction
    {
        set { _PlayerCanAction = value; }
        get { return _PlayerCanAction; }
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
        return !PlayerCanAction;
    }

    void Start()
    {
        _CurrentTimer = 0.0f;
        _SliderGame.value = 0.0f;
        _SliderGame.maxValue = _TimerGame;
    }
	
	void Update()
    {
        _CurrentTimer += Time.deltaTime;
        _SliderGame.value = _CurrentTimer;
        if (_CurrentTimer > _TimerGame)
        {
            SceneManager.LoadScene("End");
        }
    }
}
