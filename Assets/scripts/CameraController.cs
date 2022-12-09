using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform player;

    public float smoothTime;

    private void Start()
    {
        // Get player
        player = FindObjectOfType<Locomotor>().GetComponent<Transform>();
    }

    private void Update()
    {
        // Follow player
        transform.position = Vector3.Lerp(transform.position, player.position, smoothTime * Time.deltaTime);
    }
}