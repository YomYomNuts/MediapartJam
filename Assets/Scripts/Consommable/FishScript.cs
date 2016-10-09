using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FishScript : MonoBehaviour
{
    #region Public Attributes
    public int _MaxStock;
    public TextMesh _Stock;
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    public int _CurrentStock;
    #endregion

    void Start()
    {
        _CurrentStock = (int)Random.Range(_MaxStock / 2.0f, _MaxStock);
        if (_Stock)
            _Stock.text = _CurrentStock.ToString();
    }

    void Update()
    {
    }

    public void AddStock(int parValue)
    {
        _CurrentStock = (int)Mathf.Clamp(_CurrentStock + parValue, 0.0f, _MaxStock);
        if (_Stock)
            _Stock.text = _CurrentStock.ToString();
    }
}
