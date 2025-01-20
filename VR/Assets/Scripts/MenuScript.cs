using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuScript : Singleton<MenuScript>
{
    // Start is called before the first frame update
    [SerializeField]
    private Toggle toggle;
    [SerializeField]
    private Slider slider;
    private float Volume;
    private bool WASDControl;
    public bool Control { get { return WASDControl; } }
    void Start()
    {
        Volume = slider.value;
        WASDControl = toggle.isOn;
        toggle.onValueChanged.AddListener(ChangeControls);
        slider.onValueChanged.AddListener(ChangeVolume);
    }

    private void ChangeControls(bool isOn)
    {
        WASDControl=isOn;
    }

    private void ChangeVolume(float value)
    {
        Volume=value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
