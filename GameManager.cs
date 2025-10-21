using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Element Values")]
    public float fire = 50;
    public float water = 50;
    public float air = 50;
    public float earth = 50;

    [Header("Gameplay")]
    public int turn = 1;
    public int score = 0;
    private bool gameOver = false;

    [Header("UI References")]
    public TMP_Text turnText;
    public TMP_Text scoreText;

    [Header("Game Over UI")]
    public GameObject gameOverPanel;
    public TMP_Text finalScoreText;
    public TMP_Text finalTurnText;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateUI();
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void ApplyCardEffect(float fireEffect, float waterEffect, float airEffect, float earthEffect)
    {
        if (gameOver) return;

        fire += fireEffect * turn;
        water += waterEffect * turn;
        air += airEffect * turn;
        earth += earthEffect * turn;

        fire = Mathf.Clamp(fire, 0, 100);
        water = Mathf.Clamp(water, 0, 100);
        air = Mathf.Clamp(air, 0, 100);
        earth = Mathf.Clamp(earth, 0, 100);

        CalculateScore();

        CheckGameOver();

        if (!gameOver)
        {
            turn++;
            UpdateUI();
        }
    }

    void CalculateScore()
    {
        float fireDiff = Mathf.Abs(50 - fire);
        float waterDiff = Mathf.Abs(50 - water);
        float airDiff = Mathf.Abs(50 - air);
        float earthDiff = Mathf.Abs(50 - earth);

        float fireScore = Mathf.Max(0, 100 - (fireDiff * 2));
        float waterScore = Mathf.Max(0, 100 - (waterDiff * 2));
        float airScore = Mathf.Max(0, 100 - (airDiff * 2));
        float earthScore = Mathf.Max(0, 100 - (earthDiff * 2));

        int roundScore = Mathf.RoundToInt((fireScore + waterScore + airScore + earthScore) / 4);
        score += roundScore;
    }

    void UpdateUI()
    {
        if (turnText != null)
            turnText.text = "Turn: " + turn;

        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    void CheckGameOver()
    {
        if (fire <= 0 || fire >= 100 ||
            water <= 0 || water >= 100 ||
            air <= 0 || air >= 100 ||
            earth <= 0 || earth >= 100)
        {
            ShowGameOver();
        }
    }

    void ShowGameOver()
    {
        gameOver = true;
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            if (finalScoreText != null)
                finalScoreText.text = "Final Score: " + score;
            if (finalTurnText != null)
                finalTurnText.text = "Turns Survived: " + turn;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
