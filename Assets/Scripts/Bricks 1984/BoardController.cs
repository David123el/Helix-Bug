using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    public bool isGameStarted;

	void Start ()
    {
        isGameStarted = false;
	}
	
	void Update ()
    {
        if (isGameStarted)
        {
            if (Input.touchCount > 0)
                if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    MoveBoard();
                }
        }
    }

    private void MoveBoard()
    {
        var _fingerPosOnScreen = Input.GetTouch(0).position.x;
        //var _relativeFingerPosition = _fingerPosOnScreen / Screen.width;

        var _worldPoint = Camera.main.ScreenToWorldPoint(new Vector3(_fingerPosOnScreen, transform.position.y));

        var _clampedValue = Mathf.Clamp(_worldPoint.x, -2f, 2f);
        //Vector3 _deltaStep = transform.position - new Vector3(_clampedValue, transform.position.y);
        transform.position = new Vector2(_clampedValue, transform.position.y);
    }
}
