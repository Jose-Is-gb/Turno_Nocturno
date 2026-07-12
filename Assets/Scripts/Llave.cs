using UnityEngine;

public class Llave : MonoBehaviour
{
    // Memoria global: Todo el juego sabrá si esto es true (tienes la llave) o false
    public static bool tieneLlave = false;

    // Interruptor para saber si el jugador está parado frente a la llave
    private bool jugadorEnRango = false;

    // Escuchamos el teclado en cada frame
    private void Update()
    {
        // Si el jugador está tocando la llave Y presiona la letra E...
        if (jugadorEnRango == true && Input.GetKeyDown(KeyCode.E))
        {
            RecogerLlave();
        }
    }

    // Se activa cuando el jugador ENTRA a la zona de la llave
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorEnRango = true; // El jugador está cerca, puede presionar E
        }
    }

    // Se activa cuando el jugador SALE de la zona de la llave
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorEnRango = false; // El jugador se alejó, la tecla E no hará nada
        }
    }

    // Lógica para recogerla y desaparecerla
    private void RecogerLlave()
    {
        tieneLlave = true; // Le decimos al juego que ya tienes la llave
        Debug.Log("¡Llave recogida!");

        // Destruimos el objeto visual de la llave en la escena
        Destroy(gameObject);
    }
}