using System;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public static int CoinAmount;
    private readonly float _amplitude = 0.2f;
    private bool _isCollected;
    private readonly float _rotationSpeed = 10f;
    private readonly float _speed = 2f;
    private Vector3 _startPosition;

    private void Awake()
    {
        _startPosition = transform.position;
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        transform.position = Mathf.Sin(Time.fixedTime * _speed) * _amplitude * Vector3.up + _startPosition;
        transform.Rotate(Vector3.up, Time.deltaTime * _rotationSpeed);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_isCollected)
        {
            CoinAmount++;
            OnMoneyCollected?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject);
            _isCollected = true;
        }
    }

    public static event EventHandler OnMoneyCollected;

    public static int GetCoinAmount()
    {
        return CoinAmount;
    }
}