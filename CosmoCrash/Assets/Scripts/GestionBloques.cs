using UnityEngine;

public class GestionBloques : MonoBehaviour
{
    public int impactosParaDestruir = 1;
    private int impactosRecibidos = 0;
    public int puntosASumar;
    [SerializeField] private PowerUpManager powerUpManager;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Bola"))
        {
            impactosRecibidos++;

            if (impactosRecibidos >= impactosParaDestruir)
            {
                //Debug.Log($"🧱 Bloque destruido en {transform.position}");

                if (powerUpManager != null)
                {
                    powerUpManager.GenerarPowerUp(transform.position); // Generar PowerUp solo cuando se destruye el bloque
                }
                else if (powerUpManager == null)
                {
                    Debug.Log("no manager");
                }

                ControlSingleton.Instance.SumarPuntos(puntosASumar);
                Destroy(gameObject);
            }
        }
    }
}
