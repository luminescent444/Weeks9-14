using Unity.XR.OpenVR;
using UnityEngine;
using UnityEngine.Events;

public class ProgressSensor : MonoBehaviour
{
    //booleans to track whether the lines have been touched during a round
    public bool line1Hpappened = false;
    public bool line2Hpappened = false;
    public bool line3Hpappened = false;

    //references to the spriterenderers and gameobjects of the milestone lines
    public SpriteRenderer line1;
    public SpriteRenderer line2;
    public SpriteRenderer line3;
    public GameObject line1g;
    public GameObject line2g;
    public GameObject line3g;

    //create the unityevent for when the line is touched
    public UnityEvent milestone;

    //reference to the player's spriterenderer
    public SpriteRenderer playerSR;

    //references to both versions of the sensor that exist in the inspector
    public ProgressSensor lineTracker;
    public ProgressSensor jumpTracker;

        void Start()
    {
        
    }

    void Update()
    {
        //milestone events

        //if the player makes contact with the first line, AND it has not made contact with it yet this round
        if (line1.bounds.Intersects(playerSR.bounds) && line1Hpappened == false)
        {
            //deactivate the line in the scene
            line1g.SetActive(false);

            //invoke the milestone event
            milestone.Invoke();

            //remember that it has made contact with it this round
            line1Hpappened = true;
        }

        //if the player makes contact with the second line, AND it has not made contact with it yet this round
        if (line2.bounds.Intersects(playerSR.bounds) && line2Hpappened == false)
        {
            //deactivate the line in the scene
            line2g.SetActive(false);

            //invoke the milestone event
            milestone.Invoke();

            //remember that it has made contact with it this round
            line2Hpappened = true;
        }

        //if the player makes contact with the third line, AND it has not made contact with it yet this round
        if (line3.bounds.Intersects(playerSR.bounds) && line3Hpappened == false)
        {
            //deactivate the line in the scene
            line3g.SetActive(false);

            //invoke the milestone event
            milestone.Invoke();

            //remember that it has made contact with it this round
            line3Hpappened = true;
        }
    }

    //FUNCTION - reset all values to do with the milestone lines 
    public void resetBools()
    {
        //reset the booleans in the player's instance of the sensor script
        jumpTracker.line1Hpappened = false;
        jumpTracker.line2Hpappened = false;
        jumpTracker.line3Hpappened = false;

        //reset the booleans in the line tracker's instance of the sensor script
        lineTracker.line1Hpappened = false;
        lineTracker.line2Hpappened = false;
        lineTracker.line3Hpappened = false;

        //reactivate the line's gameobjects
        line1g.SetActive(true);
        line2g.SetActive(true);
        line3g.SetActive(true);
    }
}
