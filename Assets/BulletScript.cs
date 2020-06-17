using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Vector2 destination;
    public float heightEdge = 8.0f;
    public float widthEdge = 10.0f;
    public float speed;
    
    private void Start()
    {
        if (destination.Equals(Vector2.zero))
            destination = Vector2.right;
        if (speed <= 0.0f)
            speed = 1.0f;
    }

    private void Update()
    {
        if (transform.position.x < -widthEdge || transform.position.x > widthEdge)
        {
            DestroyImmediate(gameObject);
            return;
        }

        if (transform.position.y < -heightEdge || transform.position.y > heightEdge)
        {
            DestroyImmediate(gameObject);
            return;
        }

        transform.Translate(destination * (speed * Time.deltaTime));
    }
}
