using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameScript : MonoBehaviour
{
    public float xAxis;
    public float yAxis;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");
        GetComponent<Rigidbody2D>().AddForce(new Vector2(xAxis / 2, yAxis / 2), ForceMode2D.Impulse);
    }
}
