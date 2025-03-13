using UnityEngine;

public class PowerUpMovimiento : MonoBehaviour
{
    [SerializeField] private float velocidad = 40f;
    private Camera camara;

    private void Start()
    {
        camara = Camera.main;

        // Si el PowerUp existe en la escena al iniciar, lo destruimos (Evita PowerUps preexistentes)
        /*if (transform.parent == null)
        {
            Debug.LogWarning($"❌ Eliminando PowerUp preexistente en {transform.position}");
            Destroy(gameObject);
        }*/
    }

    private void Update()
    {
        transform.position += Vector3.down * velocidad * Time.deltaTime;

        Vector3 posicionEnPantalla = camara.WorldToViewportPoint(transform.position);
        if (posicionEnPantalla.y < -40f)
        {
            Debug.LogError($"❌ PowerUp eliminado por salir de la pantalla en: {transform.position}");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Paddle"))
        {
            Debug.Log("🎮 PowerUp recogido por el Paddle!");
            MovimientoJugador.Instance.InvertirControles();
            Destroy(gameObject);
        }
    }
}
