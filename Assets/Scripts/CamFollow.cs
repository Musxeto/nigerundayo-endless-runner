using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    void LateUpdate() // Use LateUpdate for smoother camera movement
    {
        if (player != null)
        {
            transform.position = player.position + offset;
        }
    }
}
