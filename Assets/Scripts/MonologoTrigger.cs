using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MonologoTrigger : MonoBehaviour
{
    [Header("Configuración del Monólogo")]
    [TextArea(2, 5)]
    [Tooltip("Lo que Daniel pensará o dirá al pasar por aquí")]
    public string textoMonologo = "Es mi primer día. Solo tengo que mantener la calma y vigilar que nada raro pase...";
    
    [Tooltip("Tiempo en segundos que el texto estará en pantalla")]
    public float duracion = 10f;

    // Para asegurarnos de que solo se active una vez
    private bool yaSeMostro = false;

    // Variables para nuestra UI generada por código
    private GameObject subtituloCanvas;
    private Text subtituloTexto;

    void Start()
    {
        // 1. AUTO-ARREGLO: Nos aseguramos de que el BoxCollider sea Trigger
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.isTrigger = true;
        }

        // 2. Creamos la interfaz visual para el texto automáticamente
        CrearUI();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si el jugador lo pisa y aún no se ha mostrado
        if (collision.CompareTag("Player") && !yaSeMostro)
        {
            yaSeMostro = true; // Lo marcamos para que no se repita
            StartCoroutine(MostrarMonologo());
        }
    }

    IEnumerator MostrarMonologo()
    {
        if (subtituloCanvas != null)
        {
            // Encendemos el texto
            subtituloCanvas.SetActive(true);
            subtituloTexto.text = textoMonologo;
            
            // Esperamos los segundos que le dijimos
            yield return new WaitForSeconds(duracion);
            
            // Apagamos el texto
            subtituloCanvas.SetActive(false);
        }
    }

    void CrearUI()
    {
        // 1. Crear el Canvas principal
        subtituloCanvas = new GameObject("MonologoCanvas_" + gameObject.name);
        Canvas canvas = subtituloCanvas.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 50; // Para que aparezca encima del juego
        
        CanvasScaler scaler = subtituloCanvas.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        
        // 2. Crear una franja negra semi-transparente abajo (estilo cine)
        GameObject panelFondo = new GameObject("FondoNegro");
        panelFondo.transform.SetParent(subtituloCanvas.transform, false);
        Image img = panelFondo.AddComponent<Image>();
        img.color = new Color(0f, 0f, 0f, 0.75f);
        
        RectTransform panelRect = panelFondo.GetComponent<RectTransform>();
        panelRect.anchorMin = new Vector2(0.1f, 0.05f); // Lo anclamos en la parte inferior
        panelRect.anchorMax = new Vector2(0.9f, 0.15f); // Le damos poco alto para que parezca subtítulo
        panelRect.sizeDelta = Vector2.zero;

        // 3. Crear el Texto
        GameObject textObj = new GameObject("Texto");
        textObj.transform.SetParent(panelFondo.transform, false);
        subtituloTexto = textObj.AddComponent<Text>();
        subtituloTexto.text = "";
        
        // En Unity 6 Arial.ttf ya no existe, usamos la nueva fuente por defecto directamente
        Font defaultFont = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        subtituloTexto.font = defaultFont;
        
        subtituloTexto.fontSize = 30;
        subtituloTexto.color = Color.white;
        subtituloTexto.alignment = TextAnchor.MiddleCenter;
        subtituloTexto.fontStyle = FontStyle.Italic; // Cursiva para dar a entender que es un pensamiento
        
        RectTransform textRect = textObj.GetComponent<RectTransform>();
        textRect.anchorMin = new Vector2(0.05f, 0.1f);
        textRect.anchorMax = new Vector2(0.95f, 0.9f);
        textRect.sizeDelta = Vector2.zero;

        // Lo ocultamos desde el inicio
        subtituloCanvas.SetActive(false);
    }
}
