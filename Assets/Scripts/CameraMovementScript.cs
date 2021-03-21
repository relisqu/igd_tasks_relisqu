using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    public float startY;
    public Transform playerPosition;

    private void Awake()
    {
        startY = transform.position.y;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        var newPosition = playerPosition.position;
        newPosition.y = startY;
        transform.position = newPosition;
    }
}