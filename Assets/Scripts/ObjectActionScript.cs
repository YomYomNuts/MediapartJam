using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ObjectActionScript : MonoBehaviour
{
    #region Public Attributes
    public GameObject _HelpBulle;
    #endregion

    #region Protected Attributes
    protected CharacterScript _CharacterApplyAction;
    protected bool _IsActivate;
    #endregion

    #region Private Attributes
    private List<CharacterScript> _Characters;
    private bool _IsUse;
    #endregion

    protected virtual void Start()
    {
        _CharacterApplyAction = null;
        _IsActivate = false;
        _Characters = new List<CharacterScript>(GameObject.FindObjectsOfType<CharacterScript>());
        _IsUse = false;
    }

    protected virtual void Update()
    {
        _HelpBulle.SetActive(IsClosestForCharacter());
    }

    public void Use()
    {
        _IsUse = true;
    }
    public void UnUse()
    {
        _IsUse = false;
    }
    public bool CanBeUse()
    {
        return !_IsUse && GameScript.Instance.PlayerCanAction;
    }

    bool IsClosestForCharacter()
    {
        CharacterScript characterClosest = null;
        float distance = Mathf.Infinity;
        foreach (CharacterScript cs in _Characters)
        {
            if (cs.gameObject.activeSelf && cs.gameObject != this.transform.parent.gameObject)
            {
                float currentDistance = (this.transform.position - cs.transform.position).magnitude;
                if (currentDistance < distance)
                {
                    distance = currentDistance;
                    characterClosest = cs;
                }
            }
        }

        if (characterClosest != null)
        {
            ActionScript[] acs = characterClosest.GetComponents<ActionScript>();
            GameObject gameObjectClosest = null;
            float distanceGO = Mathf.Infinity;
            foreach (ActionScript ac in acs)
            {
                GameObject go = ac.GetClosest();
                if (go != null)
                {
                    float currentDistance = (ac.transform.position - go.transform.position).magnitude;
                    if (currentDistance < distanceGO)
                    {
                        distanceGO = currentDistance;
                        gameObjectClosest = go;
                    }
                }
            }

            if (gameObjectClosest == this.gameObject)
                return true;
        }
        return false;
    }
}
