using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PlayerController playerControllerScript;
    private GameObject pauseButton;

    private bool paused;

    public GameObject titleScreen;
    public GameObject gameOverScreen;
    public GameObject pauseScreen;
    public GameObject inGameScreen;
    public Transform startingPoint;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI powerText;

    public float score;
    public float lerpSpeed;
    public bool isGameActive;
    public int power;
    public int maxPower = 20;

    public void StartGame()
    {
        isGameActive = true;
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        score = 0;
        power = 0;

        playerControllerScript.gameOver = true;
        StartCoroutine(PlayIntro());
        titleScreen.gameObject.SetActive(false);
        inGameScreen.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameActive && !playerControllerScript.gameOver && !paused)
        {
            if (playerControllerScript.doubleSpeed)
            {
                score += 2;
            }
            else if (playerControllerScript.powerMode)
            {
                score += 3;
            }
            else
            {
                score++;
            }

            scoreText.text = "Score: " + score;
        }
    }

    IEnumerator PlayIntro()
    {
        if (isGameActive)
        {
            Vector3 startPos = playerControllerScript.transform.position;
            Vector3 endPos = startingPoint.position;
            float journeyLength = Vector3.Distance(startPos, endPos);
            float startTime = Time.time;

            float distanceCovered = (Time.time - startTime) * lerpSpeed;
            float fractionOfJourney = distanceCovered / journeyLength;

            playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 0.5f);

            while (fractionOfJourney < 1)
            {
                distanceCovered = (Time.time - startTime) * lerpSpeed;
                fractionOfJourney = distanceCovered / journeyLength;
                playerControllerScript.transform.position = Vector3.Lerp(startPos, endPos, fractionOfJourney);
                yield return null;
            }

            playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 1.0f);
            playerControllerScript.gameOver = false;
        }
    }

    public void IncreasePower(int powerNum)
    {
        power += powerNum;
        if (power >= maxPower)
        {
            power = maxPower;
        }

        powerText.text = "Power: " + power;
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        pauseButton = GameObject.Find("Pause Button");
        if (pauseButton != null)
        {
            pauseButton.SetActive(false);
        }
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseGame()
    {
        pauseScreen.SetActive(true);
        pauseButton = GameObject.Find("Pause Button");
        pauseButton.SetActive(false);
        paused = true;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pauseScreen.SetActive(false);
        pauseButton.SetActive(true);
        paused = false;
        Time.timeScale = 1;
    }
}