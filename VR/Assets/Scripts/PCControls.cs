using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditorInternal.ReorderableList;

public class PCControls : MonoBehaviour
{
    // Start is called before the first frame update
    private XRIDefaultInputActions controls;
    private GameObject Labirinth;
    public Rigidbody Rigidbody;
    private bool Front = false;
    private bool isMovingFront = false;
    private bool Back = false;
    private bool isMovingBack = false;
    private bool Left = false;
    private bool isMovingLeft = false;
    private bool Right = false;
    private bool isMovingRight = false;
    private bool ArrowInput = false;

    float timer = 0.03f;
    void Start()
    {
        Labirinth = this.gameObject;

        if(ArrowInput)
        {
            ArrowControl();
        }
        else 
        {
            WasdControl();
        }



    }

    // Register a callback for al the actions in Arrow input map
    private void ArrowControl()
    {
        controls.Arrow.Front.performed += ctx => Front = true;
        controls.Arrow.Front.canceled += ctx => Front = false;

        controls.Arrow.Back.performed += ctx => Back = true;
        controls.Arrow.Back.canceled += ctx => Back = false;

        controls.Arrow.Left.performed += ctx => Left = true;
        controls.Arrow.Left.canceled += ctx => Left = false;

        controls.Arrow.Right.performed += ctx => Right = true;
        controls.Arrow.Right.canceled += ctx => Right = false;
    }
    // Register a callback for al the actions in Wasd input map
    private void WasdControl()
    {
        controls.Wasd.Front.performed += ctx => isMovingFront = true;
        controls.Wasd.Front.canceled += ctx => isMovingFront = false;

        controls.Wasd.Back.performed += ctx => isMovingBack = true;
        controls.Wasd.Back.canceled += ctx => isMovingBack = false;

        controls.Wasd.Left.performed += ctx => isMovingLeft = true;
        controls.Wasd.Left.canceled += ctx => isMovingLeft = false;

        controls.Wasd.Right.performed += ctx => isMovingRight = true;
        controls.Wasd.Right.canceled += ctx => isMovingRight = false;
    }
    private void Awake() //
    {
        // Initialize the generated Input Actions class, must be on awake
        controls = new XRIDefaultInputActions();
        
        
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer<0)
        {
            if(isMovingFront)
            {
                MoveFront();
            }
            if(isMovingBack)
            {
                MoveBack();
            }
            if(isMovingLeft)
            {
                MoveLeft();
            }
            if(isMovingRight)
            {
                MoveRight();
            }

            if (Front)
            {
                RotateFront();
            }
            if (Back)
            {
                RotateBack();
            }
            if (Left)
            {
                RotateLeft();
            }
            if (Right)
            {
                RotateRight();
            }
            timer = 0.03f;
        }
        
    }
    // Update is called once per frame
    private void OnEnable()
    {
        controls.Arrow.Enable();
        controls.Wasd.Enable();
    }

    private void OnDisable()
    {
        controls.Arrow.Disable();
    }

    private void MoveFront()  //labirinth mechanic
    {
        Rigidbody.AddForce(new Vector3(0,0,2), ForceMode.Impulse);
    }
    private void MoveBack()  //labirinth mechanic
    {
        Rigidbody.AddForce(new Vector3(0,0,-2), ForceMode.Impulse);
    }
    private void MoveLeft()  //labirinth mechanic
    {
        Rigidbody.AddForce(new Vector3(-2,0,0), ForceMode.Impulse);
    }
    private void MoveRight()  //labirinth mechanic
    {
        Rigidbody.AddForce(new Vector3(2,0,0), ForceMode.Impulse);
    }

    private void RotateFront()  //labirinth mechanic
    {
        if(NormalizeAngle(Labirinth.transform.eulerAngles.x) < 10)
        {
            Labirinth.transform.rotation = Quaternion.Euler(Labirinth.transform.eulerAngles + new Vector3(1f, 0, 0));
        }
    }
    private void RotateBack()  //labirinth mechanic
    {
        if (NormalizeAngle(Labirinth.transform.eulerAngles.x) > -10)
        {
            Labirinth.transform.rotation = Quaternion.Euler(Labirinth.transform.eulerAngles + new Vector3(-1f, 0, 0));
        }
    }
    private void RotateLeft()  //labirinth mechanic
    {
            if (NormalizeAngle(Labirinth.transform.eulerAngles.z) < 10)
            {
                Labirinth.transform.rotation = Quaternion.Euler(Labirinth.transform.eulerAngles + new Vector3(0, 0, 1));
            }
    }
    private void RotateRight()  //labirinth mechanic
    {
                if (NormalizeAngle(Labirinth.transform.eulerAngles.z) > -10)
                {
                    Labirinth.transform.rotation = Quaternion.Euler(Labirinth.transform.eulerAngles + new Vector3(0, 0, -1));
                }
    }

    private float NormalizeAngle(float angle) // Convert the 0 to 360 rotation to -180 180
    {
        if (angle > 180f)
        {
            angle -= 360f;
        }
        return angle;
    }
}
