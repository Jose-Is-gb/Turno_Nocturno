using UnityEngine;
using TMPro;

public class NotaInteractiva : MonoBehaviour
{
    [Header("Configuración de UI")]
    [SerializeField] private GameObject panelNota;
    [SerializeField] private TextMeshProUGUI componenteTexto;

    [Header("Contenido")]
    [TextArea(3, 10)]
    [SerializeField] private string textoDeLaHistoria;

    private bool notaAbierta = false;
    private bool enRango = false;

    private void Start()
    {
        // Configuración automática: asegura que el área de la nota sea "atravesable" y detecte al jugador
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.isTrigger = true;
            Debug.Log("¡La nota tiene un Collider y está listo!");
        }
        else
        {
            Debug.LogError("ERROR: ¡A la nota '" + gameObject.name + "' le falta un componente BoxCollider2D! No podrá detectar al jugador.");
        }
    }

    private void Update()
    {
        // Si el jugador está en el área y presiona la tecla E
        if (enRango && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("¡Se presionó E y estamos en rango!");
            if (!notaAbierta)
            {
                MostrarNota();
            }
            else
            {
                CerrarNota();
            }
        }
    }

    // Se ejecuta cuando el jugador entra al área de detección
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Algo tocó la nota: " + collision.gameObject.name);
        if (collision.CompareTag("Player"))
        {
            enRango = true;
            Debug.Log("¡El jugador entró al rango de la nota!");
        }
    }

    // Se ejecuta cuando el jugador sale del área de detección
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enRango = false;
            Debug.Log("El jugador salió del rango.");
            
            // Si el jugador se aleja mientras la nota está abierta, la cerramos automáticamente
            if (notaAbierta)
            {
                CerrarNota();
            }
        }
    }

    void MostrarNota()
    {
        componenteTexto.text = textoDeLaHistoria;

        // --- MAGIA: AUTO-ARREGLO DE LA UI ---
        // 1. Forzamos el panel a ir al centro de la pantalla con un tamaño más amigable
        RectTransform rtPanel = panelNota.GetComponent<RectTransform>();
        if (rtPanel != null)
        {
            rtPanel.anchorMin = new Vector2(0.5f, 0.5f);
            rtPanel.anchorMax = new Vector2(0.5f, 0.5f);
            rtPanel.anchoredPosition = Vector2.zero; 
            rtPanel.sizeDelta = new Vector2(450, 250); // Un poco más chico, no tanto
            rtPanel.localScale = Vector3.one;
        }

        // 2. Le ponemos un color negro semitransparente que queda mucho mejor para tu juego
        UnityEngine.UI.Image img = panelNota.GetComponent<UnityEngine.UI.Image>();
        if (img == null) img = panelNota.AddComponent<UnityEngine.UI.Image>();
        img.color = new Color(0.05f, 0.05f, 0.05f, 0.85f); // Negro casi oscuro con un poco de transparencia

        // 3. Ajustamos el tamaño del texto para que no se salga de la caja
        RectTransform rtTexto = componenteTexto.GetComponent<RectTransform>();
        if (rtTexto != null)
        {
            rtTexto.anchorMin = new Vector2(0.5f, 0.5f);
            rtTexto.anchorMax = new Vector2(0.5f, 0.5f);
            rtTexto.anchoredPosition = Vector2.zero;
            rtTexto.sizeDelta = new Vector2(400, 200); // Un poquito más chico que el panel
            rtTexto.localScale = Vector3.one;
        }

        // 4. Centramos el texto y lo ponemos blanco para que contraste con el fondo oscuro
        componenteTexto.alignment = TextAlignmentOptions.Center;
        componenteTexto.color = Color.white;
        // ------------------------------------

        panelNota.SetActive(true); // Enciende el panel
        notaAbierta = true;
    }

    public void CerrarNota()
    {
        panelNota.SetActive(false); // Apaga el panel
        notaAbierta = false;
    }
}