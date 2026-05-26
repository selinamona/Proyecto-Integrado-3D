using UnityEngine;
using UnityEngine.SceneManagement;

public class ReiniciarEscena : MonoBehaviour
{
    // Este método reinicia el nivel cargando específicamente "RapScene"
    public void ReiniciarNivel()
    {
        // Carga la escena llamada "RapScene"
        SceneManager.LoadScene("RapScene");
    }
}