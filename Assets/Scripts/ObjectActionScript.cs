using UnityEngine;
using System.Collections;

public abstract class ObjectActionScript : MonoBehaviour
{
    #region Public Attributes
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    private bool _IsUse;
    #endregion

    void Start()
    {
        _IsUse = false;
    }

    void Update()
    {
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
}
