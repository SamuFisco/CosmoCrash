using UnityEngine;

public class ReiniciarBloques : MonoBehaviour
{
    // Prefabs de los bloques por categor�a
    public GameObject[] prefabBloques; // Tienes 6 tipos de bloques (Bloque1, Bloque2, ..., Bloque6)

    // �rea de generaci�n (donde colocamos los nuevos bloques)
    public Transform[] posicionesGeneracion; // Puedes definir posiciones en el Inspector

    // N�mero m�nimo de bloques que quieres (en este caso 30)
    public int numeroMinimoBloques = 30;

    public void Reintentar()
    {
        // Obtener los bloques actuales
        GameObject[] bloquesActuales = GameObject.FindGameObjectsWithTag("Bloque");

        // Contar cu�ntos bloques existen de cada categor�a (Bloque1, Bloque2, ..., Bloque6)
        int[] cantidadBloquesPorCategoria = new int[6]; // Hay 6 categor�as (Bloque1, Bloque2, ..., Bloque6)

        foreach (var bloque in bloquesActuales)
        {
            // Comprobar la etiqueta de cada bloque para asignarlo a su categor�a
            for (int i = 0; i < 6; i++)
            {
                if (bloque.CompareTag($"Bloque{i + 1}")) // Si la etiqueta del bloque es Bloque1, Bloque2, ..., Bloque6
                {
                    cantidadBloquesPorCategoria[i]++;
                    break;
                }
            }
        }

        // Calcular cu�ntos bloques faltan
        int bloquesFaltantes = numeroMinimoBloques - bloquesActuales.Length;

        if (bloquesFaltantes > 0)
        {
            // Generamos bloques faltantes
            for (int i = 0; i < 6; i++)
            {
                int cantidadFaltanteCategoria = 5 - cantidadBloquesPorCategoria[i]; // 5 bloques por categor�a

                // Si faltan bloques de esa categor�a, los generamos
                for (int j = 0; j < cantidadFaltanteCategoria; j++)
                {
                    // Generar un bloque de la categor�a correspondiente
                    if (prefabBloques.Length > i)
                    {
                        GenerarBloque(i); // Genera el bloque correspondiente seg�n la categor�a
                    }
                }
            }

            Debug.Log($"Se generaron bloques para completar las categor�as.");
        }
        else
        {
            Debug.Log("Ya tienes suficientes bloques.");
        }
    }

    private void GenerarBloque(int categoriaIndex)
    {
        // Verificar si hay posiciones disponibles para generar el bloque
        if (posicionesGeneracion.Length > 0)
        {
            // Generar un bloque en una posici�n aleatoria de las disponibles
            Transform posicion = posicionesGeneracion[Random.Range(0, posicionesGeneracion.Length)];
            Instantiate(prefabBloques[categoriaIndex], posicion.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No hay posiciones de generaci�n configuradas.");
        }
    }
}

