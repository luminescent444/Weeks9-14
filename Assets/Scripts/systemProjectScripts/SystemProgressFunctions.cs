using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class SystemProgressFunctions : MonoBehaviour
{

    //references to text UI elements
    public TextMeshProUGUI winT;
    public TextMeshProUGUI lossT;

    //player movement variables
    public float speed = 2;
    public Vector2 movement;
    public Vector3 spawn = new Vector3(-8.5f, 0, 0);
    public Vector3 playerpos;
    public float playerBounds;

    //reference to "go go go" text
    public GameObject go;
    
    //variables to hold current and starting y values while jumping
    public float jumpy;
    public float savedJumpy;

    //booleans to control the phases of the jump
    public bool goingUp = false;
    public bool goingDown = false;

    //reference to HpBarAndGrass script
    public HpBarAndGrass HP;
      
    //variables for the milestone text appearing and vanishing
    public float timerCount = 0;
    public float timerLength = 2f;

    //trackers for the amount of wins and losses 
    public float wins = 0;
    public float losses = 0;

    void Start()
    {
        //set the player's position to the spawn location
        transform.position = spawn;

        //make the go text invisible at start
        go.SetActive(false);
    }

    void Update()
    {
        //if statemnents keeping the player within the bounds of the dirt

        //if the player moves too far towards the bottom
        if(transform.position.y <= -3)
        {
            //move them slightly backwards
            playerBounds = transform.position.y;
            playerBounds = playerBounds + 0.1f;
            transform.position = new Vector3 (transform.position.x, playerBounds);
        }

        //if the player moves too far towards the top
        if (transform.position.y >= 3)
        {
            //move them slightly backwards
            playerBounds = transform.position.y;
            playerBounds = playerBounds - 0.1f;
            transform.position = new Vector3(transform.position.x, playerBounds);
        }

        //if the player moves too far left
        if (transform.position.x <= -9)
        {
            //move them slightly backwards
            playerBounds = transform.position.x;
            playerBounds = playerBounds + 0.1f;
            transform.position = new Vector3(playerBounds, transform.position.y);
        }

        //if the player moves, calculate their movement
        transform.position += (Vector3)movement * speed * Time.deltaTime;
        
        //make sure the transform is always updated with the value of the jump
        playerpos = transform.position;
            
    }

    //FUNCTION - movement
    public void OnMove(InputAction.CallbackContext context)
    {
        //read the player's input and set it to the movement vector
        movement = context.ReadValue<Vector2>();
    }

    //FUNCTION - if the player has lost
    public void playerLost()
    {
        //bring them back to the spawn
        transform.position = spawn;
        
        //raise their loss count by 1
        losses = losses +1;

        //call the restart function to reset the game's other components
        HP.startGame();

        //update the "loss" counter on screen
        lossT.text = (losses.ToString());
    }

    //FUNCTION - if the player has won
    public void playerWon()
    {
        //bring them back to spawn
        transform.position = spawn;

        //raise their win count by 1
        wins = wins + 1;

        //call the restart function to reset the game's other components
        HP.startGame();

        //update the "win" counter on screen
        winT.text = (wins.ToString());
    }

    //FUNCTION - if the milestone unity event is called
    public void milestoneHit()
    {
        //activate the corpoutine for the go text
        StartCoroutine(milestone());
        
    }

    //COROUTINE - controls the go text
    IEnumerator milestone()
    {
        //make the test visible
        go.SetActive(true);

        //check if the timer is still going, if it is: 
        while (timerCount < timerLength)
        {
            //add to the timer
            timerCount = timerCount + Time.deltaTime;

            //exit the while loop for this frame
            yield return null;
        }

        //if the timer is over
        if(timerCount > timerLength)
        {
            //deactivate the text
            go.SetActive(false);

            //reset the timer counter
            timerCount = 0;
        }
        
    }

    //FUNCTION - if the milestone unity event is called
    public void jumpTrigger()
    {
        //start the jumping coroutine
        StartCoroutine(jump());

        //save the y value that the player is at before the jump begins
        savedJumpy= playerpos.y;

        //set the jump value that will be changed during the jump to slightly higher than the initial value
        jumpy= playerpos.y+0.01f;
    }

    //COROUTINE - controls the player's jump movement
    IEnumerator jump()
    {
        //remember that the player should be moving upwards
        goingUp = true;

        //check if the player has fallen back below the initial y value, if it has not: 
        while (jumpy > savedJumpy - 0.01f)
        {
            //if the player is below the jump's peak and has not reached the peak yet
            if (jumpy < savedJumpy + 1 && goingUp)
            {
                //add to the player's y value
                jumpy = jumpy + 0.1f;
                         }
            else
            {
                //remember that the player has reached the peak and should turn around
                goingUp = false;
            }

            //if the player should be moving downwards (but has still not reached the value that the jump started at)
            if (goingUp==false)
            {
                //subrtact from the player's y value
                jumpy = jumpy - 0.1f;
            }

            //set the player's transform y to the jump value
            transform.position = new Vector3 (transform.position.x,jumpy,0);
            
            //exit the while loop for this frame
            yield return null;
        }

        //set the player's transform y back to the initial value
        transform.position = new Vector3(transform.position.x, savedJumpy, 0);
    }
}