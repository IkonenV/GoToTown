using UnityEngine;

public class Poop : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

        }
        else if(collision.gameObject.CompareTag("Enemy"))
        {
            PlayerAction playerAction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAction>();
            playerAction.GetScore(1000);
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
