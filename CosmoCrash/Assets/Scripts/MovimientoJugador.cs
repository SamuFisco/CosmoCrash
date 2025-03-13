using System.Collections;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public static MovimientoJugador Instance;
    public float velocidadMovimiento = 1.5f;
    public GameObject bola;
    public float velocidadInicialBola = 10f;

    private Rigidbody _rb;
    private bool bolaEnMovimiento = false;
    private bool controlesInvertidos = false; // Variable para el PowerUp de inversión

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        ResetBola(); // ✅ Aseguramos que la bola se reinicia correctamente al inicio
    }

    private void FixedUpdate()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");

        if (controlesInvertidos)
        {
            inputHorizontal *= -1; // Invertir controles si el PowerUp está activo
            Debug.Log("🔄 Controles invertidos");
        }

        _rb.velocity = new Vector3(inputHorizontal * velocidadMovimiento, 0, 0);
    }

    private void Update()
    {
        if (!bolaEnMovimiento && Input.GetKeyDown(KeyCode.Space))
        {
            ControlUI.instance.juegoIniciado = true;
            SoltarBola();
        }
    }

    private void SoltarBola()
    {
        bola.transform.SetParent(null);
        Rigidbody rbBola = bola.GetComponent<Rigidbody>();

        if (rbBola != null)
        {
            rbBola.isKinematic = false;
            rbBola.velocity = Vector3.up * velocidadInicialBola;
        }

        bolaEnMovimiento = true;
    }

    // ✅ Método agregado para corregir el error CS0103
    public void ResetBola()
    {
        if (bola == null)
        {
            Debug.LogError("⚠️ No se ha asignado la bola en el Inspector.");
            return;
        }

        bola.SetActive(true); // Asegurar que la bola está activa
        bola.transform.SetParent(this.transform);

        Rigidbody rbBola = bola.GetComponent<Rigidbody>();
        if (rbBola != null)
        {
            rbBola.isKinematic = true;
            rbBola.velocity = Vector3.zero;
        }

        bolaEnMovimiento = false;
        Debug.Log("🔄 Bola reiniciada.");
    }

    // 🟢 PowerUp: Invertir controles por 10 segundos
    public void InvertirControles()
    {
        Debug.Log("🟢 Activando PowerUp: Controles Invertidos.");
        StartCoroutine(InvertirControlesTemporalmente());
    }

    private IEnumerator InvertirControlesTemporalmente()
    {
        controlesInvertidos = true;
        yield return new WaitForSeconds(10f);
        controlesInvertidos = false;
        Debug.Log("🔄 Controles restaurados.");
    }
}
