using System;
using UnityEngine;

/// <summary>
/// Helper class for snapping to the sides of the screen in 2D space
/// </summary>
public static class ScreenUtil
{
    public enum SnapPosition
    {
        Top,
        Bottom,
        Left,
        Right
    }

    /// <summary>
    /// The screen's world space 2D bounds
    /// </summary>
    public static Rect ScreenPhysicalBounds { get; private set; }

    static ScreenUtil()
    {
        // Get main camera
        var mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("No main camera found!");
            return;
        }

        // Calculate 2D bounds and save for later
        var topLeftBound = mainCamera.ViewportToWorldPoint(Vector3.zero);
        var bottomRightBound = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));
        var delta = bottomRightBound - topLeftBound;
        ScreenPhysicalBounds = new Rect(topLeftBound.x, topLeftBound.y, delta.x, delta.y);
    }

    /// <summary>
    /// Snaps a collider to a specific side of the screen with relative offset
    /// </summary>
    /// <param name="toSnap">The collider to snap</param>
    /// <param name="positionToSnap">Which side of the screen to snap the collider to</param>
    /// <param name="relativeSnapOffset">The relative offset to snap the collider (Calculated by the collider's size)</param>
    public static void SnapCollider(Collider2D toSnap, SnapPosition positionToSnap, Vector2 relativeSnapOffset)
    {
        // Calculate actual offset
        var actualOffset = new Vector3(toSnap.bounds.size.x * relativeSnapOffset.x, toSnap.bounds.size.y * relativeSnapOffset.y);
        Vector3 newPosition;

        // Calculate actual position
        switch (positionToSnap)
        {
            case SnapPosition.Top:
            {
                newPosition = new Vector3(0, ScreenPhysicalBounds.yMax + actualOffset.y);
                break;
            }
            case SnapPosition.Bottom:
            {
                newPosition = new Vector3(0, ScreenPhysicalBounds.yMin - actualOffset.y);
                break;
            }
            case SnapPosition.Left:
            {
                newPosition = new Vector3(ScreenPhysicalBounds.xMin + actualOffset.x, 0);
                break;
            }
            case SnapPosition.Right:
            {
                newPosition = new Vector3(ScreenPhysicalBounds.xMax - actualOffset.x, 0);
                break;
            }
            default:
            {
                throw new ArgumentOutOfRangeException("positionToSnap");
            }
        }

        // Snap
        toSnap.transform.position = newPosition;
    }
}