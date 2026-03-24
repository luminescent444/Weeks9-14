using UnityEngine;

public class audio : MonoBehaviour
{

    public AudioSource sfx;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void  Footstep ()
    {
        sfx.Play();
    }

}
