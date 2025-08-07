using UnityEngine;

public class MiniMapFollow : MonoBehaviour
{
    public Transform target;

    void LateUpdate()
    {
        Vector3 newPos = target.position;
        newPos.y = transform.position.y; // Sadece yukarıdan takip etsin
        transform.position = newPos;
    }
}