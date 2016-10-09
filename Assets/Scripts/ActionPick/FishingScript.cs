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

    void Start()
    {
        _FishScript = GameScript.Instance.GetComponent<FishScript>();
    }

    void Update()
    {
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
            _FishScript.AddStock((int)Random.Range(_RangeFishToAdd.x, _RangeFishToAdd.y));
        }
    }
}
