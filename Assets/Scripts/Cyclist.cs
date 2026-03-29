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
    float multiplier;
    public Transform scorePoint;
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
                PlayerAction action = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAction>();
                action.Death();
                Destroy(gameObject);

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
            Poop poop1 = poop.GetComponent<Poop>();
            multiplier = poop1.pointMultiplier;
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAction>();
            Destroy(poop);
            animator.SetTrigger("Death");
            dead = true;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerAction playerAction = collision.gameObject.GetComponent<PlayerAction>();
            playerAction.StartDeath();
        }
    }
    public void Death()
    {
        int scorePopUp = Mathf.RoundToInt(scoreFrom * multiplier);
        PopUpManager popUpManager = GameObject.FindGameObjectWithTag("PopUpManager").GetComponent<PopUpManager>();
        popUpManager.SpawnPopUp(scorePoint.position, scorePopUp);
        player.GetScore(scoreFrom * multiplier);
        Destroy(gameObject);
    }
}
