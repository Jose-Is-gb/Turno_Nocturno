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