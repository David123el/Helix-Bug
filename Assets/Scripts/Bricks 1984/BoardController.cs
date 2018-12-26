using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    public bool isGameStarted;

    //private Vector3 _InitialFingerPosWorldPoint;
    //private Vector3 _currentFingerposWorldPoint;

    [SerializeField] GameObject _numOfBallsUI;
    [SerializeField] float _lerpingMovementValue = 0.5f;

    void Start ()
    {
        isGameStarted = false;
        _numOfBallsUI.SetActive(true);
	}

    private void OnEnable()
    {
        EventManager.OnAllBallsLeftTheBoard += TurnOffBallsNumber;
        EventManager.OnGamePaused += DisableBoardMovement;
    }

    private void OnDisable()
    {
        EventManager.OnAllBallsLeftTheBoard -= TurnOffBallsNumber;
        EventManager.OnGamePaused -= DisableBoardMovement;
    }

    void Update ()
    {
        if (isGameStarted)
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Stationary)
                    //|| IsMovementInverted(_currentFingerposWorldPoint))
                {
                    //var _initialXFingerPos = Input.GetTouch(0).position.x;
                    //_InitialFingerPosWorldPoint = Camera.main.ScreenToWorldPoint(new Vector3(_initialXFingerPos, transform.position.y));
                }

                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    MoveBoard();
                    //bool _checkInvert = IsMovementInverted(_currentFingerposWorldPoint);
                    //Debug.Log(_checkInvert);
                }
            }
        }
        else _numOfBallsUI.SetActive(true);
    }

    private void MoveBoard()
    {
        var _fingerPosOnScreen = Input.GetTouch(0).position.x;

        var _currentFingerposWorldPoint = Camera.main.ScreenToWorldPoint(new Vector3(_fingerPosOnScreen, transform.position.y));

        //var _deltaStep = _currentFingerposWorldPoint - _InitialFingerPosWorldPoint;

        //var _boardPosToMoveTo = transform.position + _deltaStep;

        var _clampedValue = Mathf.Clamp(_currentFingerposWorldPoint.x, -2f, 2f);

        //where the new position of the board is going to be.
        //used for Vector2.Lerp
        var _newClampedBoardPos = new Vector2(_clampedValue, transform.position.y);

        transform.position = Vector2.Lerp(transform.position, _newClampedBoardPos, _lerpingMovementValue);

        //transform.position = new Vector2(_clampedValue, transform.position.y);
    }

    /*private bool IsMovementInverted(Vector3 currentFingerPos)
    {
        float _stackedValue = 0;
        _stackedValue += currentFingerPos.x;

        if (_stackedValue < _currentFingerposWorldPoint.x) return true;
        else return false;
    }*/

    public void TurnOffBallsNumber()
    {
        _numOfBallsUI.SetActive(false);
    }

    public void DisableBoardMovement()
    {
        isGameStarted = false;
    }
}
