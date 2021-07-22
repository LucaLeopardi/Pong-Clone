using UnityEngine;
using TMPro;

public class Game_Manager : MonoBehaviour
{
    [SerializeField] private Ball_Movement ball;
    private int score_left, score_right;
    [SerializeField] private TextMeshProUGUI score_UI_left, score_UI_right;
    [SerializeField] private ParticleSystem fx_left, fx_right;
    private AudioSource score_audio; 

    void Start()
    {
        score_audio = GetComponent<AudioSource>();

        score_left = 0;
        score_right = 0;
        updateScore();
    }

    private void updateScore()
    {
        score_UI_left.text = score_left.ToString();
        score_UI_right.text = score_right.ToString();
    }

    public void onGoal(bool left_lost)
    {
        score_audio.Play();
        ball.gameObject.SetActive(false);

        if (left_lost) {
            fx_left.Play();     // Plays goal particle effects
            score_right++;      // Increases score for opponent
        } else {
            fx_right.Play(); 
            score_left++;
        }
        updateScore();

        // Move ball to center and start again in 2 seconds
        ball.gameObject.transform.position = new Vector2(0,0);
        ball.gameObject.SetActive(true);
        ball.Invoke("startMatch", 2);        
    }
}
