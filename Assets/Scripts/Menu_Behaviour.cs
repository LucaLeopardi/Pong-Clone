using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Behaviour : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene("MainScene");
    }
}
