using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class NbPlayers : MonoBehaviour
{
    #region Public Attributes
    public List<TextValue> _Texts;
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    #endregion

    #region Public Classes
    [System.Serializable]
    public class TextValue
    {
        public int _NumberPlayer;
        public string _Text;
    }
    #endregion

    void Start()
    {
        Text text = this.gameObject.GetComponent<Text>();

        int nbALive = PlayerPrefs.GetInt("NbAlive");
        foreach (TextValue tv in _Texts)
        {
            if (tv._NumberPlayer == nbALive)
            {
                text.text = tv._Text;
                break;
            }
        }
    }
}
