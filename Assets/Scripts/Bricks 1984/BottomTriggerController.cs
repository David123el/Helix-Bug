using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomTriggerController : MonoBehaviour
{
    private BricksGamePlayManager _gamePlayManager;

    private void Start()
    {
        _gamePlayManager = FindObjectOfType<BricksGamePlayManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BallController>() != null)
        {
            var _ballController = collision.gameObject.GetComponent<BallController>();

            collision.gameObject.SetActive(false);
            _ballController.isBallOutOfField = true;

            if (_gamePlayManager != null)
            {
                for (int i = 0; i < _gamePlayManager.balls.Count; i++)
                {
                    if (_gamePlayManager.balls.TrueForAll(x => x.isBallOutOfField))
                    {
                        _gamePlayManager.balls.FindAll(x => x.isBallOutOfField = false);

                        //Bricks fall down one step.
                        _gamePlayManager.ChangeBricksPosition();

                        //reset balls.
                        _gamePlayManager.ResetBalls();
                    }
                }
            }
        }
    }
}