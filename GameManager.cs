using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int rings = 5;
    public float lightBar = 0f; // For all light attack types (blue bar).
    public float darkBar = 0f; // For evil attack types (red bar).
    public int lives = 0;
    [Header("UI Elements")]
    public Slider lightBarSlider;
    public Slider darkBarSlider;
    public TextMeshProUGUI ScoreText;
    private int Score;
    public TextMeshProUGUI timer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TakeDamage(float damage)
    {
        rings -= 10;
        darkBar += damage;

        if (rings >= 10)
        {
            rings -= 10;
        }
        else if (rings < 10)
        {
            rings -= rings;
        }
        else if (rings <= 0)
        {
            OnDeath();
        }
    }
    public void EnemyDamage(float damage)
    {
        lightBar += damage;
    }
    private void OnDeath()
    {
        lives--;
        if (lives <= -1)
        {
            GameOver();
        }
    }
    private void GameOver()
    {

    }
    private void AddScore(int points)
    {
        Score += points;
    }
}


