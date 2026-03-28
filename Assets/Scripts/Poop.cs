using UnityEngine;

public class Poop : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Plane"))
        {
            Destroy(gameObject);
        }
    }
}
