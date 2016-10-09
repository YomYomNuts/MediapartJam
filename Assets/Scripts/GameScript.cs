using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{
    #region Public Attributes
    public AudioClip _AudioClipError;
    public float _TimerEndScreen;
    [HideInInspector]
    public bool _IsActive;
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
        _IsActive = false;
    }
	
	void Update()
    {
        if (_IsActive)
        {
            _CurrentTimer += Time.deltaTime;
            if (_CurrentTimer > _TimerEndScreen)
            {
                SceneManager.LoadScene("End");
            }
        }
    }
}
