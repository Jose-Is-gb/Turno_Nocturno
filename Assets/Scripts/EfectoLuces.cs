using UnityEngine;
using System.Collections;

public class EfectoLuces : MonoBehaviour
{
    [Header("Configuración del Apagón")]
    [Tooltip("Tiempo en segundos antes de que empiecen a fallar las luces")]
    public float tiempoParaFallo = 20f;

    [Tooltip("Qué tan oscuro se pondrá (0 es negro total, 1 es normal)")]
    public float oscuridad = 0.2f;

    [Header("Escenario a oscurecer")]
    [Tooltip("Arrastra aquí los objetos de tu escenario (fondos, paredes, etc.)")]
    public SpriteRenderer[] partesDelEscenario;

    private Color colorOscuro;

    private void Start()
    {
        // Configuramos el color oscuro basado en el valor que elijas
        colorOscuro = new Color(oscuridad, oscuridad, oscuridad, 1f);

        // Iniciamos el contador en cuanto el jugador entra a esta escena
        StartCoroutine(RutinaParpadeo());
    }

    private IEnumerator RutinaParpadeo()
    {
        // 1. El juego espera en total silencio los 20 segundos
        yield return new WaitForSeconds(tiempoParaFallo);

        // 2. Comienza el terror (Bucle infinito de parpadeo)
        while (true)
        {
            // Luz AFUERA (Oscurecemos todo)
            CambiarColorEscenario(colorOscuro);

            // Espera un tiempo aleatorio muy cortito (ej. entre 0.05 y 0.3 segundos)
            yield return new WaitForSeconds(Random.Range(0.05f, 0.3f));

            // Luz ADENTRO (Volvemos a la normalidad)
            CambiarColorEscenario(Color.white);

            // Espera un tiempo aleatorio antes del siguiente parpadeo (ej. entre 0.1 y 1.5 segundos)
            yield return new WaitForSeconds(Random.Range(0.1f, 1.5f));
        }
    }

    private void CambiarColorEscenario(Color nuevoColor)
    {
        // Este código recorre todos los objetos que arrastraste y les cambia el color a la vez
        foreach (SpriteRenderer sprite in partesDelEscenario)
        {
            if (sprite != null)
            {
                sprite.color = nuevoColor;
            }
        }
    }
}