using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("Audio")]
    public AudioSource hitSFX;
    public AudioSource missSFX;

    [Header("UI")]
    public TextMeshPro scoreText;
    public TextMeshPro comboText;

    [Header("Win Condition")]
    public int winScoreThreshold = 5000;
    public float songLength = 60f;

    private static int score;
    private static int combo;

    private float timer;
    private bool ended = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        score = 0;
        combo = 0;
        timer = 0;
        ended = false;

        UpdateUI();
    }

    private void Update()
    {
        if (ended) return;

        timer += Time.deltaTime;

        // ONLY check at end of song
        if (timer >= songLength)
        {
            EndGame();
        }
    }

    public static void Hit()
    {
        combo++;

        score += 100 * Mathf.Max(1, combo / 5);

        if (Instance.hitSFX != null)
            Instance.hitSFX.Play();

        Instance.UpdateUI();
    }

    public static void Miss()
    {
        combo = 0;

        if (Instance.missSFX != null)
            Instance.missSFX.Play();

        Instance.UpdateUI();
    }

    private void EndGame()
    {
        ended = true;

        if (score >= winScoreThreshold)
        {
            SceneManager.LoadScene("WinScene");
        }
        else
        {
            SceneManager.LoadScene("LoseScene");
        }
    }

    private void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {score}";

        if (comboText != null)
            comboText.text = $"Combo: {combo}";
    }
}