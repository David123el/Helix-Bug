using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BricksGamePlayManager : MonoBehaviour
{
    public int numberOfBalls;
    public List<BallController> balls;
    public List<BrickController> bricks;

    private bool areBallsLaunched = false;
    private bool _isAimingMode = true;

    [SerializeField] BoardController _boardController;
    [SerializeField] GameObject ball;
    [SerializeField] Transform ballParent;
    [SerializeField] Canvas _gameWonCanvas;
    [SerializeField] LineRenderer _aimingSystemGUI;

    void Start ()
    {
        balls.AddRange(FindObjectsOfType<BallController>());
        bricks.AddRange(FindObjectsOfType<BrickController>());

        GenerateBalls();
        LockBallsToBoard();
    }
	
	void Update ()
    {
        if (!areBallsLaunched)
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began || Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    Touch _touch = Input.GetTouch(0);
                    var _fingerPos = Camera.main.ScreenToWorldPoint(_touch.position);

                    //aim / draw aim on the screen
                    if (_isAimingMode)
                    {
                        _aimingSystemGUI.SetPosition(0, new Vector3(_boardController.transform.position.x, _boardController.transform.position.y));
                        _aimingSystemGUI.SetPosition(1, _fingerPos);
                    }
                }

                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    Touch _touch = Input.GetTouch(0);
                    var _fingerPos = Camera.main.ScreenToWorldPoint(_touch.position);

                    _aimingSystemGUI.SetPosition(1, new Vector3(_boardController.transform.position.x, _boardController.transform.position.y));
                    _isAimingMode = false;

                    StartCoroutine(BallLauncher(_fingerPos));
                }
            }
        }

        //Display Game Won when all the bricks are destroyed.
        if (bricks.Count == 0)
        {
            foreach (var ball in balls)
            {
                ball.gameObject.SetActive(false);
            }
            _boardController.isGameStarted = false;
            DisplayGameWonCanvas();
        }

        //Increase balls speed after they are all launched.
        if (balls.TrueForAll(x => x._isBallLaunched))
        {
            for (int i = 0; i < balls.Count; i++)
            {
                balls[i].IncreaseBallSpeedBySteps();
            }
        }
	}

    IEnumerator BallLauncher(Vector3 _fingerPos)
    {
        if (balls != null)
        {
            for (int i = 0; i < balls.Count; i++)
            {
                if (_boardController != null)
                {
                    balls[i].ballVelocityVector = new Vector2(_fingerPos.x - balls[i].transform.position.x,
                    Mathf.Clamp(Mathf.Abs(_fingerPos.y - balls[i].transform.position.y), _boardController.transform.position.y, Screen.height));

                    balls[i].ballVelocityVector.Normalize();
                    var _normalizedVelocity = balls[i].ballVelocityVector.normalized;

                    balls[i].LaunchOnTouch(_normalizedVelocity);
                    yield return new WaitForSeconds(0.1f);

                    //if (_boardController != null)
                    _boardController.isGameStarted = true;

                    //isGameStarted = true;
                    areBallsLaunched = true;

                    //handle balls number indicator.
                    if (i == balls.Count - 1) EventManager.OnAllBallsLeftTheBoardHandler();
                }
            }
        }
    }

    public void ChangeBricksPosition()
    {
        foreach (var brick in bricks)
        {
            brick.transform.position = new Vector3(brick.transform.position.x, brick.transform.position.y - 0.6f);
        }
    }

    public void ResetBalls()
    {
        if (_boardController != null)
            _boardController.isGameStarted = false;

        //isGameStarted = false;
        areBallsLaunched = false;
        _isAimingMode = true;

        foreach (var ball in balls)
        {
            ball.gameObject.SetActive(true);
            ball._BallSpeed = ball._initialBallSpeed;
            ball._isBallLaunched = false;
        }

        //GenerateBalls();
        LockBallsToBoard();
    }

    private void LockBallsToBoard()
    {
        for (int j = 0; j < balls.Count; j++)
        {
            balls[j].LockBallToBoard();
        }
    }

    private void GenerateBalls()
    {
        for (int i = 0; i < numberOfBalls; i++)
        {
            var _ball = Instantiate(ball, ballParent);
            BallController _ballController = _ball.GetComponent<BallController>();
            balls.Add(_ballController);
        }
    }

    private void DisplayGameWonCanvas()
    {
        _gameWonCanvas.gameObject.SetActive(true);
    }
}
