using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager
{
    public static Action<BrickController> onBrickGotHit;

    public static void OnBrickGotHit(BrickController brick)
    {
        if (onBrickGotHit != null)
            onBrickGotHit(brick);
    }
}
