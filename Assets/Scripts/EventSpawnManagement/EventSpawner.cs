using UnityEngine;
using System.Collections;

public class EventSpawner : MonoBehaviour
{
    #region Public Attributes
    public GameObject _parent;

    public GameObject pf_fish;
    public GameObject pf_rock;

    public GameObject spawnPoint01;
    public GameObject spawnPoint02;
    public GameObject spawnPoint03;

    public float cdMinForFish;
    public float cdMaxForFish;
    public float cdModPerPlayerForFish;
    public float cdModPerStepForFish;

    public float cdMinForRock;
    public float cdMaxForRock;
    public float cdModPerPlayerForRock;
    public float cdModPerStepForRock;

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
        timerFish = cdMinForFish;
        timerRock = cdMinForRock;
        timerBreak = cdMinForBreak;

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

            SpawnRock();

            timerRock = Random.Range(cdMinForRock, cdMaxForRock);
        }
    }

    void SpawnRock()
    {
        Instantiate(pf_rock, spawnPoint02.transform.position, Quaternion.identity, _parent.transform);
    }
}
