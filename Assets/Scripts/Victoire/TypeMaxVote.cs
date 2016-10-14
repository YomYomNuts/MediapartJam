using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TypeMaxVote : MonoBehaviour
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
        public Const.MaxVote _MaxVote;
        public string _Text;
    }
    #endregion

    void Start()
    {
        Text text = this.gameObject.GetComponent<Text>();

        Const.MaxVote maxVote = (Const.MaxVote)(PlayerPrefs.GetInt("MaxVote"));
        foreach (TextValue tv in _Texts)
        {
            if (tv._MaxVote == maxVote)
            {
                text.text = tv._Text;
                break;
            }
        }
    }
}
