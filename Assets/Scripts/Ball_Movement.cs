using UnityEngine;

public class Ball_Movement : MonoBehaviour
{

    [SerializeField] [Range(60,140)] private int speed = 90;
    private Rigidbody2D rb2d;
    private AudioSource bounce_audio;
    [SerializeField] private Game_Manager gm;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bounce_audio = GetComponent<AudioSource>();

        Invoke("startMatch", 2);    // Start after 2 seconds
    }

    public void startMatch()
    {
        // Start by throwing the ball left or right randomly
        if (Random.value > .5f) {
            rb2d.velocity = Vector2.right * speed;
        } else {
            rb2d.velocity = Vector2.left * speed;
        }        
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        bounce_audio.Play();

        string col_tag = col.gameObject.tag;
        // Racket collision
        if(col_tag == "Racket")
        {
            Vector2 ball_pos = transform.position;
            Vector2 racket_pos = col.transform.position;

            // Horizontal direction is 1 (right) or -1 (left) depending on relative horizontal position of Ball and Racket
            float hor_direction = (ball_pos.x - racket_pos.x) > 0 ? 1 : -1;

            // Vertical direction is determined by relative vertical position of Ball and Racket, normalized to (-1,1)
            float ver_direction = (ball_pos.y - racket_pos.y) / col.collider.bounds.size.y;

            Vector2 direction = new Vector2(hor_direction, ver_direction).normalized;
            
            // Apply final direction to Ball's RigidBody2D
            rb2d.velocity = direction * speed;
        }

        // Goal collision
        if(col_tag == "LeftGoal" || col_tag == "RightGoal")
        {
            bool left_lost = col_tag == "LeftGoal" ? true : false;
            gm.onGoal(left_lost); // Game_Manager only needs to know if the Goal was on the left side
            
            // Game_Manager will update score and start a new match
        }
    }

}
