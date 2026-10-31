using UnityEngine;

public class CamMove : MonoBehaviour
{
    public GameObject player;

    private Vector3 velocity = Vector3.zero;
    public float smoothTime = 0.1f;

    void LateUpdate()
    {
        if (player != null)
        {

            Vector3 targetPosition = new Vector3(player.transform.position.x, player.transform.position.y, -10f);

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}