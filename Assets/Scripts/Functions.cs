using UnityEngine;

public class functions : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void blowup()
    {
        gameObject.SetActive(false);
    }

    public void move()
    {
        gameObject.transform.position += new Vector3(1, 1, 1);
    }
}
