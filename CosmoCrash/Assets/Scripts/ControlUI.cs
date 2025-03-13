using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class ControlUI : MonoBehaviour
{
    public static ControlUI instance;

    public RectTransform panelMenuLateral;
    public RectTransform panelOpciones;
    public Button botonInicio;
    public Button botonOpciones;
    public Button botonSalir;
    public Image imagenFondo;
    public Button botonNuevaPartida;
    public GameObject player;
    public GameObject bola;
    public TextMeshProUGUI textoTiempo;
    public Image imagenMenu;
    public float velocidad = 1f;
    public float distancia = 100f;
    public RectTransform canvasGameOver; // El Canvas de Game Over
    public RectTransform canvasGanador; // Arrastra el Canvas de Ganador aquí en el Inspector

    private float tiempoActual = 0f;
    public bool juegoIniciado = false;

    [SerializeField] private AudioMixer audioMixer;

    private Vector2 posicionInicialMenuLateral = new Vector2(-1.999817f, -670);
    private Vector2 posicionFinalMenuLateral = new Vector2(-1.999817f, -7);
    private Vector2 posicionInicialOpciones = new Vector2(-1.999817f, -1466);
    private Vector2 posicionFinalOpciones = new Vector2(-1.999817f, -7);

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        panelMenuLateral.anchoredPosition = posicionInicialMenuLateral;
        panelOpciones.anchoredPosition = posicionInicialOpciones;
        panelOpciones.gameObject.SetActive(false);
        botonNuevaPartida.gameObject.SetActive(false);
        player.SetActive(false);
        bola.SetActive(false);

        Vector2 posicionInicialImagen = imagenMenu.rectTransform.anchoredPosition;
        Vector2 posicionFinalImagen = new Vector2(
            imagenMenu.rectTransform.anchoredPosition.x,
            imagenMenu.rectTransform.anchoredPosition.y + distancia);

        LeanTween.move(imagenMenu.rectTransform, posicionFinalImagen, velocidad)
            .setLoopPingPong()
            .setEase(LeanTweenType.easeInOutSine);
    }

    private void Update()
    {
        if (juegoIniciado)
        {
            tiempoActual += Time.deltaTime;
            textoTiempo.text = $"Tiempo: {tiempoActual:F1}s";
        }
    }
    // Método para mostrar el Canvas de Ganador
    public void MostrarCanvasGanador()
    {
        if (canvasGanador != null)
        {
            canvasGanador.gameObject.SetActive(true);
        }
    }
    public void Reintentar()
    {
        canvasGameOver.gameObject.SetActive(false);

        bola.transform.parent = player.transform;
        bola.transform.localPosition = new Vector3(0f, -1.197144e-09f, 0.02000001f);

        tiempoActual = 0f;
        juegoIniciado = false;

        ControlSingleton.Instance.reiniciarCronometro();
        ControlSingleton.Instance.reinciarPuntos();
        MovimientoJugador.Instance.ResetBola();
        ControlSingleton.Instance.reiniciarVida();
        ControlSingleton.Instance.restaurarBloques();
        


        Debug.Log("Juego reiniciado y cronómetro reseteado.");
    }

    public void MostrarMenu()
    {
        LeanTween.move(panelMenuLateral, posicionFinalMenuLateral, 0.5f).setEase(LeanTweenType.easeInOutQuad);
        botonInicio.gameObject.SetActive(false);
        imagenFondo.gameObject.SetActive(false);
        botonNuevaPartida.gameObject.SetActive(true);
    }

    public void IniciarNuevaPartida()
    {
        player.SetActive(true);
        bola.SetActive(true);

        LeanTween.move(panelMenuLateral, posicionInicialMenuLateral, 0.5f).setEase(LeanTweenType.easeInOutQuad);

        tiempoActual = 0f;
        juegoIniciado = false;
    }

    public void MostrarOpciones()
    {
        panelMenuLateral.anchoredPosition = posicionInicialMenuLateral;
        panelOpciones.gameObject.SetActive(true);
        panelOpciones.anchoredPosition = posicionFinalOpciones;
    }

    public void CerrarOpciones()
    {
        panelOpciones.anchoredPosition = posicionInicialOpciones;
        panelOpciones.gameObject.SetActive(false);
        panelMenuLateral.anchoredPosition = posicionFinalMenuLateral;
    }

    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void PantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }

    public void CambiarVolumen(float volumen)
    {
        audioMixer.SetFloat("Volumen", volumen);
    }

    public void CambiarCalidad(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }

    // Método para mostrar el canvas de Game Over
    public void MostrarCanvasGameOver()
    {
        // Activar el canvas de Game Over
        canvasGameOver.gameObject.SetActive(true);
    }
}

