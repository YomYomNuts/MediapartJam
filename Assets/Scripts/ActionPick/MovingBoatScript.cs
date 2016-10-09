using UnityEngine;
using System.Collections;

public class MovingBoatScript : ObjectActionPickScript
{
    #region Public Attributes
    public GameObject _ParentObjectsMoving;
    public float _Offset;
    public float _TimeForDeplacement = 3.0f;
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    #endregion

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void LaunchAction(CharacterScript parCharacter, GameObject parZoneAction)
    {
        MovingBoatZoneScript mbzs = parZoneAction.GetComponent<MovingBoatZoneScript>();
        if (mbzs)
        {
            _CharacterApplyAction = parCharacter;
            _CharacterApplyAction._AudioSource.Stop();
            _CharacterApplyAction._AudioSource.clip = _AudioClipAction;
            _CharacterApplyAction._AudioSource.Play();
            StartCoroutine(MoveBoat(mbzs._Direction * _Offset));
        }
    }

    IEnumerator MoveBoat(Vector3 parDirection)
    {
        float startMove = 0.0f;
        Vector3 normalizeDirectionBySecond = parDirection / _TimeForDeplacement;
        _CharacterApplyAction._BlockMovement = true;
        _CharacterApplyAction._Animator.SetBool("Rowing", true);
        while (startMove < _TimeForDeplacement)
        {
            for (int i = 0; i < _ParentObjectsMoving.transform.childCount; ++i)
            {
                Transform child = _ParentObjectsMoving.transform.GetChild(i);
                child.position += normalizeDirectionBySecond * Time.deltaTime;
            }
            startMove += Time.deltaTime;
            yield return 0.0f;
        }
        _CharacterApplyAction._BlockMovement = false;
        _CharacterApplyAction._Animator.SetBool("Rowing", false);
    }
}
