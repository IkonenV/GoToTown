using UnityEngine;

public class Plane : MonoBehaviour
{
    public float speed = 5f;
    public float leftBoundary = -15f; // Where the object gets destroyed
    public float spawnX = 15f;      // Where to reset it (optional)

    void Update()
    {
        // Moves the object left every frame
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // If it goes off-screen, destroy it or reset it
        if (transform.position.x < leftBoundary)
        {
            // Option A: Destroy it (best for spawned obstacles)
            Destroy(gameObject);

            // Option B: Loop it (uncomment below to make it reappear on the right)
            // transform.position = new Vector2(spawnX, transform.position.y);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerAction playerAction = collision.gameObject.GetComponent<PlayerAction>();
            playerAction.StartDeath();
        }
    }
}
