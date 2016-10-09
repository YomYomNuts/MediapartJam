using UnityEngine;
using System.Collections;

public class FishSchoolScript : MonoBehaviour
{
    #region Public Attributes
    public float fishSpeedMin;
    public float fishSpeedMax;
    #endregion

    #region Private Attributes
    private float fishSpeed;
    #endregion

    void Start ()
    {
        Destroy(this.gameObject, 20.0f);
        fishSpeed = Random.Range(fishSpeedMin, fishSpeedMax);
	}
	
	void Update ()
    {
        transform.Translate(-fishSpeed * Time.deltaTime, 0, 0);
	}
}
