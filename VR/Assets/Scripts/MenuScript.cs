using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuScript : Singleton<MenuScript>
{

    [SerializeField]
    // Reference of the toggle
    private Toggle toggleControls;

    [SerializeField]
    // Reference of the slider
    private Slider volumeSlider;

    // Value of the slider
    private float volume;

    //Read-only property of the volume float
    public float Volume { get { return volume; } }

    // Bool to know which controls the player want to use
    // If true the player will use WASD as controls
    private bool control;

    // Read-only property of the control bool
    public bool Control { get { return control; } }

    // Start is called before the first frame update
    void Start()
    {
        // Save the value of the slider as volume
        volume = volumeSlider.value;

        // Save the value of the bool as control
        control = toggleControls.isOn;

        // Add a listener to the toggle and slider, when the value change the respective methods are called
        toggleControls.onValueChanged.AddListener(ChangeControls);
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    // Change the bool value when clicking on the toggle
    private void ChangeControls(bool isOn)
    {
        control = isOn;
    }

    // Change the value based on the slider position
    private void ChangeVolume(float value)
    {
        volume = value;

        // Call the method Change Volume that change the Volume of all the sound Source
        AudioScript.Instance.ChangeVolume();
    }

    // Change scene to the Gameplay Scene
    public void LoadGame()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    // Quit the Game
    public void ExitGame()
    {
        Application.Quit();
    }

    // Used to stop the timer in the setting menu is open 
    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    // Used to resume the timer when the setting menu is closed
    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}
