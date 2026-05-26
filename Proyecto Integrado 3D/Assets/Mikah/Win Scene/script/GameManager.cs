using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int puntuacion = 0;

    [Header("WinScene")]
    public string nombreWinScene = "WinScene"; // Nombre de la escena de victoria
    public int puntuacionRequerida = 5500;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SumarPuntos(int puntos)
    {
        puntuacion += puntos;
        Debug.Log("Puntuación actual: " + puntuacion);

        if (puntuacion > puntuacionRequerida)
        {
            GanarPartida();
        }
    }

    void GanarPartida()
    {
        Debug.Log("¡Has ganado! Puntuación: " + puntuacion);
        SceneManager.LoadScene(nombreWinScene);
    }
}