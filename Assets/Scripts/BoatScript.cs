using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoatScript : MonoBehaviour
{
    #region Public Attributes
    public List<GameObject> _ListRepairZone;
    public AudioClip _AudioClipBreak;
    public AudioClip _AudioClipImpact;
    public Vector2 _RangeUsure;
    public GameObject _SpriteEndBoat;
    public List<GameObject> _ObjectsDisable;
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    private List<GameObject> _CleanZones;
    private AudioSource _AudioSource;
    private float _CurrentTimerUsure;
    private float _TimerLaunchUsure;
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
        _AudioSource = this.GetComponent<AudioSource>();
        _CurrentTimerUsure = 0.0f;
        _TimerLaunchUsure = Random.Range(_RangeUsure.x, _RangeUsure.y);
    }

    void Update()
    {
        if (!GameScript.Instance.IsGamePause())
        {
            _CurrentTimerUsure += Time.deltaTime;
            if (_CurrentTimerUsure > _TimerLaunchUsure)
            {
                if (_CleanZones.Count > 0)
                {
                    int index = Random.Range(0, _CleanZones.Count);
                    _CleanZones[index].SetActive(true);
                    _CleanZones.RemoveAt(index);
                }
                _AudioSource.Stop();
                _AudioSource.clip = _AudioClipBreak;
                _AudioSource.Play();

                _CurrentTimerUsure = 0.0f;
                _TimerLaunchUsure = Random.Range(_RangeUsure.x, _RangeUsure.y);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D parCollider)
    {
        if (parCollider.gameObject.layer == Const.LAYER_ISLAND)
        {
            _AudioSource.Stop();
            _AudioSource.clip = _AudioClipImpact;
            _AudioSource.Play();
            Destroy(parCollider.gameObject);

            _SpriteEndBoat.SetActive(true);
            foreach (GameObject go in _ObjectsDisable)
                go.SetActive(false);
            GameScript.Instance._IsActive = true;
        }
    }

    public void RemoveDamage(GameObject parZone)
    {
        parZone.SetActive(false);
        _CleanZones.Add(parZone);
    }
}
