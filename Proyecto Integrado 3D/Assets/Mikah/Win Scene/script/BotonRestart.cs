using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonRestart : MonoBehaviour
{
    public void VolverAlMenu()
    {
        SceneManager.LoadScene(0);  // ← Carga la escena en posición 0
    }
}