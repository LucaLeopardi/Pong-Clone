using UnityEngine;

public class Racket_Movement : MonoBehaviour
{

    [SerializeField] [Range(60,140)] private int speed = 80;

    private enum input_axis {
        [InspectorName("WASD")] Vertical_WS , 
        [InspectorName("Arrow Keys")] Vertical_Arrows
    };
    [SerializeField] private input_axis input_keys;
    
    private Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb2d.velocity = new Vector2(0 , Input.GetAxisRaw(input_keys.ToString())) * speed;
    }
}
