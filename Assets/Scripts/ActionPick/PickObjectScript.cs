using UnityEngine;
using System.Collections;

public class PickObjectScript : MonoBehaviour
{
    #region Public Attributes
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    private CharacterScript _Character;
    #endregion

    void Start()
    {
        _Character = this.GetComponent<CharacterScript>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1_" + _Character._IDJoystick))
        {
            Debug.Log("Test");
        }
    }
}
