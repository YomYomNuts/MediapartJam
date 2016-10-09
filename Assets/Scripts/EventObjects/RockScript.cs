using UnityEngine;
using System.Collections;

public class RockScript : MonoBehaviour
{
    #region Public Attributes
    public float speedMin;
    public float speedMax;
    #endregion

    #region Private Attributes
    private float speed;
    #endregion

    void Start ()
    {
        Destroy(this.gameObject, 20.0f);
        speed = Random.Range(speedMin, speedMax);
	}
	
	void Update ()
    {
        transform.Translate(-speed * Time.deltaTime, 0, 0);
	}
}
