using NUnit.Framework.Internal;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    public GameObject poopObject;
    public Transform dropPosition;
    public int poopFuel;
    public int maxFuel = 5;
    public float score;
    int scoreInt;
    public TMP_Text scoreText;
    public GameObject poop1;
    public GameObject poop2;
    public GameObject poop3;
    public GameObject poop4;
    public GameObject poop5;
    public GameObject deathScreen;
    public TMP_Text yourScoreText;
    public TMP_Text highScoreText;
    public TMP_Text newHighScoreText;
    public GameObject mainMenu;
    public TMP_Text menuHighScore;
    public GameObject poopIndicator;
    public GameObject inGameScore;
    public GameObject deathEffect;
    Rigidbody2D rb;
    public Animator animator;
    public AudioClip[] loseSound;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        float getMenuHighScore = PlayerPrefs.GetFloat("HighScore");
        int getMenuHighScoreInt = Mathf.RoundToInt(getMenuHighScore);
        UpdateScore();
        menuHighScore.text = "Highscore: " + getMenuHighScoreInt;
        poopIndicator.SetActive(false);
        inGameScore.SetActive(false);
        Time.timeScale = 0f;
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && poopFuel > 0)
        {
            DropPoop();
        }
        if (poopFuel == 5)
        {
            poop1.SetActive(true);
            poop2.SetActive(true); poop3.SetActive(true);
            poop4.SetActive(true); poop5.SetActive(true);
        }
        else if (poopFuel == 4)
        {
            poop1.SetActive(true);
            poop2.SetActive(true); poop3.SetActive(true);
            poop4.SetActive(true); poop5.SetActive(false);
        }
        else if (poopFuel == 3)
        {
            poop1.SetActive(true);
            poop2.SetActive(true); poop3.SetActive(true);
            poop4.SetActive(false); poop5.SetActive(false);
        }
        else if (poopFuel == 2)
        {
            poop1.SetActive(true);
            poop2.SetActive(true); poop3.SetActive(false);
            poop4.SetActive(false); poop5.SetActive(false);
        }
        else if (poopFuel == 1)
        {
            poop1.SetActive(true);
            poop2.SetActive(false); poop3.SetActive(false);
            poop4.SetActive(false); poop5.SetActive(false);
        }
        else if (poopFuel == 0)
        {
            poop1.SetActive(false);
            poop2.SetActive(false); poop3.SetActive(false);
            poop4.SetActive(false); poop5.SetActive(false);
        }
    }
    public void DropPoop()
    {
        poopFuel -= 1;
        Instantiate(poopObject, dropPosition);
    }
    public void GetFuel(int fuel)
    {
        poopFuel += fuel;
        poopFuel = Mathf.Clamp(poopFuel, 0, maxFuel);
    }
    public void GetScore(float gottenScore)
    {
        score += gottenScore;
        scoreInt = Mathf.RoundToInt(score);
        UpdateScore();
    }
    public void UpdateScore()
    {
        scoreText.text = "Score: " + scoreInt;
    }
    public void Death()
    {
        AudioManager.Instance.StopMusic();
        int randLose = Random.Range(0, loseSound.Length);
        AudioManager.Instance.PlaySFX(loseSound[randLose]);
        deathScreen.SetActive(true);
        float lastHighScore = PlayerPrefs.GetFloat("HighScore");
        int lastHighScoreInt = Mathf.RoundToInt(lastHighScore);
        Time.timeScale = 0f;
        if(scoreInt > lastHighScoreInt)
        {
            PlayerPrefs.SetFloat("HighScore", scoreInt);
            newHighScoreText.enabled = true;
            newHighScoreText.text = "New highscore: " + scoreInt;
        }
        else
        {
            yourScoreText.enabled = true;
            yourScoreText.text = "Your score: " + scoreInt;
            highScoreText.enabled= true;
            highScoreText.text = "highscore: " + lastHighScoreInt;
        }
    }
    public void ToMenu()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        AudioManager.Instance.PlayMusic();
    }
    public void PlayGame()
    {
        
        Time.timeScale = 1f;
        rb.gravityScale = 0f;
        mainMenu.SetActive(false);
        poopIndicator.SetActive(true );
        inGameScore.SetActive(true);
    }
    public IEnumerator DeathEffect()
    {
        AudioManager.Instance.StopMusic();
        rb.gravityScale = 35f;
        int randLose = Random.Range(0, loseSound.Length);
        AudioManager.Instance.PlaySFX(loseSound[randLose]);
        animator.SetBool("Dead", true);
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        ParticleSystem ps = effect.GetComponent<ParticleSystem>();
        float duration = ps.main.duration + ps.main.startLifetime.constantMax;
        Destroy(effect, duration);
        yield return new WaitForSeconds(1);
        Death();
    }
    public void StartDeath()
    {
        StartCoroutine(DeathEffect());
    }

}
