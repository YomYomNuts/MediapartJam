using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ZOrderScript : MonoBehaviour
{
    #region Public Attributes
    public List<GameObject> _ObjectsToOrder;
    public int _MinZOrder;
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    #endregion

    void Start()
    {
    }

    void Update()
    {
        var newListZOrder = _ObjectsToOrder.OrderByDescending(obj => obj.transform.position.y + (obj.GetComponent<Collider2D>() != null ? obj.GetComponent<Collider2D>().offset.y : 0.0f));
        int startZOrder = _MinZOrder;
        foreach (GameObject go in newListZOrder)
        {
            SpriteRenderer sp = go.GetComponent<SpriteRenderer>();
            if (sp != null)
            {
                sp.sortingOrder = startZOrder++;
            }
        }
    }
}
