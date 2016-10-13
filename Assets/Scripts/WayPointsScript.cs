using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WayPointsScript : MonoBehaviour
{
    #region Public Attributes
    public List<GameObject> _ListWayPoints;
    public float _SpeedX;
    #endregion

    #region Protected Attributes
    #endregion

    #region Private Attributes
    private int _CurrentIndex;
    private Rigidbody2D _Rigidbody2D;
    private Animator _Animator;
    private Vector2 _Direction;
    private Vector3 _PreviousPosition;
    #endregion

    void Start()
    {
        _Rigidbody2D = this.GetComponent<Rigidbody2D>();
        _Animator = this.GetComponent<Animator>();
        _CurrentIndex = 0;
        NewDirection();
        _PreviousPosition = this.transform.position;
    }
	
	void FixedUpdate()
    {
        if (_CurrentIndex < _ListWayPoints.Count)
        {
            GameObject goal = _ListWayPoints[_CurrentIndex];
            if (goal != null)
            {
                float diffX = goal.transform.position.x - _PreviousPosition.x;
                float newDiffX = goal.transform.position.x - this.transform.position.x;

                _Rigidbody2D.velocity = _Direction.normalized * _SpeedX;
                _Animator.SetFloat("Speed", Mathf.Abs(_Rigidbody2D.velocity.magnitude));

                if (Mathf.Sign(diffX) != Mathf.Sign(newDiffX) || newDiffX == 0.0f)
                {
                    _Rigidbody2D.velocity = Vector2.zero;
                    this.transform.position = goal.transform.position;
                    ++_CurrentIndex;
                    NewDirection();
                }
            }
            _PreviousPosition = this.transform.position;
        }
    }

    void NewDirection()
    {
        if (_CurrentIndex < _ListWayPoints.Count)
        {
            GameObject goal = _ListWayPoints[_CurrentIndex];
            if (goal != null)
                _Direction = goal.transform.position - this.transform.position;
        }
    }

    public bool GoalDone()
    {
        return this.transform.position == _ListWayPoints[_ListWayPoints.Count-1].transform.position;
    }
}
