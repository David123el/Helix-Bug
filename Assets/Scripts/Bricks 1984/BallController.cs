using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallController : MonoBehaviour
{
    [SerializeField] float _timeIntervalToIncreaseBallSpeed = 1.0f;
    [SerializeField] float _ballSpeedMultiplyer = 1.1f;
    [SerializeField] float _minBallSpeed;
    [SerializeField] float _maxBallSpeed;

    Rigidbody2D _rb2D;
    private float _initialTime;
    private BoardController _board;

    public float ballVelocity;
    public float _BallSpeed;
    public float _initialBallSpeed;
    public float _offsetToBoard;
    public bool isBallOutOfField = false;
    public bool _isBallLaunched = false;

    public Vector2 ballVelocityVector;

    void Start ()
    {
        _board = FindObjectOfType<BoardController>();
        _rb2D = GetComponent<Rigidbody2D>();

        LockBallToBoard();
        _isBallLaunched = false;

        _initialTime = Time.time;
        _initialBallSpeed = _BallSpeed;
	}
	
	void Update ()
    {

    }

    public void LockBallToBoard()
    {
        if (_rb2D != null)
            _rb2D.simulated = false;

        transform.position = new Vector2(_board.transform.position.x, _board.transform.position.y + _offsetToBoard);
        Debug.Log(_board.transform.position.x);
    }

    public void LaunchOnTouch(Vector2 vector)
    {
        if (_rb2D != null)
        {
            _rb2D.simulated = true;
            _rb2D.velocity = vector * _BallSpeed;
        }

        _isBallLaunched = true;
    }

    public void IncreaseBallSpeedBySteps()
    {
        if (Time.time >= _initialTime + _timeIntervalToIncreaseBallSpeed)
        {
            IncreaseBallSpeed();
            _initialTime = Time.time;
        }
    }

    IEnumerator IncreaseSpeedBySteps()
    {
        IncreaseBallSpeed();
        yield return new WaitForSeconds(5.0f);
    }
    //ball speed needs fixing!
    public void IncreaseBallSpeed()
    {
        _BallSpeed *= _ballSpeedMultiplyer;
        //_rb2D.velocity += _BallSpeed;
    }
}
