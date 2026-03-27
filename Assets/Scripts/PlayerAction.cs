using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public GameObject poopObject;
    public Transform dropPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            DropPoop();
        }
    }
    public void DropPoop()
    {
        Instantiate(poopObject, dropPosition);
    }
}
