using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour
{
    #region Public Attributes
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
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

    void Start()
    {
    }
	
	void Update()
    {
    }
}
