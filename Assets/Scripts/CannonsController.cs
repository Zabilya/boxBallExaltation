using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CannonsController : MonoBehaviour
{
    public List<GameObject> bullets;
    public float bulletSpawnDelay;
    public int maxBulletsCount;

    private List<GameObject> _cannons;
    private GameObject _bulletExample;
    private float _lastBulletSpawnTime;
    private int _lastCannonIndexUsed;
    
    private void Start()
    {
        if (maxBulletsCount == 0)
            maxBulletsCount = 10;
        if (bulletSpawnDelay <= 0.0f)
            bulletSpawnDelay = 0.75f;

        var upCannons = GameObject.FindGameObjectsWithTag("UpCannon");
        var downCannons = GameObject.FindGameObjectsWithTag("DownCannon");
        var leftCannons = GameObject.FindGameObjectsWithTag("LeftCannon");
        var rightCannons = GameObject.FindGameObjectsWithTag("RightCannon");
        
        _cannons = upCannons.Concat(downCannons).Concat(leftCannons).Concat(downCannons).ToList();
        _lastBulletSpawnTime = 0.0f;
        _lastCannonIndexUsed = 0;
        bullets = new List<GameObject>();
        _bulletExample = Resources.Load<GameObject>("Prefabs/bullet");
    }
    
    private void Update()
    {
        if (bullets.Count < maxBulletsCount)
        {
            if (Time.time - _lastBulletSpawnTime > bulletSpawnDelay)
            {
                SpawnNewBullet();
            }
        }
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
}
