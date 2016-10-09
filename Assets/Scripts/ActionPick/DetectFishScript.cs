using UnityEngine;
using System.Collections;

public class DetectFishScript : MonoBehaviour
{
    #region Public Attributes
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    private int _NumberBandFish;
    #endregion

    void Start ()
    {
        _NumberBandFish = 0;
    }
	
	void Update ()
    {
    }

    void OnTriggerEnter2D(Collider2D parCollider)
    {
        if (parCollider.gameObject.layer == Const.LAYER_BANDFISH)
            ++_NumberBandFish;
    }
    void OnTriggerExit2D(Collider2D parCollider)
    {
        if (parCollider.gameObject.layer == Const.LAYER_BANDFISH)
            --_NumberBandFish;
    }

    public bool CanCatchFish()
    {
        return _NumberBandFish > 0;
    }
}
