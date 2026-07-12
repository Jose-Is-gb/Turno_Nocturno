using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class Puerta : MonoBehaviour
{
    public string nombreEscenaDestino = "InteriorMuseo";
    public GameObject nubeFondo;
    public TextMeshProUGUI textoEnPantalla;
    public float tiempoVisible = 3f;

    private bool jugadorEnRango = false;

    private void Update()
    {
        // 2. Revisamos si presionas la E
        if (jugadorEnRango == true && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("PASO 2: ¡El jugador presionó la tecla E!");
            IntentarAbrirPuerta();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 0. Revisamos CUALQUIER cosa que toque la puerta
        Debug.Log("ALGO TOCÓ LA PUERTA. Objeto: " + collision.gameObject.name + " | Etiqueta (Tag): " + collision.gameObject.tag);

        // 1. Revisamos si el que tocó fue el Player
        if (collision.CompareTag("Player"))
        {
            jugadorEnRango = true;
            Debug.Log("PASO 1: ¡El Player está en rango! Ya puede presionar E.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorEnRango = false;
            Debug.Log("El Player se alejó de la puerta.");
            if (nubeFondo != null) nubeFondo.SetActive(false);
        }
    }

    private void IntentarAbrirPuerta()
    {
        if (Llave.tieneLlave == true)
        {
            Debug.Log("PASO 3: Tiene la llave. Abriendo escena...");
            Llave.tieneLlave = false;
            SceneManager.LoadScene(nombreEscenaDestino);
        }
        else
        {
            Debug.Log("PASO 3: NO tiene llave. Intentando mostrar la nube...");
            if (nubeFondo != null && textoEnPantalla != null)
            {
                StopAllCoroutines();
                StartCoroutine(MostrarMensaje("La puerta está cerrada con llave. Necesitas buscarla."));
            }
            else
            {
                Debug.LogError("ERROR: Te faltó arrastrar la Nube o el Texto al Inspector de la Puerta.");
            }
        }
    }

    private IEnumerator MostrarMensaje(string mensaje)
    {
        textoEnPantalla.text = mensaje;
        nubeFondo.SetActive(true);
        Debug.Log("PASO 4: ¡La nube se encendió en el código!");

        yield return new WaitForSeconds(tiempoVisible);

        nubeFondo.SetActive(false);
    }
}