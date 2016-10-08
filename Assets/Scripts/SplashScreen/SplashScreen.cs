using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public bool isPlayer0Ready;
    public bool isPlayer1Ready;
    public bool isPlayer2Ready;
    public bool isPlayer3Ready;


    void Start ()
    {
        isPlayer0Ready = false;
        isPlayer1Ready = false;
        isPlayer2Ready = false;
        isPlayer3Ready = false;

        StartCoroutine(WaitingForPlayerInput());
    }
	
    IEnumerator WaitingForPlayerInput()
    {
        while(!isPlayer0Ready || !isPlayer1Ready || !isPlayer2Ready || !isPlayer3Ready)
        {
            yield return null;
        }

        yield return new WaitForSeconds(3.0f);

        SceneManager.LoadScene(1);
    }
}
