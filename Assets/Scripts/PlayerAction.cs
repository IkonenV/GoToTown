using TMPro;
using UnityEngine;
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
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateScore();
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
}
