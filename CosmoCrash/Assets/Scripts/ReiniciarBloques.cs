using UnityEngine;

public class ReiniciarBloques : MonoBehaviour
{
    // Prefabs de los bloques por categoría
    public GameObject[] prefabBloques; // Tienes 6 tipos de bloques (Bloque1, Bloque2, ..., Bloque6)

    // Área de generación (donde colocamos los nuevos bloques)
    public Transform[] posicionesGeneracion; // Puedes definir posiciones en el Inspector

    // Número mínimo de bloques que quieres (en este caso 30)
    public int numeroMinimoBloques = 30;

    public void Reintentar()
    {
        // Obtener los bloques actuales
        GameObject[] bloquesActuales = GameObject.FindGameObjectsWithTag("Bloque");

        // Contar cuántos bloques existen de cada categoría (Bloque1, Bloque2, ..., Bloque6)
        int[] cantidadBloquesPorCategoria = new int[6]; // Hay 6 categorías (Bloque1, Bloque2, ..., Bloque6)

        foreach (var bloque in bloquesActuales)
        {
            // Comprobar la etiqueta de cada bloque para asignarlo a su categoría
            for (int i = 0; i < 6; i++)
            {
                if (bloque.CompareTag($"Bloque{i + 1}")) // Si la etiqueta del bloque es Bloque1, Bloque2, ..., Bloque6
                {
                    cantidadBloquesPorCategoria[i]++;
                    break;
                }
            }
        }

        // Calcular cuántos bloques faltan
        int bloquesFaltantes = numeroMinimoBloques - bloquesActuales.Length;

        if (bloquesFaltantes > 0)
        {
            // Generamos bloques faltantes
            for (int i = 0; i < 6; i++)
            {
                int cantidadFaltanteCategoria = 5 - cantidadBloquesPorCategoria[i]; // 5 bloques por categoría

                // Si faltan bloques de esa categoría, los generamos
                for (int j = 0; j < cantidadFaltanteCategoria; j++)
                {
                    // Generar un bloque de la categoría correspondiente
                    if (prefabBloques.Length > i)
                    {
                        GenerarBloque(i); // Genera el bloque correspondiente según la categoría
                    }
                }
            }

            Debug.Log($"Se generaron bloques para completar las categorías.");
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
            // Generar un bloque en una posición aleatoria de las disponibles
            Transform posicion = posicionesGeneracion[Random.Range(0, posicionesGeneracion.Length)];
            Instantiate(prefabBloques[categoriaIndex], posicion.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No hay posiciones de generación configuradas.");
        }
    }
}

