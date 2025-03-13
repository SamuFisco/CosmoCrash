using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vida : MonoBehaviour
{
    public Image vida; // Imagen de la barra de vida

    public float vidaActual = 5f; // Vida inicial
    public float vidaMaxima = 5f; // Vida m�xima

    public GameObject gameOverPanel; // Panel de Game Over

    private void Start()
    {
        // Asegurarse de que la barra de vida est� llena al iniciar
        vida.fillAmount = 1f;

        // Asegurarse de que el panel de Game Over est� desactivado
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    // M�todo para restar vida
    public void RestarVida(float cantidad)
    {
        vidaActual -= cantidad; // Reducir la vida
        vida.fillAmount = vidaActual / vidaMaxima; // Actualizar la barra de vida

        // Comprobar si la vida llega a 0
        if (vidaActual <= 0)
        {
            vidaActual = 0;
            GameOver(); // Llamar a Game Over
        }
    }

    // M�todo para manejar el Game Over
    private void GameOver()
    {
        Debug.Log("Game Over!");
        // Activar el panel de Game Over si est� asignado
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }
}
