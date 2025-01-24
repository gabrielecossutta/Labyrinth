using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PCControls : MonoBehaviour
{
    // New Input System
    private XRIDefaultInputActions controls;

    // Reference of the Labirinth
    private GameObject labirinth;

    // Player rigidbody reference
    public Rigidbody playeRigidbody;

    // Bools for Arrow key controls
    private bool front = false;
    private bool back = false;
    private bool left = false;
    private bool right = false;

    // Bools for WASD key controls
    private bool isMovingFront = false;
    private bool isMovingBack = false;
    private bool isMovingLeft = false;
    private bool isMovingRight = false;

    // Bool to know what controls the player have choosen
    private bool inputWASD;

    // Timer to check the Player Input
    private float timer;

    private void Awake()
    {
        // Initialize the generated Input Actions class, must be on awake
        controls = new XRIDefaultInputActions();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set the timer
        timer = 0.03f;

        // Get the reference of the labirinth
        labirinth = this.gameObject;

        // Reset the HUD values
        HUDScript.Instance.ResetHUD();

        // Start the BackGround music
        AudioScript.Instance.StartMusic();

        ChooseControl();
    }

    // Check what controls the Player have chosen
    void ChooseControl()
    {
        // Copy the bool then change the controls based on the bool
        inputWASD = MenuScript.Instance.Control;

        if (inputWASD)
        {
            WasdControl();
        }
        else
        {
            ArrowControl();
        }
    }

    // Register a callback for al the actions in Arrow input map
    private void ArrowControl()
    {
        // Change the value when the key is pressed
        controls.Arrow.Front.performed += ctx => front = true;
        controls.Arrow.Back.performed += ctx => back = true;
        controls.Arrow.Left.performed += ctx => left = true;
        controls.Arrow.Right.performed += ctx => right = true;

        // Change the value when the key is released
        controls.Arrow.Front.canceled += ctx => front = false;
        controls.Arrow.Back.canceled += ctx => back = false;
        controls.Arrow.Left.canceled += ctx => left = false;
        controls.Arrow.Right.canceled += ctx => right = false;
    }

    // Register a callback for al the actions in Wasd input map
    private void WasdControl()
    {
        // Change the value when the key is pressed
        controls.Wasd.Front.performed += ctx => isMovingFront = true;
        controls.Wasd.Back.performed += ctx => isMovingBack = true;
        controls.Wasd.Left.performed += ctx => isMovingLeft = true;
        controls.Wasd.Right.performed += ctx => isMovingRight = true;

        // Change the value when the key is released
        controls.Wasd.Front.canceled += ctx => isMovingFront = false;
        controls.Wasd.Back.canceled += ctx => isMovingBack = false;
        controls.Wasd.Left.canceled += ctx => isMovingLeft = false;
        controls.Wasd.Right.canceled += ctx => isMovingRight = false;

    }

    // Update is called once per frame
    private void Update()
    {
        // Subtract the deltaTime from the timer every frame to create a count down
        timer -= Time.deltaTime;

        // Check if the timer reaches zero, then we check if the player has performed any inputs
        if (timer < 0)
        {
            if (inputWASD)
            {
                if (isMovingFront)
                {
                    MoveFront();
                }
                if (isMovingBack)
                {
                    MoveBack();
                }
                if (isMovingLeft)
                {
                    MoveLeft();
                }
                if (isMovingRight)
                {
                    MoveRight();
                }
            }
            else
            {
                if (front)
                {
                    RotateFront();
                }
                if (back)
                {
                    RotateBack();
                }
                if (left)
                {
                    RotateLeft();
                }
                if (right)
                {
                    RotateRight();
                }
            }
            timer = 0.03f;
        }
    }

    // Enable the inputs
    private void OnEnable()
    {
        controls.Arrow.Enable();
        controls.Wasd.Enable();
    }

    //  Disable the inputs
    private void OnDisable()
    {
        controls.Arrow.Disable();
        controls.Wasd.Disable();
    }

    // Apply impulse force to the rigidbody to move the player
    private void MoveFront()
    {
        playeRigidbody.AddForce(new Vector3(0, 0, 2), ForceMode.Impulse);
    }
    private void MoveBack()
    {
        playeRigidbody.AddForce(new Vector3(0, 0, -2), ForceMode.Impulse);
    }
    private void MoveLeft()
    {
        playeRigidbody.AddForce(new Vector3(-2, 0, 0), ForceMode.Impulse);
    }
    private void MoveRight()
    {
        playeRigidbody.AddForce(new Vector3(2, 0, 0), ForceMode.Impulse);
    }

    // Rotate the Labyrinth to use the gravity to move the player
    private void RotateFront()
    {
        if (NormalizeAngle(labirinth.transform.eulerAngles.x) < 10)
        {
            labirinth.transform.rotation = Quaternion.Euler(labirinth.transform.eulerAngles + new Vector3(1f, 0, 0));
        }
    }
    private void RotateBack()
    {
        if (NormalizeAngle(labirinth.transform.eulerAngles.x) > -10)
        {
            labirinth.transform.rotation = Quaternion.Euler(labirinth.transform.eulerAngles + new Vector3(-1f, 0, 0));
        }
    }
    private void RotateLeft()
    {
        if (NormalizeAngle(labirinth.transform.eulerAngles.z) < 10)
        {
            labirinth.transform.rotation = Quaternion.Euler(labirinth.transform.eulerAngles + new Vector3(0, 0, 1));
        }
    }
    private void RotateRight()
    {
        if (NormalizeAngle(labirinth.transform.eulerAngles.z) > -10)
        {
            labirinth.transform.rotation = Quaternion.Euler(labirinth.transform.eulerAngles + new Vector3(0, 0, -1));
        }
    }

    // Convert the 0 to 360 rotation into -180 to 180, for the arrow key controls mechanic
    private float NormalizeAngle(float angle)
    {
        if (angle > 180f)
        {
            angle -= 360f;
        }
        return angle;
    }
}
