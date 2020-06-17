using System;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Vector2 destination;
    public float heightEdge = 8.0f;
    public float widthEdge = 10.0f;
    public float speed;
    
    private CannonsController _controller;

    private void Start()
    {
        if (destination.Equals(Vector2.zero))
            destination = Vector2.right;
        if (speed <= 0.0f)
            speed = 1.0f;
        _controller = GameObject.Find("CannonController").GetComponent<CannonsController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Frame") || other.transform.CompareTag("BulletsDestroyer"))
        {
            if (other.transform.CompareTag("Frame"))
            {
                CannonsController cannonsController = GameObject.Find("CannonController").GetComponent<CannonsController>();
                cannonsController.points++;
            }

            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        _controller.bullets.RemoveAt(_controller.bullets.IndexOf(gameObject));
    }

    private void Update()
    {
        if (transform.position.x < -widthEdge || transform.position.x > widthEdge ||
            transform.position.y < -heightEdge || transform.position.y > heightEdge)
            Destroy(gameObject);
        
        transform.Translate(destination * (speed * Time.deltaTime));
    }
}
