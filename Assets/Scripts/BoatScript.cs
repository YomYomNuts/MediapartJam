using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoatScript : MonoBehaviour
{
    #region Public Attributes
    public List<GameObject> _ListRepairZone;
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    private List<GameObject> _CleanZones;
    #endregion

    #region Static Attributs
    private static BoatScript _Instance;
    public static BoatScript Instance
    {
        get
        {
            if (BoatScript._Instance == null)
                BoatScript._Instance = new BoatScript();
            return BoatScript._Instance;
        }
    }
    #endregion

    void Awake()
    {
        if (BoatScript._Instance == null)
            BoatScript._Instance = this;
        else if (BoatScript._Instance != this)
            Destroy(this.gameObject);
    }

    void Start()
    {
        _CleanZones = _ListRepairZone;
    }

    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D parCollider)
    {
        if (parCollider.gameObject.layer == Const.LAYER_ISLAND)
        {
            AddDamage(parCollider.gameObject);
        }
    }

    public void AddDamage(GameObject parZone)
    {
        if (_CleanZones.Count > 0)
        {
            int index = Random.Range(0, _CleanZones.Count);
            _CleanZones[index].SetActive(true);
            _CleanZones.RemoveAt(index);
            Destroy(parZone);
        }
    }

    public void RemoveDamage(GameObject parZone)
    {
        parZone.SetActive(false);
        _CleanZones.Add(parZone);
    }
}
