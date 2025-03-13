using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private GameObject powerUpPrefab;
    [SerializeField] private float probabilidadMin = 0.2f;
    [SerializeField] private float probabilidadMax = 0.3f;
    private Camera camara;

    private void Start()
    {
        camara = Camera.main;
    }

    public void GenerarPowerUp(Vector3 posicionBloque)
    {
        float probabilidad = Random.Range(0f, 1f);
        float umbral = Random.Range(probabilidadMin, probabilidadMax);

        Debug.Log(" Probabilidad calculada: {probabilidad} (Umbral: {umbral})");

        if (probabilidad <= umbral)
        {
            Vector3 posicionAjustada = new Vector3(posicionBloque.x, posicionBloque.y + 1.5f, posicionBloque.z);

            Vector3 posEnPantalla = camara.WorldToViewportPoint(posicionAjustada);
            if (posEnPantalla.y < 0.2f || posEnPantalla.y > 0.9f)
            {
                posicionAjustada.y = camara.ViewportToWorldPoint(new Vector3(0.5f, 0.8f, camara.nearClipPlane)).y;
                Debug.LogWarning(" Ajustando PowerUp dentro de la pantalla: {posicionAjustada}");
            }

            Debug.Log(" PowerUp generado correctamente en: {posicionAjustada}");
            Instantiate(powerUpPrefab, posicionAjustada, Quaternion.identity);
        }
    }
}

