using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    public string textValue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text.text = textValue;
        Destroy(gameObject, 1.3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
