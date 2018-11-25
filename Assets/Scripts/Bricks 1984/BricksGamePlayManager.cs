using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BricksGamePlayManager : MonoBehaviour
{
    public bool isGameStarted = false;
    public int numberOfBalls;
    public List<BallController> balls;
    public List<BrickController> bricks;

    [SerializeField] BoardController _boardController;
    [SerializeField] GameObject ball;
    [SerializeField] Transform ballParent;
    [SerializeField] Canvas _gameWonCanvas;

    void Start ()
    {
        balls.AddRange(FindObjectsOfType<BallController>());
        bricks.AddRange(FindObjectsOfType<BrickController>());

        GenerateBalls();
        LockBallsToBoard();
    }
	
	void Update ()
    {
		if (!isGameStarted)
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Touch _touch = Input.GetTouch(0);
                    var _fingerPos = Camera.main.ScreenToWorldPoint(_touch.position);

                    StartCoroutine(BallLauncher(_fingerPos));
                }
            }
        }

        if (bricks.Count == 0)
        {
            foreach (var ball in balls)
            {
                ball.gameObject.SetActive(false);
            }
            DisplayGameWonCanvas();
        }
	}

    IEnumerator BallLauncher(Vector3 _fingerPos)
    {
        if (balls != null)
        {
            foreach (var item in balls)
            {
                item.ballVelocityVector = new Vector2(_fingerPos.x - item.transform.position.x,
                _fingerPos.y - item.transform.position.y);

                item.ballVelocityVector.Normalize();
                var _normalizedVelocity = item.ballVelocityVector.normalized;

                item.LaunchOnTouch(_normalizedVelocity);
                yield return new WaitForSeconds(0.1f);

                if (_boardController != null)
                    _boardController.isGameStarted = true;

                isGameStarted = true;
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

        isGameStarted = false;

        GenerateBalls();
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
