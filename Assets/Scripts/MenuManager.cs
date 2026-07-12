using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Paneles del Menú")]
    public GameObject panelBotones;
    public GameObject panelCreditos;

    [Header("Efectos de Sonido")]
    [Tooltip("Arrastra aquí tu sonido para cuando el ratón pasa por encima")]
    public AudioClip sonidoHover;
    [Tooltip("Arrastra aquí el objeto SFX_Source que acabas de crear")]
    public AudioSource sfxSource;

    void Start()
    {
        if (panelBotones != null) panelBotones.SetActive(true);
        if (panelCreditos != null) panelCreditos.SetActive(false);
    }

    // ----------------------------------------------------
    // NUEVA FUNCIÓN: Para cuando el ratón toca el botón
    // ----------------------------------------------------
    public void ReproducirSonidoHover()
    {
        // Verifica que pusimos el sonido y el reproductor en el Inspector para evitar errores
        if (sonidoHover != null && sfxSource != null)
        {
            // Reproduce el sonido una vez
            sfxSource.PlayOneShot(sonidoHover);
        }
    }

    // Lógica para COMENZAR
    public void BotonComenzar()
    {
        SceneManager.LoadScene("INTRO 00");
    }

    // Lógica para CONTINUAR
    public void BotonContinuar()
    {
        // Revisamos si hay una partida guardada previamente (por el menú de pausa)
        if (PlayerPrefs.HasKey("SavedScene"))
        {
            // Le avisamos al juego que estamos cargando desde una partida guardada
            PlayerPrefs.SetInt("LoadFromSave", 1);

            // Obtenemos el nombre de la escena guardada
            string escenaGuardada = PlayerPrefs.GetString("SavedScene");
            Debug.Log("Cargando partida guardada en: " + escenaGuardada);
            SceneManager.LoadScene(escenaGuardada);
        }
        else
        {
            // Si no hay partida guardada, empezamos desde el principio
            Debug.Log("No hay partida guardada, empezando desde el inicio.");
            SceneManager.LoadScene("INTRO 00");
        }
    }

    // Lógica para CRÉDITOS
    public void BotonAbrirCreditos()
    {
        panelBotones.SetActive(false);
        panelCreditos.SetActive(true);
    }

    public void BotonCerrarCreditos()
    {
        panelCreditos.SetActive(false);
        panelBotones.SetActive(true);
    }

    // Lógica para SALIR
    public void BotonSalir()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}