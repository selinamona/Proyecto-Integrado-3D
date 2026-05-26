using UnityEngine;

public class BotonSalir : MonoBehaviour
{
    public void SalirDelJuego()
    {
        // Si estamos en el editor de Unity, detiene la reproducción
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Si es un juego compilado (exe, apk, etc.), cierra la aplicación
        Application.Quit();
#endif
    }
}