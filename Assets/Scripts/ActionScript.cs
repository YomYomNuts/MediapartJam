using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionScript : MonoBehaviour
{
    #region Public Attributes
    public Const.LAYER_ACTION_VOTE _LayerObjectAction;
    #endregion

    #region Protected Attributes
    protected CharacterScript _Character;
    protected List<GameObject> _ObjectsCollide;
    protected AudioSource _AudioSource;
    #endregion

    #region Private Attributes
    #endregion

    protected virtual void Start()
    {
        _Character = this.GetComponent<CharacterScript>();
        _ObjectsCollide = new List<GameObject>();
        _AudioSource = this.GetComponent<AudioSource>();
    }

    protected virtual void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D parCollider)
    {
        if (parCollider.gameObject.layer == (int)_LayerObjectAction)
        {
            if (!_ObjectsCollide.Contains(parCollider.gameObject) && parCollider.GetComponent<ObjectActionScript>())
                _ObjectsCollide.Add(parCollider.gameObject);
        }
    }
    protected virtual void OnTriggerStay2D(Collider2D parCollider)
    {
    }
    protected virtual void OnTriggerExit2D(Collider2D parCollider)
    {
        if (parCollider.gameObject.layer == (int)_LayerObjectAction)
            _ObjectsCollide.Remove(parCollider.gameObject);
    }

    public GameObject GetClosest()
    {
        GameObject objectAction = null;
        float distance = Mathf.Infinity;
        foreach (GameObject go in _ObjectsCollide)
        {
            if (go != this.transform.parent.gameObject)
            {
                float currentDistance = (this.transform.position - go.transform.position).magnitude;
                if (currentDistance < distance)
                {
                    distance = currentDistance;
                    objectAction = go;
                }
            }
        }
        return objectAction;
    }
}
