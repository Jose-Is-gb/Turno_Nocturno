using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnTrigger : MonoBehaviour
{
    [Tooltip("Nombre de la escena a la que quieres viajar")]
    public string sceneName = "INTRO";

    [Tooltip("Etiqueta (Tag) del objeto que puede activar este cambio de escena. Déjalo vacío si cualquier objeto puede activarlo.")]
    public string targetTag = "";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si no pusimos un Tag específico, o si el objeto que choca tiene el Tag correcto
        if (string.IsNullOrEmpty(targetTag) || collision.gameObject.CompareTag(targetTag))
        {
            Debug.Log("¡Cambiando a la escena " + sceneName + "!");
            SceneManager.LoadScene(sceneName);
        }
    }
}
