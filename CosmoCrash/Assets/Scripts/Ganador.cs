using UnityEngine;

public class Ganador : MonoBehaviour
{
    // Referencia al Canvas de Ganador
    public GameObject imagenGanador;

    // Variable estática para el patrón Singleton
    public static Ganador Instance;

    // Variable para contar cuántos bloques quedan
    public int bloquesRestantes; // Contador de bloques restantes

    private void Awake()
    {
        // Verificamos si la instancia de 'Ganador' ya existe
        if (Instance == null)
        {
            Instance = this; // Asignamos la instancia
            DontDestroyOnLoad(gameObject); // Esto hace que persista entre escenas, si es necesario
        }
        else
        {
            Destroy(gameObject); // Si ya existe una instancia, destruimos este objeto
        }
    }

    private void Start()
    {
        // Verificamos si no quedan bloques
        bloquesRestantes = FindObjectsOfType<GestionBloques>().Length; // Asigna el número de bloques restantes
        //if (bloquesRestantes == 0) // Compara si no quedan bloques
        //{
            //MostrarImagenGanador(); // Si no quedan bloques, mostramos la imagen de ganador
        //}
    }

    // Método para mostrar la imagen de ganador
    private void MostrarImagenGanador()
    {
        if (imagenGanador != null)
        {
            imagenGanador.SetActive(true); // Activamos la imagen de ganador
        }
    }
}
