using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Vector2 destination;
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
            Destroy(gameObject);
            if (other.transform.CompareTag("Frame"))
                _controller.points++;
        }
    }

    private void OnDestroy()
    {
        _controller.bullets.RemoveAt(_controller.bullets.IndexOf(gameObject));
    }

    private void Update()
    {
        transform.Translate(destination * (speed * Time.deltaTime));
    }
}
