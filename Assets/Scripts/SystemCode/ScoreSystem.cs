using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public int score, highscore;

    [SerializeField]
    TextMeshProUGUI scoreText, highscoreText;

    public void Start()
    {
        Load();
    }

    public void Update()
    {
        scoreText.text = score.ToString("D3");
        highscoreText.text = highscore.ToString("D3");

        if (score > highscore)
        {
            highscore = score;
            Save();
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
    }

    public void SubtractScore(int amount)
    {
        score -= amount;
    }

    public void Save()
    {
        PlayerPrefs.SetInt("highscore", highscore);
    }

    public void Load()
    {
        highscore = PlayerPrefs.GetInt("highscore");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Milk")
        {
            AddScore(1);
        }
    }
}
