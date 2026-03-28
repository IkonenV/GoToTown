using UnityEngine;

public class Cyclist : MonoBehaviour
{
    public float speed = 5f;
    public float leftBoundary = -15f; // Where the object gets destroyed
    public float spawnX = 15f;      // Where to reset it (optional)
    public int scoreFrom;
    PlayerAction player;
    Animator animator;
    bool dead;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!dead)
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

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Poop"))
        {
            GameObject poop = collision.gameObject;
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAction>();
            Destroy(poop);
            animator.SetTrigger("Death");
            dead = true;
        }
    }
    public void Death()
    {
        player.GetScore(scoreFrom);
        Destroy(gameObject);
    }
}
