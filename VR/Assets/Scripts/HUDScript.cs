using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;
using UnityEngine.Rendering;
public class HUDScript : Singleton<HUDScript>
{
    // Text that show the time left
    [SerializeField]
    private TMP_Text timerText;

    // Text that show the score of the player
    [SerializeField]
    private TMP_Text scoreText;

    // Text that show the number of collectable collected
    [SerializeField]
    private TMP_Text collectiblesText;

    // Int that keep track of how many collectable the player collected
    [SerializeField]
    private int collectables;

    // Int that keep track of the player points
    private float score;

    // Float that keep track of the time left
    private float timer;

    // Victory Canvas
    private Canvas winCanvas;

    // Defeat Canvas
    private Canvas defeatCanvas;

    // Bool to allow only one time the DefeatScreen and skip some code
    private bool defeatBool;

    // Start is called before the first frame update
    void Start()
    {
        //Set all the value at 0
        ResetHUD();
    }

    public void ResetHUD()
    {
        // Find the reference of the Game Objects by tag
        winCanvas = GameObject.FindGameObjectWithTag("WinCanvas").GetComponent<Canvas>();
        defeatCanvas = GameObject.FindGameObjectWithTag("LoseCanvas").GetComponent<Canvas>();
        collectiblesText = GameObject.FindGameObjectWithTag("Collectable(TMP)").GetComponent<TMPro.TMP_Text>();

        // Hide the canvas
        defeatCanvas.enabled = false;
        winCanvas.enabled = false;

        // Set the Score to 0
        score = 0f;

        // Set the text of the scoreText with always two digits 
        scoreText.SetText($"Score: {score:00}");

        // Set the number of collectables to 0
        collectables = 0;

        // Set the time to 2 minutes
        timer = 120f;

        // Set the defeate bool at true
        defeatBool = false;
    }


    // Update is called once per frame
    void Update()
    {
        //check to skip the UpdateTimer
        if (defeatBool)
        {
            return;
        }
        UpdateTimer();
    }

    // Update the timer
    private void UpdateTimer()
    {
        // Divide the time left by 60 to find how many minutes are left
        int Minutes = Mathf.FloorToInt(timer / 60);

        // Use modulo to find the remaining seconds
        int Second = Mathf.FloorToInt(timer % 60);

        // Set the text of the scoreText with always two digits 
        timerText.SetText($"Timer Left: {Minutes}:{Second:00}");

        // Subtract the deltaTime from the timer every frame to create a count down
        timer -= Time.deltaTime;

        // If the timer reaches zero the player lose
        if (timer < 0)
        {
            // Stop the BackGround music
            AudioScript.Instance.StopMusic();

            defeatBool = true;

            // Call the Lose Method
            Lose();
        }
    }

    // Called when the player collect the a collectable
    public void ObjectCollected()
    {
        AddPoints();
        AddCollectable();
    }

    private void AddPoints()
    {
        // Add point to the score based on the timer left, the faster the player collect the more point the player gain
        score += Mathf.Clamp(timer * 0.1f, 1, 10);

        // Set the text of the scoreText with always two digits 
        scoreText.SetText($"Score: {score:00}");
    }

    private void AddCollectable()
    {
        // Increse the collectable counter
        collectables += 1;

        // Set the text of the collectableText
        collectiblesText.SetText($"{collectables:00}/20");

        // If the Player collect 20 collectables the player win
        if (collectables == 20)
        {
            Win();
        }
    }

    private void Win()
    {
        // Stop the Music
        AudioScript.Instance.StopMusic();

        // Play the Victory Effect
        AudioScript.Instance.PlayEffect(AudioScript.Effects.Victory);

        // Show the WinCanvas
        winCanvas.enabled = true;
    }

    private void Lose()
    {
        // Stop the Music
        AudioScript.Instance.StopMusic();

        // Play the Defeat Effect
        AudioScript.Instance.PlayEffect(AudioScript.Effects.Defeat);

        // Show the DefeatCanvas
        defeatCanvas.enabled = true;
    }
}
