using UnityEngine;

public class RegenerarBloquesSimple : MonoBehaviour
{
    public GameObject[] padresCategorias; // Padres desde Bloques1 a Bloques6 en el Inspector

    public void Reintentar()
    {
        // Desactiva y vuelve a activar el padre de todos los bloques para reiniciar
        foreach (GameObject padre in padresCategorias)
        {
            padre.SetActive(false); // Desactiva la categor�a
            padre.SetActive(true);  // Reactiva la categor�a
        }

        // Reiniciar la l�gica de los bloques restantes
        ControlSingleton.Instance.bloquesRestantes = CalcularBloquesRestantes();
        Debug.Log("Se han regenerado los bloques.");
    }

    // Calcula cu�ntos bloques quedan activos en la escena
    private int CalcularBloquesRestantes()
    {
        return FindObjectsOfType<GestionBloques>().Length;
    }
}
