using UnityEngine;
using System.Collections;

public class FishScript : MonoBehaviour
{
    #region Public Attributes
    public int _MaxStock;
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    public int _CurrentStock;
    #endregion

    void Start()
    {
        _CurrentStock = (int)Random.Range(_MaxStock / 2.0f, _MaxStock);
    }

    void Update()
    {
    }

    public void AddStock(int parValue)
    {
        _CurrentStock = (int)Mathf.Clamp(_CurrentStock + parValue, 0.0f, _MaxStock);
    }
}
