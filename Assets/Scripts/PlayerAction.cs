using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    public GameObject poopObject;
    public Transform dropPosition;
    public int poopFuel;
    public int maxFuel = 5;
    public Slider poopSlider;
    public float score;
    public TMP_Text scoreText;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        poopSlider = GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>();
        poopSlider.value = poopFuel;
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && poopFuel > 0)
        {
            DropPoop();
        }
    }
    public void DropPoop()
    {
        poopFuel -= 1;
        poopSlider.value = poopFuel;
        Instantiate(poopObject, dropPosition);
    }
    public void GetFuel(int fuel)
    {
        poopFuel += fuel;
        poopFuel = Mathf.Clamp(poopFuel, 0, maxFuel);
        poopSlider.value = poopFuel;
    }
    public void GetScore(float gottenScore)
    {
        score += gottenScore;
        UpdateScore();
    }
    public void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
}
