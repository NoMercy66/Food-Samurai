using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 0.8f;
    private int score;
    private int lives;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    private GameObject titleScreen;
    public GameObject scoreText;
    public GameObject livesText;
    public bool isGameActive;
    public GameObject pauseScreen;
    public GameObject yourScoreText;
    private bool paused;
    // Start is called before the first frame update
    void Start()
    {
        titleScreen = GameObject.Find("Title Screen");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && isGameActive){
            ChangePaused();
        }
    }
    IEnumerator SpawnTarget(){
        while(isGameActive){
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0,targets.Count);
            Instantiate(targets[index]);
        }
    }
    public void UpdateScore(int scoreToAdd){
        score += scoreToAdd;
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: "+ score;
    }
    public void UpdateLives(int livesToChange)
    {
        lives += livesToChange;
        livesText.GetComponent<TextMeshProUGUI>().text = "Lives: " + lives;
        if (lives <= 0){
            GameOver();
        }
    }
    public void GameOver(){
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        yourScoreText.GetComponent<TextMeshProUGUI>().text = "Your Score: " + score;
        yourScoreText.gameObject.SetActive(true);
        scoreText.SetActive(false);
        livesText.SetActive(false);
        isGameActive = false;
    }
    public void RestartGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame(int difficulty){
        spawnRate /= difficulty;
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        score = 0;
        lives =0;
        UpdateScore(0);
        UpdateLives(difficulty+2);
        titleScreen.SetActive(false);
        scoreText.SetActive(true);
        livesText.SetActive(true);
    }
    void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            scoreText.GetComponent<AudioSource>().Pause();
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            scoreText.GetComponent<AudioSource>().UnPause();
            Time.timeScale = 1;
        }
    }
}
