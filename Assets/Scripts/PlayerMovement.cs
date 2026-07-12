using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Configuración")]
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        // Configuración automática: le pone la etiqueta "Player" para que funcionen las interacciones
        gameObject.tag = "Player";

        // Configuración automática: asegura que el personaje tenga un cuerpo físico (Collider)
        Collider2D col = GetComponent<Collider2D>();
        if (col == null)
        {
            gameObject.AddComponent<BoxCollider2D>();
            Debug.Log("¡Cuerpo físico (BoxCollider2D) añadido automáticamente al jugador!");
        }

        // --- CARGAR POSICIÓN (Guardado de Menú) ---
        // Si el menú nos dice que venimos de presionar "Continuar"
        if (PlayerPrefs.GetInt("LoadFromSave", 0) == 1)
        {
            // Obtenemos las coordenadas X e Y guardadas (o usamos las actuales si hay error)
            float savedX = PlayerPrefs.GetFloat("SavedPosX", transform.position.x);
            float savedY = PlayerPrefs.GetFloat("SavedPosY", transform.position.y);
            
            // Movemos al jugador a esa posición
            transform.position = new Vector2(savedX, savedY);
            
            // Apagamos el "switch" para que no nos teletransporte la próxima vez que cambiemos de escena por una puerta
            PlayerPrefs.SetInt("LoadFromSave", 0);
            
            Debug.Log("¡Jugador posicionado en el punto de guardado!");
        }
        else
        {
            // --- CARGAR POSICIÓN (Cambio de Escena normal por Puerta) ---
            string spawnName = PlayerPrefs.GetString("TargetSpawnPoint", "");
            if (!string.IsNullOrEmpty(spawnName))
            {
                // Buscamos si hay algún objeto en esta escena que se llame exactamente como pedimos
                GameObject spawnPoint = GameObject.Find(spawnName);
                if (spawnPoint != null)
                {
                    transform.position = spawnPoint.transform.position;
                    Debug.Log("Jugador teletransportado a la puerta: " + spawnName);
                }
                
                // Borramos el dato para no aparecer ahí por accidente si reiniciamos
                PlayerPrefs.SetString("TargetSpawnPoint", "");
            }
        }
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // SI EL PERSONAJE SE ESTÁ MOVIENDO
        if (movement != Vector2.zero)
        {
            // Actualizamos la dirección en el Animator
            animator.SetFloat("MovX", movement.x);
            animator.SetFloat("MovY", movement.y);
        }

        // Le pasamos la velocidad actual al Animator (magnitud del vector)
        // Si no te mueves, sqrMagnitude será 0. Si te mueves, será mayor a 0.
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}