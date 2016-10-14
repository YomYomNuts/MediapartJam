using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayAbstention : MonoBehaviour
{
    #region Public Attributes
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    #endregion

    void Start()
    {
        Text text = this.gameObject.GetComponent<Text>();

        bool abstenstion = PlayerPrefs.GetInt("Abstention") == 1;

        text.enabled = abstenstion;
    }
}
