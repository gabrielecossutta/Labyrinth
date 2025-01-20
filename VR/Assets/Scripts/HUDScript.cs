using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;
using UnityEngine.Rendering;
public class HUDScript : Singleton<HUDScript>
{
    // Start is called before the first frame update
    private TMP_Text TimerText;
    [SerializeField]
    private TMP_Text Score_Text;
    [SerializeField]
    private TMP_Text Collectibles_Text;
    private float Score;
    private int Collectibles;
    private float Timer;

    void Start()
    {
        TimerText = GetComponent<TMP_Text>();
        Score = 0f;
        Collectibles = 0;
        Timer = 120f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        int Minutes = Mathf.FloorToInt(Timer / 60);
        int Second = Mathf.FloorToInt(Timer % 60);
        TimerText.SetText($"Timer Left: {Minutes}:{Second:00}");
        Timer -= Time.deltaTime;
    }

    public void ObjectCollected()
    {
        AddPoints();
        AddCollectable();
    }

    private void AddPoints()
    {
        Score += Mathf.Clamp(Timer * 0.1f,1,10);
        Score_Text.SetText($"Score: {Score:00}");
    }

    private void AddCollectable()
    {
        Collectibles += 1;
        Collectibles_Text.SetText($"Collectables: {Collectibles:00}/20");
    }
}
