using UnityEngine;

public class VictoriaManager : MonoBehaviour
{
    // Referencia a la imagen de victoria
    public GameObject imagenVictoria;

    private void Update()
    {
        // Accede a la puntuación actual desde ControlSingleton
        if (ControlSingleton.Instance != null)
        {
            int puntuacionActual = ControlSingleton.Instance.puntuacion;

            // Verifica si la puntuación es igual o mayor a 1050
            if (puntuacionActual >= 1050)
            {
                ActivarImagenVictoria();
                DetenerTiempo(); // Llama al método para detener el tiempo
            }
        }
    }

    private void ActivarImagenVictoria()
    {
        // Activa la imagen de victoria y sus hijos
        if (imagenVictoria != null && !imagenVictoria.activeSelf)
        {
            imagenVictoria.SetActive(true);
            foreach (Transform child in imagenVictoria.transform)
            {
                child.gameObject.SetActive(true);
            }
            Debug.Log("¡Victoria! Imagen activada y tiempo detenido.");
        }
    }

    private void DetenerTiempo()
    {
        // Detiene el tiempo del juego
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
            Debug.Log("Tiempo detenido.");
        }
    }
}
