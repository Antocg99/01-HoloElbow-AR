using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public TMP_Text countdownText;
    private float countdownTime = 10f;
    private bool isCounting = false;

    void Update()
    {
        if (isCounting)
        {
            countdownTime -= Time.deltaTime;
            UpdateUI();

            if (countdownTime <= 0)
            {
                // Countdown completed, you can add actions to perform after the countdown here
                countdownText.text = "Reference Frame Positioned";
                Debug.Log("Countdown completed!");
                isCounting = false;
            }
        }
    }

    void UpdateUI()
    {
        int seconds = Mathf.FloorToInt(countdownTime % 60);
        countdownText.text = seconds.ToString();
    }

    public void StartCountdown()
    {
        isCounting = true;
    }

    public void OnButtonPress()
    {
        // This method is called when the button is pressed
        StartCountdown();
    }
}
