using UnityEngine;

public class dropBarrel : MonoBehaviour
{

    public GameObject barrel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Drop()
    {
        while (barrel.transform.position.y > -100)
        {
            barrel.transform.position = new Vector3 (0, barrel.transform.position.y -1);
        }
            
            
    }

}
