using UnityEngine;

public class Poop : MonoBehaviour
{
    public float pointMultiplier;
    private void Update()
    {
        pointMultiplier += Time.deltaTime / 1.2f;
        pointMultiplier = Mathf.Clamp(pointMultiplier, 0, 1);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

        }
        else if(collision.gameObject.CompareTag("Enemy"))
        {
            PlayerAction playerAction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAction>();
            playerAction.GetScore(1000 * pointMultiplier);
            GameObject bird = collision.gameObject;
            Destroy(bird);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
