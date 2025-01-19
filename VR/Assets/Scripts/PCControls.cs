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
    void Start()
    {
        controls.PC.Front.performed += ctx => MoveLabirinth();
        controls.PC.Back.performed += ctx => MoveLabirinth();
        controls.PC.Left.performed += ctx => MoveLabirinth();
        controls.PC.Right.performed += ctx => MoveLabirinth();
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

    private void MoveLabirinth()  //labirinth mechanic
    {
    }
}
