using UnityEngine;
using UnityEngine.Events;

public class ProgressSensor : MonoBehaviour
{

    public bool line1Hpappened = false;
    public bool line2Hpappened = false;
    public bool line3Hpappened = false;

    //bars
    public SpriteRenderer line1;
    public SpriteRenderer line2;
    public SpriteRenderer line3;
    public GameObject line1g;
    public GameObject line2g;
    public GameObject line3g;

    public UnityEvent milestone;

    public SpriteRenderer playerSR;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //milestone events
        if (line1.bounds.Intersects(playerSR.bounds) && line1Hpappened == false)
        {
            line1g.SetActive(false);
            milestone.Invoke();
            line1Hpappened = true;
        }

        if (line2.bounds.Intersects(playerSR.bounds) && line2Hpappened == false)
        {
            line2g.SetActive(false);
            milestone.Invoke();
            line2Hpappened = true;
        }

        if (line3.bounds.Intersects(playerSR.bounds) && line3Hpappened == false)
        {
            line3g.SetActive(false);
            milestone.Invoke();
            line3Hpappened = true;
        }
    }

    public void resetBools()
    {
        line1Hpappened = false;
        line2Hpappened = false;
        line3Hpappened = false;
    }

}
