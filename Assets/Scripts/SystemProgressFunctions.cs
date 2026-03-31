using System.Collections;
using System.Diagnostics.Contracts;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class SystemProgressFunctions : MonoBehaviour
{
    public TextMeshProUGUI winT;
    public TextMeshProUGUI lossT;
    public float speed = 2;
    public Vector2 movement;
    public Vector3 spawn = new Vector3(-8.5f, 0, 0);
    public GameObject go;
    public Vector3 playerpos;
    public float jumpy;
    public float savedJumpy;
    public bool goingUp = false;
    public bool goingDown = false;

    public float playerBounds;

    public HpBarAndGrass HP;
      
    //milestone timer
    public float timerCount = 0;
    public float timerLength = 2f;

    //trackers
    public float wins = 0;
    public float losses = 0;

    //reset event
    public UnityEvent resetGame;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = spawn;
        go.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if(transform.position.y <= -3)
        {

            playerBounds = transform.position.y;
            playerBounds = playerBounds + 0.1f;
            transform.position = new Vector3 (transform.position.x, playerBounds);
        }

        if (transform.position.y >= 3)
        {

            playerBounds = transform.position.y;
            playerBounds = playerBounds - 0.1f;
            transform.position = new Vector3(transform.position.x, playerBounds);
        }

        if (transform.position.x <= -9)
        {

            playerBounds = transform.position.x;
            playerBounds = playerBounds + 0.1f;
            transform.position = new Vector3(playerBounds, transform.position.y);
        }

        transform.position += (Vector3)movement * speed * Time.deltaTime;
        playerpos = transform.position;
            
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void playerLost()
    {
        transform.position = spawn;
        losses = losses +1;
        HP.startGame();
        lossT.text = (losses.ToString());
    }

    public void playerWon()
    {
        transform.position = spawn;
        wins = wins + 1;
        HP.startGame();
        winT.text = (wins.ToString());
    }

    public void milestoneHit()
    {
        Debug.Log("milestone func");
        StartCoroutine(milestone());
        
    }

    IEnumerator milestone()
    {
        Debug.Log("milestone");
        go.SetActive(true);

        while (timerCount < timerLength)
        {
            timerCount = timerCount + Time.deltaTime;
            yield return null;
        }
        if(timerCount > timerLength)
        {
            go.SetActive(false);
            timerCount = 0;
            
        }
        
    }
    public void jumpTrigger()
    {
        StartCoroutine(jump());
        savedJumpy= playerpos.y;
        jumpy= playerpos.y+0.01f;
    }
    IEnumerator jump()
    {
        goingUp = true;
        while (jumpy > savedJumpy - 0.01f)
        {
            if (jumpy < savedJumpy + 1 && goingUp)
            {
                jumpy = jumpy + 0.1f;
             
            }
            else
            {
                goingUp = false;
            }

            if (goingUp==false)
            {

                jumpy = jumpy - 0.1f;
                
            }
            transform.position = new Vector3 (transform.position.x,jumpy,0);
            yield return null;
        }
        jumpy = savedJumpy;
        transform.position = new Vector3(transform.position.x, jumpy, 0);
    }

}