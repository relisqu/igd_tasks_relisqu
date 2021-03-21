using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerationScript : MonoBehaviour
{
    private const float PreferedWidth = 2f;
    private const int MinLength = 3;
    private const int MaxLength = 15;

    public int amountOfPlatforms;
    public Vector3 startPosition;
    public Transform platform;
    public Transform player;
    public Transform coin;
    public Transform finish;

    private void Start()
    {
        GenerateLevel();
    }

    private void GenerateLevel()
    {
        var platformList = new List<Platform>(amountOfPlatforms);
        // Debug.Log();
        Debug.Log(platformList.Count);
        platformList.Add(new Platform(startPosition.x, startPosition.z, true));

        player.position = new Vector3(platformList[0].GetStartX(), startPosition.y, platformList[0].GetStartY());
        for (var i = 1; i < amountOfPlatforms; i++)
        {
            var startPlatformPosition = platformList[i - 1].ReturnNextPlatformStartCoordinates();
            platformList.Add(new Platform(startPlatformPosition.x, startPlatformPosition.y,
                platformList[i - 1].ReturnNextPlatformRotation()));
            Debug.Log(i);
        }

        for (var i = 0; i < amountOfPlatforms; i++) platformList[i].Initialise(platform, coin);

        platformList.Last().InitialiseFinish(finish);
    }

    private class Platform
    {
        private readonly int _amountOfCoins;
        private readonly bool _isRotated;
        private readonly float _length;
        private readonly float _startX;
        private readonly float _startY;

        public Platform(float startX, float startY, bool isRotated)
        {
            _length = Random.Range(MinLength, MaxLength);
            Debug.Log(startX + " " + startY);
            if (isRotated)
            {
                _startX = startX;
                _startY = startY + _length / 2;
            }
            else
            {
                _startX = startX + _length / 2;
                _startY = startY;
            }

            _isRotated = isRotated;
            _amountOfCoins = (int) _length / 3;
        }

        public float GetStartX()
        {
            return _startX;
        }

        public float GetStartY()
        {
            return _startY;
        }

        public bool ReturnNextPlatformRotation()
        {
            return !_isRotated;
        }

        public Vector2 ReturnNextPlatformStartCoordinates()
        {
            return _isRotated
                ? new Vector2(_startX - PreferedWidth / 2, _startY + _length / 2)
                : new Vector2(_startX + _length / 2, _startY - PreferedWidth / 2);
        }

        public void InitialiseFinish(Transform finish)
        {
            Quaternion rot;
            Vector3 finishPosition;
            if (_isRotated)
            {
                finishPosition = new Vector3(_startX, -14f, _startY + _length / 2);
                rot = Quaternion.Euler(0f, 0f, 0f);
            }
            else
            {
                finishPosition = new Vector3(_startX + _length / 2, -14f, _startY);
                rot = Quaternion.Euler(0f, 90f, 0f);
            }

            Instantiate(finish, finishPosition, rot);
        }

        public void Initialise(Transform platform, Transform coin)
        {
            var position = new Vector3(_startX, -15f, _startY);
            var rot = Quaternion.Euler(0, _isRotated ? 90f : 0f, 0);
            var newObject = Instantiate(platform, position, rot);
            newObject.transform.localScale = new Vector3(_length, 1f, PreferedWidth);
            for (var i = 0; i < _amountOfCoins; i++)
                if (i != 0)
                {
                    var coinPosition = _isRotated
                        ? new Vector3(_startX, -14f, _startY + _length / _amountOfCoins * i - _length / 2)
                        : new Vector3(_startX - _length / 2 + _length / _amountOfCoins * i, -14f, _startY);
                    Instantiate(coin, coinPosition, rot);
                }
        }
    }
}