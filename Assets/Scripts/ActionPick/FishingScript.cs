using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FishingScript : ObjectActionPickScript
{
    #region Public Attributes
    public Vector2 _RangeFishToAdd;
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    private FishScript _FishScript;
    #endregion

    protected override void Start()
    {
        base.Start();
        _FishScript = GameScript.Instance.GetComponent<FishScript>();
    }

    protected override void Update()
    {
        base.Update();
        if (_IsActivate)
        {
            if (!_CharacterApplyAction._AudioSource.isPlaying)
            {
                _IsActivate = false;
                _CharacterApplyAction._BlockMovement = false;
                _CharacterApplyAction._Animator.SetBool("Fishing", false);
                _FishScript.AddStock((int)Random.Range(_RangeFishToAdd.x, _RangeFishToAdd.y));
            }
        }
    }

    public override void LaunchAction(CharacterScript parCharacter, GameObject parZoneAction)
    {

        List<DetectFishScript> detectFishScript = new List<DetectFishScript>(parCharacter.GetComponentsInChildren<DetectFishScript>());
        bool canLaunchAction = false;
        foreach (DetectFishScript dfs in detectFishScript)
        {
            canLaunchAction = dfs.CanCatchFish();
            if (canLaunchAction)
                break;
        }
        if (canLaunchAction)
        {
            parCharacter._AudioSource.Stop();
            parCharacter._AudioSource.clip = _AudioClipAction;
            parCharacter._AudioSource.Play();

            _IsActivate = true;
            _CharacterApplyAction = parCharacter;
            _CharacterApplyAction._BlockMovement = true;
            _CharacterApplyAction._Animator.SetBool("Fishing", true);
        }
        else
        {
            parCharacter._AudioSource.Stop();
            parCharacter._AudioSource.clip = GameScript.Instance._AudioClipError;
            parCharacter._AudioSource.Play();
        }
    }
}
