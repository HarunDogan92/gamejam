using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour
{
    public Image timerImage;
    public float maxTime = 60.0f;
    public float timeRemaining;
    public GameObject gameOver;
    public GameObject tryAgainButton;
    public GameObject player;
    public GameObject camera;
    

    void Start() {
        timeRemaining = maxTime;
    }

    void Update() {
        if (timeRemaining > 0) {
            timeRemaining -= Time.deltaTime;
            timerImage.fillAmount = timeRemaining / maxTime;
        } else {
            GameOver();
        }
    }

    public void GameOver() {
        gameOver.SetActive(true);
        tryAgainButton.SetActive(true);
        player.SetActive(false);
        camera.SetActive(true);
    }

    public void tryAgain() {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
