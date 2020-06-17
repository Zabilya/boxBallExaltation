using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviour
{
    public Vector2 direction;
    
    private void Start()
    {
        //TODO рандомные стороны
        direction = new Vector2(Random.Range(0.5f, 1.0f) * 2, Random.Range(0.5f, 1.0f) * 2);
    }
    
    private void Update()
    {
        Move(direction);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "RightCannon":
                CalculateReflectDir(Vector2.left);
                return;
            case "LeftCannon":
                CalculateReflectDir(Vector2.right);
                return;
            case "UpCannon":
                CalculateReflectDir(Vector2.down);
                return;
            case "DownCannon":
                CalculateReflectDir(Vector2.up);
                return;
            case "Bullet":
                SceneManager.LoadScene(1);
                return;
        }
    }

    private void Move(Vector2 dir)
    {
        transform.Translate(dir * Time.deltaTime);
    }

    private void CalculateReflectDir(Vector2 norm)
    {
        direction = Vector2.Reflect(direction, norm);
    }
}
