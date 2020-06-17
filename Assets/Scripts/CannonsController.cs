using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CannonsController : MonoBehaviour
{
    public List<GameObject> bullets;
    public int points;
    private float _bulletSpawnDelay;
    // private int _maxBulletsCount;

    private List<GameObject> _cannons;
    private GameObject _bulletExample;
    private Text _score;
    private BallController _ballController;
    private float _lastBulletSpawnTime;
    private int _lastCannonIndexUsed;
    private int _pointsLastFrame;
    
    private void Start()
    {
        points = 0;
        _pointsLastFrame = 0;
        // _maxBulletsCount = 15;
        _bulletSpawnDelay = 0.5f;

        var upCannons = GameObject.FindGameObjectsWithTag("UpCannon");
        var downCannons = GameObject.FindGameObjectsWithTag("DownCannon");
        var leftCannons = GameObject.FindGameObjectsWithTag("LeftCannon");
        var rightCannons = GameObject.FindGameObjectsWithTag("RightCannon");
        
        _cannons = upCannons.Concat(downCannons).Concat(leftCannons).Concat(downCannons).ToList();
        _lastBulletSpawnTime = 0.0f;
        _lastCannonIndexUsed = 0;
        bullets = new List<GameObject>();
        _bulletExample = Resources.Load<GameObject>("Prefabs/bullet");
        _ballController = GameObject.Find("ball").GetComponent<BallController>();
        _score = GameObject.Find("Score").GetComponent<Text>();
    }
    
    private void Update()
    {
        if (_pointsLastFrame < points)
        {
            _pointsLastFrame++;
            IncreaseUiAndDifficulty();
        }
        // if (bullets.Count < _maxBulletsCount)
        // {
        if (Time.time - _lastBulletSpawnTime > _bulletSpawnDelay)
        {
            SpawnNewBullet();
        }
        // }
    }

    private void SpawnNewBullet()
    {
        var cannonIndex = GetNewCannonIndex();
        var daddyCannon = _cannons[cannonIndex];
        var newBullet = Instantiate(_bulletExample);
        var newBulletScript = newBullet.GetComponent<BulletScript>();
        
        bullets.Add(newBullet);
        _lastBulletSpawnTime = Time.time;
        _lastCannonIndexUsed = cannonIndex;
        newBullet.transform.position = daddyCannon.transform.position;
        if (daddyCannon.CompareTag("LeftCannon"))
            newBulletScript.destination = Vector2.right;
        else if (daddyCannon.CompareTag("RightCannon"))
            newBulletScript.destination = Vector2.left;
        else if (daddyCannon.CompareTag("UpCannon"))
            newBulletScript.destination = Vector2.down;
        else
            newBulletScript.destination = Vector2.up;
    }
    
    private int GetNewCannonIndex()
    {
        var newIndex = Random.Range(0, _cannons.Count - 1);;
        
        while (newIndex == _lastCannonIndexUsed)
            newIndex = Random.Range(0, _cannons.Count - 1);
        return newIndex;
    }

    private void IncreaseUiAndDifficulty()
    {
        IncreaseUi();
        if (points >= 30)
            IncreaseDifficulty();
    }

    private void IncreaseUi()
    {
        _score.text = "Score: " + points;
    }

    private void IncreaseDifficulty()
    {
        if (points % 50 == 0)
        {
            _ballController.IncreaseBallSpeed();
        }
        // if (points % 10 == 0)
        // {
            // if (_maxBulletsCount < 50)
            //     _maxBulletsCount++;
        // }
        if (points % 10 == 0)
        {
            if (_bulletSpawnDelay > 0.1f)
                _bulletSpawnDelay -= 0.01f;
        }
    }
}
