using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnTrigger : MonoBehaviour
{
    [Tooltip("Nombre de la escena a la que quieres viajar")]
    public string sceneName = "INTRO";

    [Tooltip("Etiqueta (Tag) del objeto que puede activar este cambio de escena. Déjalo vacío si cualquier objeto puede activarlo.")]
    public string targetTag = "";

    [Tooltip("OPCIONAL: Nombre del objeto (Empty) en la siguiente escena donde debe aparecer el jugador. Ej: 'AparecerDesdeBano'")]
    public string targetSpawnPoint = "";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si no pusimos un Tag específico, o si el objeto que choca tiene el Tag correcto
        if (string.IsNullOrEmpty(targetTag) || collision.gameObject.CompareTag(targetTag))
        {
            // Guardamos el punto de aparición si escribimos uno
            if (!string.IsNullOrEmpty(targetSpawnPoint))
            {
                PlayerPrefs.SetString("TargetSpawnPoint", targetSpawnPoint);
            }

            Debug.Log("¡Cambiando a la escena " + sceneName + "!");
            SceneManager.LoadScene(sceneName);
        }
    }
}
