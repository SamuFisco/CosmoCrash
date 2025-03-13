using UnityEngine;
using UnityEngine.UI;

public class GestionVidas : MonoBehaviour
{
    [Header("Configuración de la Barra de Vida")]
    public Image barraBase; // La imagen base de la barra de vida (fija)

    [Header("Imágenes de Vida dentro del Canvas")]
    public Image vida1; // Imagen de vida 5
    public Image vida2; // Imagen de vida 4
    public Image vida3; // Imagen de vida 3
    public Image vida4; // Imagen de vida 2
    public Image vida5; // Imagen de vida 1

    [Header("Configuración de Vidas")]
    public int totalVidas = 5; // Número total de vidas
    private int vidasRestantes; // Número de vidas restantes

    [Header("Configuración del Collider")]
    public string etiquetaCollider = "BarraInferior"; // Etiqueta para el BoxCollider que descuenta vida

    private void Start()
    {
        // Inicializamos las vidas restantes
        vidasRestantes = totalVidas;

        // Aseguramos que la barra de vida esté completa al inicio
        ActualizarBarraDeVida();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verificamos si la bola entra en contacto con un collider con la etiqueta "BarraInferior"
        if (collision.gameObject.CompareTag(etiquetaCollider))
        {
            PerderVida(); // Se pierde una vida
        }
    }

    // Método para perder una vida
    private void PerderVida()
    {
        if (vidasRestantes > 0)
        {
            vidasRestantes--; // Disminuir el número de vidas restantes
            ActualizarBarraDeVida(); // Actualizar las imágenes de la barra de vida
        }

        // Si se quedan sin vidas, puedes activar la lógica de fin de juego
        if (vidasRestantes == 0)
        {
            Debug.Log("Game Over!");
            // Aquí podrías implementar la lógica para terminar el juego o reiniciarlo.
        }
    }

    // Actualiza las imágenes de la barra de vida según las vidas restantes
    private void ActualizarBarraDeVida()
    {
        // Ocultar todas las imágenes de vida primero
        vida1.enabled = false;
        vida2.enabled = false;
        vida3.enabled = false;
        vida4.enabled = false;
        vida5.enabled = false;

        // Mostrar las imágenes según el número de vidas restantes
        if (vidasRestantes == 5) vida1.enabled = true;
        if (vidasRestantes >= 4) vida2.enabled = true;
        if (vidasRestantes >= 3) vida3.enabled = true;
        if (vidasRestantes >= 2) vida4.enabled = true;
        if (vidasRestantes >= 1) vida5.enabled = true;
    }
}



