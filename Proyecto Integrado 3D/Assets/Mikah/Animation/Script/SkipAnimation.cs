using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipAnimation : MonoBehaviour
{
    [Header("Configuración")]
    public int siguienteEscenaIndex = 1; // Índice de la siguiente escena
    public float tiempoMinimo = 2f; // Tiempo mínimo antes de poder saltar (opcional)

    private float tiempoInicio;
    private bool puedeSaltar = false;

    void Start()
    {
        tiempoInicio = Time.time;

        // Opcional: desbloquear salto después de X segundos
        Invoke("ActivarSalto", tiempoMinimo);
    }

    void Update()
    {
        // Detectar tecla ESPACIO
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SaltarAnimacion();
        }

        // Opcional: cualquier tecla
        if (Input.anyKeyDown && Input.GetKeyDown(KeyCode.Space) == false)
        {
            // Esto es para que cualquier tecla también salte (opcional)
            // SaltarAnimacion();
        }
    }

    void ActivarSalto()
    {
        puedeSaltar = true;
    }

    public void SaltarAnimacion()
    {
        // Si quieres tiempo mínimo obligatorio, usa esta línea
        // if (Time.time - tiempoInicio < tiempoMinimo) return;

        // Cargar siguiente escena
        SceneManager.LoadScene(siguienteEscenaIndex);
    }
}