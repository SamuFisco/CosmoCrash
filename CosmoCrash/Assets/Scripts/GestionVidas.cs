using UnityEngine;
using UnityEngine.UI;

public class GestionVidas : MonoBehaviour
{
    [Header("Configuraci�n de la Barra de Vida")]
    public Image barraBase; // La imagen base de la barra de vida (fija)

    [Header("Im�genes de Vida dentro del Canvas")]
    public Image vida1; // Imagen de vida 5
    public Image vida2; // Imagen de vida 4
    public Image vida3; // Imagen de vida 3
    public Image vida4; // Imagen de vida 2
    public Image vida5; // Imagen de vida 1

    [Header("Configuraci�n de Vidas")]
    public int totalVidas = 5; // N�mero total de vidas
    private int vidasRestantes; // N�mero de vidas restantes

    [Header("Configuraci�n del Collider")]
    public string etiquetaCollider = "BarraInferior"; // Etiqueta para el BoxCollider que descuenta vida

    private void Start()
    {
        // Inicializamos las vidas restantes
        vidasRestantes = totalVidas;

        // Aseguramos que la barra de vida est� completa al inicio
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

    // M�todo para perder una vida
    private void PerderVida()
    {
        if (vidasRestantes > 0)
        {
            vidasRestantes--; // Disminuir el n�mero de vidas restantes
            ActualizarBarraDeVida(); // Actualizar las im�genes de la barra de vida
        }

        // Si se quedan sin vidas, puedes activar la l�gica de fin de juego
        if (vidasRestantes == 0)
        {
            Debug.Log("Game Over!");
            // Aqu� podr�as implementar la l�gica para terminar el juego o reiniciarlo.
        }
    }

    // Actualiza las im�genes de la barra de vida seg�n las vidas restantes
    private void ActualizarBarraDeVida()
    {
        // Ocultar todas las im�genes de vida primero
        vida1.enabled = false;
        vida2.enabled = false;
        vida3.enabled = false;
        vida4.enabled = false;
        vida5.enabled = false;

        // Mostrar las im�genes seg�n el n�mero de vidas restantes
        if (vidasRestantes == 5) vida1.enabled = true;
        if (vidasRestantes >= 4) vida2.enabled = true;
        if (vidasRestantes >= 3) vida3.enabled = true;
        if (vidasRestantes >= 2) vida4.enabled = true;
        if (vidasRestantes >= 1) vida5.enabled = true;
    }
}



