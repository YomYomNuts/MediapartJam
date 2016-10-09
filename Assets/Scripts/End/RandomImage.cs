using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RandomImage : MonoBehaviour
{
    public Image _Renderer;
    public List<Sprite> _Images;

	void Start()
    {
        int value = Random.Range(0, _Images.Count);
        _Renderer.sprite = _Images[value];
    }
	
	void Update()
    {
        if (Input.GetButtonDown("Fire1_0") || Input.GetButtonDown("Fire1_1") || Input.GetButtonDown("Fire1_2") || Input.GetButtonDown("Fire1_3"))
            SceneManager.LoadScene("Splash");
    }
}
