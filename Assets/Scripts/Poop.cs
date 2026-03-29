using UnityEngine;

public class Poop : MonoBehaviour
{
    public float pointMultiplier;
    public float scoreFromBird;
    public AudioClip birdDeath;
    public AudioClip splash;
    public AudioSource source;

    private void Start()
    {
        source.Play();
    }
    private void Update()
    {
        pointMultiplier += Time.deltaTime / 1.2f;
        pointMultiplier = Mathf.Clamp(pointMultiplier, 0, 1);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Poop"))
        {

        }
        else if (collision.gameObject.CompareTag("Player"))
        {

        }
        else if (collision.gameObject.CompareTag("Cyclist"))
        {

        }
        else if(collision.gameObject.CompareTag("Enemy"))
        {
            AudioManager.Instance.PlaySFX(birdDeath);
            PlayerAction playerAction = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAction>();
            playerAction.GetScore(scoreFromBird * pointMultiplier);
            GameObject bird = collision.gameObject;
            Destroy(bird);
            int scorePopUp = Mathf.RoundToInt(scoreFromBird * pointMultiplier);
            PopUpManager popUpManager = GameObject.FindGameObjectWithTag("PopUpManager").GetComponent<PopUpManager>();
            popUpManager.SpawnPopUp(bird.transform.position, scorePopUp);
            Destroy(gameObject);
            source.Stop();
        }
        else
        {
            AudioManager.Instance.PlaySFX(splash);
            Destroy(gameObject);
            source.Stop();
        }

    }
}
