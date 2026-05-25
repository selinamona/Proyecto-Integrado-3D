using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("Audio")]
    public AudioSource hitSFX;
    public AudioSource missSFX;

    [Header("UI")]
    public TextMeshPro scoreText;
    public TextMeshPro comboText;

    private static int score;
    private static int combo;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
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

        UpdateUI();
    }

    public static void Hit()
    {
        combo++;

        score += 100;

        if (Instance.hitSFX != null)
        {
            Instance.hitSFX.Play();
        }

        Instance.UpdateUI();
    }

    public static void Miss()
    {
        combo = 0;

        if (Instance.missSFX != null)
        {
            Instance.missSFX.Play();
        }

        Instance.UpdateUI();
    }

    private void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }

        if (comboText != null)
        {
            comboText.text = "Combo: " + combo;
        }
    }
}