using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditorInternal.ReorderableList;

public class PCControls : MonoBehaviour
{
    // Start is called before the first frame update
    private XRIDefaultInputActions controls;
    private GameObject Labirinth;
    void Start()
    {
        Labirinth = this.gameObject;
        controls.PC.Front.performed += ctx => RotateFront();
        controls.PC.Back.performed += ctx => RotateBack();
        controls.PC.Left.performed += ctx => RotateLeft();
        controls.PC.Right.performed += ctx => RotateRight();
    }
    private void Awake() //
    {
        // Initialize the generated Input Actions class, must be on awake
        controls = new XRIDefaultInputActions();
        // Register a callback for al the actions in PC input map
        
    }
    // Update is called once per frame
    private void OnEnable()
    {
        controls.PC.Enable();
    }

    private void OnDisable()
    {
        controls.PC.Disable();
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
