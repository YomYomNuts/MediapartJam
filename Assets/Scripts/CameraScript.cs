using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour
{
    #region Public Attributs
    public int Width = 1920;
    public int Height = 1080;
    #endregion

    #region Private Attributs
    #endregion

    void Start ()
    {
    }

    void Update ()
    {
    }

    void Awake()
    {
        Screen.SetResolution(this.Width, this.Height, true);
    }
}
