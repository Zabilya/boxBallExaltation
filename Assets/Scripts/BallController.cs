using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallController : MonoBehaviour
{
    public Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        //TODO рандомные стороны
        direction = new Vector2(Random.Range(0.5f, 1.0f) * 2, Random.Range(0.5f, 1.0f) * 2);
    }

    // Update is called once per frame
    void Update()
    {
        CheckIsStuck();
        Move(direction);
    }

    private void Move(Vector2 dir)
    {
        transform.Translate(dir * Time.deltaTime);
    }

    private void CheckIsStuck()
    {
        Vector2 pos = transform.position;
        if (pos.x >= 6.75)
        {
            CalculateReflectDir(Vector2.left);
        }
        else if (pos.x <= -6.75)
        {
            CalculateReflectDir(Vector2.right);
        }
        else if (pos.y >= 3.75)
        {
            CalculateReflectDir(Vector2.down);
        }
        else if (pos.y <= -3.75)
        {
            CalculateReflectDir(Vector2.up);
        }
    }

    private void CalculateReflectDir(Vector2 norm)
    {
        direction = Vector2.Reflect(direction, norm);
    }
}
