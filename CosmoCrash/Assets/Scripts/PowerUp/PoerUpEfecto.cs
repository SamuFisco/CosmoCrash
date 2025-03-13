using System.Collections;
using UnityEngine;

public class PowerUpEfecto : MonoBehaviour
{
    private bool controlesInvertidos = false;
    private float tiempoInversion = 10f;

    public void InvertirControles()
    {
        if (!controlesInvertidos)
        {
            controlesInvertidos = true;
            StartCoroutine(RevertirControles());
        }
    }

    private IEnumerator RevertirControles()
    {
        yield return new WaitForSeconds(tiempoInversion);
        controlesInvertidos = false;
    }

    public float ObtenerDireccion(float input)
    {
        return controlesInvertidos ? -input : input;
    }
}
