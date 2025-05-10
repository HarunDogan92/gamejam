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
    

    void Start() {
        timeRemaining = maxTime;
    }

    void Update() {
        if (timeRemaining > 0) {
            timeRemaining -= Time.deltaTime;
            timerImage.fillAmount = timeRemaining / maxTime;
        } else {
            gameOver.SetActive(true);
            tryAgainButton.SetActive(true);
            player.SetActive(false);
        }
    }

    public void tryAgain() {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
