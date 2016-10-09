using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextImage : MonoBehaviour {
    public Sprite NextSprite;
    public Image _Renderer;
    private int _Count;

    void Start ()
    {
        _Count = 0;
    }
	
	void Update ()
    {
        if (Input.GetButtonDown("Fire1_0") || Input.GetButtonDown("Fire1_1") || Input.GetButtonDown("Fire1_2") || Input.GetButtonDown("Fire1_3"))
        {
            ++_Count;
            if (_Count == 1)
                _Renderer.sprite = NextSprite;
            else
                SceneManager.LoadScene("Game");
        }
    }
}
