using UnityEngine;

public class TaxiMovement : MonoBehaviour
{
    [Tooltip("Velocidad a la que se mueve el taxi")]
    public float speed = 5f;

    void Update()
    {
        // Movemos el taxi hacia arriba (frente) de manera constante
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
