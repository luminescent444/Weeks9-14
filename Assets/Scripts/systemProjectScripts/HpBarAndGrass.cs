using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class HpBarAndGrass : MonoBehaviour
{
    //array of grass prefabs
    public List<GameObject> grassObjs;

    //reference to the grass prefab
    public GameObject grass;

    //reference to the player's gameobject
    public GameObject player;

    //the designated spawn position
    public Vector3 spawn;

    //the maximum HP value
    public float HP = 5;

    //trackers to remember if the HP should be decreasing or not
    public bool poison = true;

    //references to the grasses'/player's spriterenderers
    public SpriteRenderer grassSR;
    public SpriteRenderer playerSR;

    //reference to the HP bar
    public Slider HPBar;

    //variable to hold the current x value so it can be checked/changed
    public float playerX;
    
    //reference to the sensor and functions scripts
    public ProgressSensor sensor; 
    public SystemProgressFunctions functions;

    //timer variables
    public float timerCount;
    public float timerLength = 3;

    void Start()
    {
        //load up 10 slots of the array list with grass
        for (int i = 0; i < 10; i++)
        {
            //create a spawn location at a random location within the confines of the dirt
            spawn = new Vector3(Random.Range(-8.5f, 9), Random.Range(-2.5f, 2.5f));

            //spawn a grass at the determined spawn location
            grassObjs.Add(Instantiate(grass, spawn, transform.rotation));
        }

        //get the player's spriterenderer
        playerSR = player.GetComponent<SpriteRenderer>();
        
        //set the player's hp to 5
        HP = 5;

        //set the slider's maximum value to 5
        HPBar.maxValue = 5;
    }

    void Update()
    {
        //update the slider with the current HP value
        HPBar.value = HP;

        //store the player's current x position
        playerX = player.transform.position.x;

        //if the player is losing HP
        if (poison == true)
        {
            //subtract 1 from their hp
            HP = HP - 1*Time.deltaTime;
        }

        //if the player has eaten grass and should NOT be losing HP
        if (poison == false)
        {
            //add to a 3 second timer
            timerCount = timerCount + Time.deltaTime;

            //if the timer runs out
            if (timerCount > timerLength)
            {
                //reset the timer's count
                timerCount = 0;

                //turn the poison back on again
                poison = true;
            }
        }

        //if the player's hp runs out
        if (HP < 0)
        {
            //call the player lost function
            functions.playerLost();
        }


        //if the player reaches the right side of the screen
        if (playerX > 9)
        {
            //call the player won function
            functions.playerWon();
        }
    }

    //FUNCTION - when the player presses e, check for interactions
    public void grassAte()
    {
        //for every piece of grass in the array
        for(int i = 0;i < grassObjs.Count; i++)
        {
            //get the grass' spriterenderer
            grassSR = grassObjs[i].GetComponent<SpriteRenderer>(); 

            //if the grass's spriterenderer overlaps with the player's
            if (grassSR.bounds.Intersects(playerSR.bounds))
            {
                //turn off the poison's effects
                poison = false;
            }
        }
    }

    //FUNCTION - when the game restarts 
    public void startGame()
    {
        //for every piece of grass in the array 
        for (int i = 0; i < 10; i++)
        {
            //destroy the object currently in the slot
            Destroy(grassObjs[i]);

            //remove the object from the array list
            grassObjs.Remove(grassObjs[i]);

            //create a spawn location at a random location within the confines of the dirt
            spawn = new Vector3(Random.Range(-8.5f, 9), Random.Range(-2.5f, 2.5f));

            //spawn a grass at the determined spawn location
            grassObjs.Add(Instantiate(grass, spawn, transform.rotation));
        }
        
        //reset the HP to its max value
        HP = 5;

        //call the function to reset the lines in the progress sensor
        sensor.resetBools();
    }
}
