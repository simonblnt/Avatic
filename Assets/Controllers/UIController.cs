using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject timerTextObject;
    public TimerController timerController;
    private TMPro.TextMeshProUGUI timerText;

    private void Start()
    {
        timerText = timerTextObject.GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void Update()
    {
        UpdateTimerDisplay();
    }

    void UpdateTimerDisplay()
    {
        // Update the timer text based on the TimerController's timer value
        int seconds = Mathf.FloorToInt(timerController.GetTimer());
        int minutes = seconds / 60;
        seconds %= 60;

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
