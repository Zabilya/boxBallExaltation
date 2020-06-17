using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public Vector2 direction;
    public bool isStuck;

    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector2(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (isStuck)
        {
            direction = Vector2.Reflect(direction, Vector2.left);
            isStuck = false;
        }
        Move(direction);
    }

    private void Move(Vector2 dir)
    {
        transform.Translate(dir * Time.deltaTime);
    }
}
