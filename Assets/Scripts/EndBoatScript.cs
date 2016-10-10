using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndBoatScript : MonoBehaviour
{
    #region Public Attributes
    public float _TimerEndScreen;
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    private float _CurrentTimer;
    #endregion

    void Start()
    {
    }
	
	void Update()
    {
        _CurrentTimer += Time.deltaTime;
        if (_CurrentTimer > _TimerEndScreen)
        {
            SceneManager.LoadScene("End");
        }
    }
}
