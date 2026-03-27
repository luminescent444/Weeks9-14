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
        resetGame.Invoke();
        lossT.text = (losses.ToString());
    }

    public void playerWon()
    {
        transform.position = spawn;
        wins = wins + 1;
        resetGame.Invoke();
        winT.text = (wins.ToString());
    }

    public void milestoneHit()
    {
        StartCoroutine(milestone());
        Debug.Log("milestone func");
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