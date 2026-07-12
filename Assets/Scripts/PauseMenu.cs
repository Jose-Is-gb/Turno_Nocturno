using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    private GameObject pauseCanvas;
    public static bool isPaused = false;

    void Start()
    {
        // Generamos la interfaz de usuario automáticamente a través del código
        CreatePauseMenuUI();
    }

    void Update()
    {
        // Detectamos si el jugador presiona Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void CreatePauseMenuUI()
    {
        // 1. Crear el Canvas (el lienzo de la UI)
        pauseCanvas = new GameObject("AutoPauseCanvas");
        Canvas canvas = pauseCanvas.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999; // Para que aparezca encima de TODO
        
        CanvasScaler scaler = pauseCanvas.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        
        pauseCanvas.AddComponent<GraphicRaycaster>();

        // 2. Verificar si existe un EventSystem (Necesario para hacer clic en los botones)
        if (FindObjectOfType<EventSystem>() == null)
        {
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }

        // 3. Crear el Panel oscuro de fondo
        GameObject panel = new GameObject("PausePanel");
        panel.transform.SetParent(pauseCanvas.transform, false);
        Image panelImage = panel.AddComponent<Image>();
        panelImage.color = new Color(0.05f, 0.05f, 0.05f, 0.9f); // Fondo casi negro y transparente
        
        RectTransform panelRect = panel.GetComponent<RectTransform>();
        panelRect.anchorMin = Vector2.zero;
        panelRect.anchorMax = Vector2.one;
        panelRect.sizeDelta = Vector2.zero;

        // 4. Crear el Título "PAUSA"
        GameObject title = new GameObject("TitleText");
        title.transform.SetParent(panel.transform, false);
        TextMeshProUGUI titleText = title.AddComponent<TextMeshProUGUI>();
        titleText.text = "JUEGO PAUSADO";
        titleText.fontSize = 80;
        titleText.alignment = TextAlignmentOptions.Center;
        titleText.color = Color.white;
        titleText.fontStyle = FontStyles.Bold;
        
        RectTransform titleRect = title.GetComponent<RectTransform>();
        titleRect.anchoredPosition = new Vector2(0, 200);
        titleRect.sizeDelta = new Vector2(800, 100);

        // 5. Crear los Botones llamando a nuestra función ayudante
        CreateButton("Reanudar", panel.transform, new Vector2(0, 50), Resume);
        CreateButton("Guardar Partida", panel.transform, new Vector2(0, -50), SaveGame);
        CreateButton("Ir al Menú", panel.transform, new Vector2(0, -150), LoadMenu);

        // 6. Ocultar el menú al inicio
        pauseCanvas.SetActive(false);
    }

    void CreateButton(string label, Transform parent, Vector2 position, UnityEngine.Events.UnityAction onClickAction)
    {
        GameObject buttonObj = new GameObject("Btn_" + label);
        buttonObj.transform.SetParent(parent, false);
        
        // Fondo del botón
        Image btnImage = buttonObj.AddComponent<Image>();
        btnImage.color = new Color(0.15f, 0.15f, 0.15f, 1f); // Gris oscuro
        
        // Componente Botón y evento OnClick
        Button btn = buttonObj.AddComponent<Button>();
        btn.onClick.AddListener(onClickAction);

        // Tamaño y posición
        RectTransform btnRect = buttonObj.GetComponent<RectTransform>();
        btnRect.anchoredPosition = position;
        btnRect.sizeDelta = new Vector2(350, 70);

        // Texto dentro del botón
        GameObject textObj = new GameObject("Text");
        textObj.transform.SetParent(buttonObj.transform, false);
        TextMeshProUGUI tmp = textObj.AddComponent<TextMeshProUGUI>();
        tmp.text = label;
        tmp.fontSize = 30;
        tmp.color = Color.white;
        tmp.alignment = TextAlignmentOptions.Center;
        
        RectTransform textRect = textObj.GetComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.sizeDelta = Vector2.zero;
    }

    public void Resume()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void SaveGame()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        PlayerPrefs.SetString("SavedScene", currentScene);
        
        // Buscar al jugador y guardar su posición exacta
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerPrefs.SetFloat("SavedPosX", player.transform.position.x);
            PlayerPrefs.SetFloat("SavedPosY", player.transform.position.y);
        }

        PlayerPrefs.Save();
        Debug.Log("¡Partida guardada correctamente en la escena: " + currentScene + "!");
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("Main_Menu"); 
    }
}
