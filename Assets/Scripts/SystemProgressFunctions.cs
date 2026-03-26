using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class SystemProgressFunctions : MonoBehaviour
{

    public float speed = 2;
    public Vector2 movement;
    public Vector3 spawn = new Vector3(-8.5f, 0, 0);

    //trackers
    public float wins = 0;
    public float losses = 0;

    //reset event
    public UnityEvent resetGame;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = spawn;
    }

    // Update is called once per frame
    void Update()
    {

        transform.position += (Vector3)movement * speed * Time.deltaTime;
    
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
    }

    public void playerWon()
    {
        transform.position = spawn;
        wins = wins + 1;
        resetGame.Invoke();
    }

}