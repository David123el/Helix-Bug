using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private BoardController _board;

    [SerializeField] float _BallSpeed;
    [SerializeField] float _minBallSpeed;
    [SerializeField] float _maxBallSpeed;

    Rigidbody2D _rb2D;

    public float ballVelocity;
    public float _offsetToBoard;

    public Vector2 ballVelocityVector;

    //private void OnEnable()
    //{
    //    BricksGamePlayManager._ballsAvailable.Add(this);
    //}

    //private void OnDisable()
    //{
    //    BricksGamePlayManager._ballsAvailable.Remove(this);
    //}

    void Start ()
    {
        _board = FindObjectOfType<BoardController>();
        _rb2D = GetComponent<Rigidbody2D>();

        LockBallToBoard();
	}
	
	void Update ()
    {
        //IncreaseBallSpeedBySteps();
    }

    public void LockBallToBoard()
    {
        _rb2D.simulated = false;
        transform.position = new Vector2(_board.transform.position.x, _board.transform.position.y + _offsetToBoard);
    }

    public void LaunchOnTouch(Vector2 vector)
    {
        _rb2D.simulated = true;
        _rb2D.velocity = vector * _BallSpeed;
    }

    private void IncreaseBallSpeedBySteps()
    {
        float _currentTime = Time.deltaTime;
        if (Time.deltaTime > _currentTime + 5.0f)
        {
            IncreaseBallSpeed();
            _currentTime = Time.deltaTime;
        }
    }

    IEnumerator IncreaseSpeedBySteps()
    {
        IncreaseBallSpeed();
        yield return new WaitForSeconds(5.0f);
    }

    public void IncreaseBallSpeed()
    {
        _rb2D.velocity.Normalize();
        var _normalizedValue = _rb2D.velocity.normalized;
        _rb2D.velocity += _normalizedValue * ballVelocity;
    }
}
