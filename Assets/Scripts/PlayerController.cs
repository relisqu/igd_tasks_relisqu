using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.2f;
    private bool _isMovingLeft;
    private Vector3 _startPosition;

    // Start is called before the first frame update
    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) transform.rotation = ChangePosition();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position += transform.forward * (speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            Debug.Log("You won.");
            SceneManager.LoadScene("SampleScene");
        }

        if (other.CompareTag("DeathZone"))
        {
            Debug.Log("You died.");
            die();
        }
    }

    private void die()
    {
        SceneManager.LoadScene("SampleScene");
        CoinScript.CoinAmount = 0;
    }

    private Quaternion ChangePosition()
    {
        if (_isMovingLeft)
        {
            _isMovingLeft = false;
            return Quaternion.Euler(0, 0f, 0);
        }

        _isMovingLeft = true;
        return Quaternion.Euler(0, 90f, 0);
    }
}