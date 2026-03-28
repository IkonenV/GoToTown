using UnityEngine;

public class CollectBird : MonoBehaviour
{
    public float speed = 5f;
    public float leftBoundary = -15f; // Where the object gets destroyed
    public float rightBoundary = 15f;
    public float spawnX = 15f;      // Where to reset it (optional)
    public int fuelFrom;
    public bool goingLeft;
    private void Start()
    {
        if (!goingLeft)
        {
            transform.Rotate(0f, 180f, 0f);
        }
    }
    void Update()
    {
        if (goingLeft)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x < leftBoundary)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x > rightBoundary)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //anna kakkaa
            //efekti
            //ääni
            PlayerAction player = collision.gameObject.GetComponent<PlayerAction>();
            player.GetFuel(fuelFrom);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Plane"))
        {
            //efekti
            //ääni
            Destroy(gameObject);
        }
    }
}
