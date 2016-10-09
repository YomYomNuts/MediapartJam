using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour
{
    #region Public Attributes
    public int _IDJoystick;
    public Sprite _Face;
    public float _MaxSpeed;
    [HideInInspector]
    public AudioSource _AudioSource;
    [HideInInspector]
    public bool _BlockMovement;
    [HideInInspector]
    public Animator _Animator;
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    private Rigidbody2D _Rigidbody2D;
    private bool _FacingRight = true;
    private ActionVoteScript _CurrentAction;
    #endregion

    #region Properties
    public ActionVoteScript CurrentAction
    {
        set { _CurrentAction = value; }
        get { return _CurrentAction; }
    }
    #endregion

    void Start()
    {
        _Rigidbody2D = this.GetComponent<Rigidbody2D>();
        _Animator = this.GetComponent<Animator>();
        _AudioSource = this.GetComponent<AudioSource>();
        _CurrentAction = null;
        _BlockMovement = false;
    }

	void FixedUpdate()
    {
        if (GameScript.Instance.PlayerCanAction && !_BlockMovement)
        {
            float moveRightLeft = Input.GetAxis("Horizontal_" + _IDJoystick);
            float moveUpDown = Input.GetAxis("Vertical_" + _IDJoystick);
            _Rigidbody2D.velocity = new Vector2(moveRightLeft, moveUpDown).normalized * _MaxSpeed;
            _Animator.SetFloat("Speed", Mathf.Abs(_Rigidbody2D.velocity.magnitude));

            if (moveRightLeft > 0.0f && !_FacingRight)
                Flip();
            else if (moveRightLeft < 0.0f && _FacingRight)
                Flip();
        }
        else
        {
            _Rigidbody2D.velocity = new Vector2();
            _Animator.SetFloat("Speed", Mathf.Abs(_Rigidbody2D.velocity.magnitude));
        }
    }

    void Flip()
    {
        _FacingRight = !_FacingRight;
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1.0f;
        transform.localScale = currentScale;
    }
}
