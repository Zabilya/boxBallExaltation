using Controllers;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviour
{
    public Vector2 direction;

    private void Start()
    {
        float corner = Random.Range(0.0f, 360.0f);
        direction = RotateVector2(Vector2.one, corner) * 1.3f;
        GameController.Instance.ballController = this;
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
                GameController.CurrentGameState = GameController.GameState.Lose;
                return;
        }
    }

    public void IncreaseBallSpeed()
    {
        direction *= 1.1f;
    }

    private void Move(Vector2 dir)
    {
        transform.Translate(dir * Time.deltaTime);
    }

    private void CalculateReflectDir(Vector2 norm)
    {
        Vector2 trueRef = Vector2.Reflect(direction, norm);
        direction = RotateVector2(trueRef, Random.Range(-20.0f, 20.0f));
    }

    private Vector2 RotateVector2(Vector2 vec, float angleDegrees)
    {
        float corner = angleDegrees * Mathf.Deg2Rad;
        float x = vec.x * Mathf.Cos(corner) - vec.y * Mathf.Sin(corner);
        float y = vec.x * Mathf.Sin(corner) + vec.y * Mathf.Cos(corner);
        return new Vector2(x, y);
    }
}
