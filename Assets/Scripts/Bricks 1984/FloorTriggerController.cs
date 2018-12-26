using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game Over if Bricks reach the floor.
/// </summary>
public class FloorTriggerController : MonoBehaviour
{
    [SerializeField] Canvas _gameOverCanvas;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BrickController>() != null)
        {
            //Disable Board Movement.
            //needs fixing.
            EventManager.OnGamePausedHandler();

            _gameOverCanvas.gameObject.SetActive(true);
        }
    }
}
