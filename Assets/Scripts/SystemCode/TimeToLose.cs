using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using UnityEngine.SceneManagement;

public class TimeToLose : MonoBehaviour
{
    public float TimeLeft;
    public bool TimerOn = false;

    [SerializeField]
    TextMeshProUGUI timerText;

    void Start()
    {
        TimerOn = true;
    }

    void Update()
    {
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            }
            else
            {
                TimeLeft = 0;
                TimerOn = false;
            }
        }
        else
        {
            PlayerPrefs.SetInt("kingofgame", 1);
            SceneManager.LoadScene("Scene0");
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}