using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ColliderSnap : MonoBehaviour
{
    #region Editor exposed members
    [SerializeField] private ScreenUtil.SnapPosition _snapPosition;
    [SerializeField] private Vector2 _relativeOffset = Vector2.zero;
    #endregion

    private void Start()
    {
        // Snap and destroy
        ScreenUtil.SnapCollider(GetComponent<Collider2D>(), _snapPosition, _relativeOffset);
        Destroy(this);
    }
}