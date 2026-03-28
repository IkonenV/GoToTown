using UnityEngine;

public class Cyclist : MonoBehaviour
{
    public float speed = 5f;
    public float leftBoundary = -15f; // Where the object gets destroyed
    public float spawnX = 15f;      // Where to reset it (optional)
    public int scoreFrom;

    void Update()
    {
        // Moves the object left every frame
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // If it goes off-screen, destroy it or reset it
        if (transform.position.x < leftBoundary)
        {
            // Option A: Destroy it (best for spawned obstacles)
            Debug.Log("Hävisit");
            Time.timeScale = 0;

            // Option B: Loop it (uncomment below to make it reappear on the right)
            // transform.position = new Vector2(spawnX, transform.position.y);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Poop"))
        {
            PlayerAction player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAction>();
            player.GetScore(scoreFrom);
            Destroy(gameObject);
        }
    }
}
