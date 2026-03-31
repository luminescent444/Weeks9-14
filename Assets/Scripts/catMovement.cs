using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class catMovement : MonoBehaviour
{

    public Vector3 movement;
    public float speed = 5;
    public CinemachineImpulseSource impulse;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)movement * speed * Time.deltaTime;

    }

    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void Shake()
    {
        impulse.GenerateImpulse();
    }


}
