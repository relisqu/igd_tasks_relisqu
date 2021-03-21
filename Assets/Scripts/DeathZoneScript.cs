using UnityEngine;

public class DeathZoneScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    private Vector3 _startPosition;

    private void Awake()
    {
        _startPosition = transform.position;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //  transform.position  _startPosition;
        var newPosition = player.position;
        newPosition.y = _startPosition.y;
        transform.position = newPosition;
    }
}