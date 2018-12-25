using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager
{
    public static Action<BrickController> onBrickGotHit;
    //public static Action<GameObject> onAllBallsLeftTheBoard;

    public delegate void AllBallsLeftTheBoardEvent();
    public static event AllBallsLeftTheBoardEvent OnAllBallsLeftTheBoard;

    public delegate void GamePausedEvent();
    public static event GamePausedEvent OnGamePaused;

    public static void OnBrickGotHit(BrickController brick)
    {
        if (onBrickGotHit != null)
            onBrickGotHit(brick);
    }

    public static void OnAllBallsLeftTheBoardHandler()
    {
        if (OnAllBallsLeftTheBoard != null)
        {
            OnAllBallsLeftTheBoard();
        }
    }

    public static void OnGamePausedHandler()
    {
        if (OnGamePaused != null)
        {
            OnGamePaused();
        }
    }
}
