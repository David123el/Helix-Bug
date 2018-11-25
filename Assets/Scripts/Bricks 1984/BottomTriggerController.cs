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
            collision.gameObject.SetActive(false);
            _gamePlayManager.balls.Remove(collision.gameObject.GetComponent<BallController>());            

        if (_gamePlayManager != null)
            {
                if (_gamePlayManager.balls.Count <= 0)
                {
                    //Bricks fall down one step.
                    _gamePlayManager.ChangeBricksPosition();

                    //reset balls.
                    _gamePlayManager.ResetBalls();
                }
            }
        }
    }
}