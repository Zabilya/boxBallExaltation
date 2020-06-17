using UnityEngine;

public class FrameScript : MonoBehaviour
{
    public float xAxis;
    public float yAxis;

    private Rigidbody2D _rb;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        xAxis = Input.GetAxis("Horizontal");
        yAxis = Input.GetAxis("Vertical");
        _rb.AddForce(new Vector2(xAxis / 2, yAxis / 2), ForceMode2D.Impulse);
    }
}
