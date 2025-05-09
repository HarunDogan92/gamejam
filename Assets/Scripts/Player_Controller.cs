using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //Components
    Rigidbody myRB;
    Transform myAvatar;
    Transform myLightLeft;
    Transform myLightRight;
    //Player movement 
    [SerializeField] InputAction WASD;
    Vector2 movementInput;
    [SerializeField] float movementSpeed;
    float currentDirection = 1f;

    private void OnEnable()
    {
        WASD.Enable();
    }

    private void OnDisable()
    {
        WASD.Disable();
    }

    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myAvatar = transform.GetChild(0);
        myLightLeft = transform.GetChild(2);
        myLightRight = transform.GetChild(3);
    }

    void Update()
    {
        movementInput = WASD.ReadValue<Vector2>();
        if(movementInput.x!=0)
        {
            float direction = Mathf.Sign(movementInput.x);
            myAvatar.localScale = new Vector2(direction, 1);

            if (direction != currentDirection) {
                currentDirection = direction;

                // Activate/deactivate lights based on direction
                bool facingRight = direction > 0;
                myLightRight.gameObject.SetActive(facingRight);
                myLightLeft.gameObject.SetActive(!facingRight);
            }
        }
    }
    private void FixedUpdate()
    {
        myRB.linearVelocity = movementInput * movementSpeed;
    }
}