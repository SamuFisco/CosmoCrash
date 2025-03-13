using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlSingleton : MonoBehaviour
{
    // Instancia �nica para implementar el patr�n Singleton
    public static ControlSingleton Instance;

    [Header("Puntuaci�n y tiempo")]
    public int puntuacion = 0; // Puntuaci�n actual del jugador
    public float tiempoTranscurrido = 0f; // Tiempo que ha pasado desde el inicio del juego
    public TextMeshProUGUI textoPuntuacion; // Referencia al texto que muestra la puntuaci�n en la UI
    public TextMeshProUGUI textoTiempo; // Referencia al texto que muestra el tiempo en la UI
    [SerializeField] GameObject gameOver; // Referencia al objeto de Game Over (UI)

    // Contador para llevar el registro de los impactos a los bloques
    private int contadorImpactos;

    // Variable para contar cu�ntos bloques quedan en la escena
    public int bloquesRestantes;

    // Referencia al objeto de imagen que indica al ganador
    public GameObject imagenGanador;

    [Header("Vidas")]
    public int vidas = 5; // N�mero inicial de vidas del jugador
    public Image barraDeVida; // Barra visual para mostrar las vidas en la UI

    [Header("PowerUps")]
    [SerializeField] private GameObject powerUpPrefab;
    private float probabilidadMin = 0.2f;
    private float probabilidadMax = 0.3f;

    private void Awake()
    {
        // Implementaci�n del patr�n Singleton
        if (Instance == null)
        {
            Instance = this; // Se establece esta instancia como la principal
            DontDestroyOnLoad(gameObject); // Se asegura que no se destruya al cambiar de escena
        }
        else
        {
            Destroy(gameObject); // Si ya existe una instancia, se destruye esta copia
        }
    }

    private void Start()
    {
        // Inicializamos el contador de impactos en 0
        contadorImpactos = 0;

        // Contamos los bloques restantes al inicio del juego
        if (bloquesRestantes == 0)
        {
            bloquesRestantes = FindObjectsOfType<GestionBloques>().Length; // Busca todos los bloques en la escena
        }

        // Aseguramos que la imagen del ganador est� desactivada al inicio del juego
        if (imagenGanador != null)
        {
            imagenGanador.SetActive(false);
        }
    }

    private void Update()
    {
        // Si el juego est� iniciado (seg�n el ControlUI)
        if (ControlUI.instance.juegoIniciado)
        {
            // Se actualiza el cron�metro
            tiempoTranscurrido += Time.deltaTime;

            // Convertimos el tiempo transcurrido en minutos, segundos y milisegundos
            int minutos = Mathf.FloorToInt(tiempoTranscurrido / 60);
            int segundos = Mathf.FloorToInt(tiempoTranscurrido % 60);
            int milisegundos = Mathf.FloorToInt((tiempoTranscurrido * 1000) % 1000);

            // Mostramos el tiempo en el formato adecuado
            textoTiempo.text = $"{minutos:00}:{segundos:00}:{milisegundos:000}";
        }
    }

    // M�todo para generar PowerUp con probabilidad al destruir un bloque
    public void GenerarPowerUp(Vector3 posicion)
    {
        float probabilidad = Random.Range(0f, 1f);
        float umbral = Random.Range(probabilidadMin, probabilidadMax);

        if (probabilidad <= umbral) // Aparece el PowerUp con un 20-30% de probabilidad
        {
            Instantiate(powerUpPrefab, posicion, Quaternion.identity);
        }
    }

    // M�todo para reiniciar el cron�metro
    public void reiniciarCronometro()
    {
        tiempoTranscurrido = 0f; // Reinicia el tiempo a cero
        ControlUI.instance.juegoIniciado = false; // Detiene el estado del juego iniciado

        // Actualiza el texto del cron�metro a 0
        int minutos = 0, segundos = 0, milisegundos = 0;
        textoTiempo.text = $"{minutos:00}:{segundos:00}:{milisegundos:000}";
    }

    // M�todo para sumar puntos
    public void SumarPuntos(int puntos)
    {
        puntuacion += puntos; // Agrega los puntos
        ActualizarPuntuacion(puntuacion); // Actualiza la UI
    }

    // M�todo para reiniciar la puntuaci�n
    public void reinciarPuntos()
    {
        puntuacion = 0; // Reinicia la puntuaci�n a 0
        ActualizarPuntuacion(puntuacion); // Actualiza la UI
    }

    // Suma puntos seg�n la etiqueta del bloque
    public void SumarPuntosPorEtiqueta(string etiquetaBloque)
    {
        int puntosGanados = 0;

        // Asigna puntos dependiendo de la etiqueta del bloque
        switch (etiquetaBloque)
        {
            case "Bloque1": puntosGanados = 2; break;
            case "Bloque2": puntosGanados = 4; break;
            case "Bloque3": puntosGanados = 6; break;
            case "Bloque4": puntosGanados = 8; break;
            case "Bloque5": puntosGanados = 10; break;
            case "Bloque6": puntosGanados = 12; break;
            default: Debug.LogWarning($"Etiqueta desconocida: {etiquetaBloque}"); break;
        }

        SumarPuntos(puntosGanados); // Suma los puntos calculados
    }

    // M�todo para restaurar bloques desactivados
    public void restaurarBloques()
    {
        GameObject[] bloques = GameObject.FindGameObjectsWithTag("Bloque"); // Busca bloques por etiqueta

        foreach (GameObject bloque in bloques)
        {
            bloque.SetActive(true); // Reactiva cada bloque encontrado
        }

        Debug.Log("Todos los bloques han sido restaurados.");
    }

    // M�todo para actualizar la puntuaci�n en la UI
    private void ActualizarPuntuacion(int sumaPuntosText)
    {
        if (textoPuntuacion != null)
        {
            textoPuntuacion.text = $"Puntuaci�n: {sumaPuntosText}";
        }
    }

    // M�todo para restar vidas
    public void RestarVida(int vidaActual)
    {
        vidas -= vidaActual; // Reduce las vidas

        if (vidas <= 0)
        {
            GameOver(); // Si las vidas llegan a 0, activa Game Over
            barraDeVida.fillAmount = 0; // Vac�a la barra de vida
        }
        else if (barraDeVida != null)
        {
            barraDeVida.fillAmount = (float)vidas / 5f; // Actualiza la barra de vida proporcionalmente
        }
    }

    // M�todo para reiniciar las vidas
    public void reiniciarVida()
    {
        vidas = 5; // Restablece las vidas al m�ximo
        barraDeVida.fillAmount = 1f; // Llena la barra de vida completamente

        if (gameOver.activeSelf)
        {
            gameOver.SetActive(false); // Si el estado de Game Over est� activo, lo desactiva
        }

        Debug.Log("Vidas reiniciadas y barra de vida llena.");
    }

    // M�todo para manejar el estado de Game Over
    private void GameOver()
    {
        Debug.Log("Game Over");
        gameOver.SetActive(true); // Activa la UI de Game Over

        ControlUI.instance.juegoIniciado = false; // Detiene el estado del juego
        Debug.Log("�Game Over!");
    }
}
