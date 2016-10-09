using UnityEngine;
using System.Collections;

public class EventSpawner : MonoBehaviour
{
    #region Public Attributes
    public GameObject _parent;

    public GameObject pf_fish;
    public GameObject pf_rock;
    public GameObject pf_kraken;

    public GameObject spawnPoint01;
    public GameObject spawnPoint02;
    public GameObject spawnPoint03;

    public AudioSource as4_SFX_Rock;
    public AudioSource as5_SFX_Fish;

    public float cdIniForFish;
    public float cdMinForFish;
    public float cdMaxForFish;
    public float cdModPerPlayerForFish;
    public float cdModPerStepForFish;

    public float cdIniForRock;
    public float cdMinForRock;
    public float cdMaxForRock;
    public float cdModPerPlayerForRock;
    public float cdModPerStepForRock;

    public float cdIniForBreak;
    public float cdMinForBreak;
    public float cdMaxForBreak;
    public float cdModPerPlayerForBreak;
    public float cdModPerStepForBreak;
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    private float timerFish;
    private float timerRock;
    private float timerBreak;
    #endregion

    void Start ()
    {
        timerFish = cdIniForFish;
        timerRock = cdIniForRock;
        timerBreak = cdIniForBreak;

        StartCoroutine(FishGenerator());
        StartCoroutine(RockGenerator());
    }

    IEnumerator FishGenerator()
    {
        while (GameScript.Instance.isGameStillActive())
        {
            while (timerFish > Mathf.Epsilon)
            {
                if (!GameScript.Instance.IsGamePause())
                    timerFish -= Time.deltaTime;

                yield return null;
            }

            as5_SFX_Fish.Stop();
            as5_SFX_Fish.Play();

            SpawnFish();

            timerFish = Random.Range(cdMinForFish, cdMaxForFish);
        }
    }

    void SpawnFish()
    {
        switch (Random.Range(0, 2))
        {
            case 0:
                Instantiate(pf_fish, spawnPoint01.transform.position, Quaternion.identity, _parent.transform);
                break;

            case 1:
                Instantiate(pf_fish, spawnPoint03.transform.position, Quaternion.identity, _parent.transform);
                break;
        }
    }

    IEnumerator RockGenerator()
    {
        while (GameScript.Instance.isGameStillActive())
        {
            while (timerRock > Mathf.Epsilon)
            {
                if (!GameScript.Instance.IsGamePause())
                    timerRock -= Time.deltaTime;

                yield return null;
            }

            as4_SFX_Rock.Stop();
            as4_SFX_Rock.Play();

            SpawnRock();

            timerRock = Random.Range(cdMinForRock, cdMaxForRock);
        }
    }

    void SpawnRock()
    {
        switch (Random.Range(0, 4))
        {
            case 0:
                Instantiate(pf_rock, spawnPoint02.transform.position, Quaternion.identity, _parent.transform);
                break;

            case 1:
                Instantiate(pf_rock, spawnPoint02.transform.position, Quaternion.identity, _parent.transform);
                break;

            case 2:
                Instantiate(pf_rock, spawnPoint02.transform.position, Quaternion.identity, _parent.transform);
                break;

            case 3:
                Instantiate(pf_kraken, spawnPoint02.transform.position, Quaternion.identity, _parent.transform);
                break;
        }
    }
}
