using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    public Camera cam;
    public GameObject popUpPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnPopUp(Vector3 position, int score)
    {
        GameObject popUpObject = Instantiate(popUpPrefab, position, Quaternion.identity);
        popUpObject.GetComponent<PopUp>().textValue = score.ToString();
    }
}
