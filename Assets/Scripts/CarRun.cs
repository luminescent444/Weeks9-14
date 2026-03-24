using UnityEngine;

public class CarRun : MonoBehaviour
{

    public GameObject bird;
    public AudioSource sfx;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void step()
    {

        sfx.Play();
        bird.gameObject.SetActive(!bird);

    }

}
