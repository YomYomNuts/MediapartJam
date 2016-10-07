using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour
{
    #region Public Attributes
    public int _IDJoystick;
    public float _MaxSpeed;
    #endregion

    #region Private Attributes
    private Rigidbody2D _Rigidbody2D;
    private Animator _Animator;
    private bool _FacingRight = true;
    #endregion

    void Start()
    {
        _Rigidbody2D = this.GetComponent<Rigidbody2D>();
        _Animator = this.GetComponent<Animator>();
    }

	void FixedUpdate()
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

    void Flip()
    {
        _FacingRight = !_FacingRight;
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1.0f;
        transform.localScale = currentScale;
    }
}
