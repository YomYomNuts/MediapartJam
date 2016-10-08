using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PressButtonOnSplash : MonoBehaviour
{
    public GameObject manager;
    public int ID;
    public Text textField;
    public GameObject button;

    private bool isWaiting;
    private SplashScreen splashScreen;
    private int displayedID;

    void Start()
    {
        isWaiting = true;
        splashScreen = manager.GetComponent<SplashScreen>();
        displayedID = ID + 1;

        StartCoroutine(WaitingOnSplash());
    }
    
    IEnumerator WaitingOnSplash()
    {
        while(isWaiting)
        {
            if(Input.GetButton("Fire1_"+ ID))
            {
                switch(ID)
                {
                    case 0:
                        splashScreen.isPlayer0Ready = true;
                        break;

                    case 1:
                        splashScreen.isPlayer1Ready = true;
                        break;

                    case 2:
                        splashScreen.isPlayer2Ready = true;
                        break;

                    case 3:
                        splashScreen.isPlayer3Ready = true;
                        break;
                }

                button.SetActive(false);
                textField.text = "\nJoueur " + displayedID + "\n\nPrêt!";

                isWaiting = false;
            }

            yield return null;
        }
    }
}
