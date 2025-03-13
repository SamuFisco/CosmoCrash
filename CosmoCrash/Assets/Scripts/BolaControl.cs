using UnityEngine;

public class BolaControl : MonoBehaviour
{
    public Transform posicionInicial; // Posici�n inicial de la bola
    [Header("Configuraci�n de Velocidad")]
    public float velocidad = 5f; // Velocidad inicial de la bola, ajustable desde el Inspector
    public float velocidadMaxima = 10f; // Velocidad m�xima permitida, ajustable desde el Inspector

    private Rigidbody rb; // Referencia al Rigidbody de la bola

    private void Start()
    {
        // Asegurarse de que la bola comience en la posici�n inicial
        if (posicionInicial != null)
        {
            transform.position = posicionInicial.position;
        }

        // Obtener el Rigidbody de la bola
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Corregir la velocidad para que no sea demasiado peque�a o grande
        Vector3 velocidadActual = rb.velocity;

        // Normalizamos la velocidad para asegurarnos de que no quede en direcciones peque�as
        velocidadActual = velocidadActual.normalized;

        // Ajustamos las componentes X e Y para evitar movimientos demasiado peque�os
        if (Mathf.Abs(velocidadActual.x) < 0.1f) velocidadActual.x = 0.3f * Mathf.Sign(velocidadActual.x); // Ajustamos para X
        if (Mathf.Abs(velocidadActual.y) < 0.1f) velocidadActual.y = 0.3f * Mathf.Sign(velocidadActual.y); // Ajustamos para Y

        // Limitamos la velocidad para que no supere la velocidad m�xima
        velocidadActual = velocidadActual * Mathf.Clamp(velocidadActual.magnitude, velocidad, velocidadMaxima);

        // Asignamos la nueva velocidad al Rigidbody
        rb.velocity = velocidadActual;
    }

    private void OnTriggerEnter(Collider otro)
    {
        // Comprobar si la colisi�n es con un bloque de vida
        if (otro.CompareTag("BloqueVida"))
        {
            // Restar 1 de vida al jugador
            if (ControlSingleton.Instance != null)
            {
                ControlSingleton.Instance.RestarVida(1);
            }

            // Reiniciar la posici�n de la bola si el jugador a�n tiene vidas
            if (ControlSingleton.Instance.vidas > 0 && posicionInicial != null)
            {
                transform.position = posicionInicial.position;

                // Opcional: Reiniciar la velocidad de la bola
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                MovimientoJugador.Instance.ResetBola();
            }
        }
    }

    // Funciona para el sistema de vida y puntaje
    /*private void OnCollisionEnter(Collision otro)
    {
        SistemaDeControlDeBloques bloque = otro.gameObject.GetComponent<SistemaDeControlDeBloques>();
        if (bloque != null)
        {
            bloque.vidas -= 1;

            // Si las vidas del bloque llegan a 0, sumamos puntos y destruimos el bloque
            if (bloque.vidas <= 0)
            {
                if (ControlSingleton.Instance != null)
                {
                    ControlSingleton.Instance.SumarPuntos(bloque.puntaje);
                }

                Destroy(otro.gameObject);
            }
        }
    }*/
}



