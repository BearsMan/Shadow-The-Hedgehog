using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int rings = 5; // Collected at start of stage.
    public float lightBar = 0f; // For all light attack types (blue bar).
    public float darkBar = 0f; // For evil attack types (red bar).
    public int lives = 0;
    public bool continueGame = false;
    public bool restart = false;
    [Header("UI Elements")]
    public Slider lightBarSlider; // This is used for "Chaos Control" or "Chaos Spear" attacks. "Note." Must be filled before the attacks can be called out.
    public Slider darkBarSlider; // This is used for "Chaos Blast." "Note." Must be filled before the attack can be called out.
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
    public void PlayerDamage(float damage)
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
        else
        {
            rings = 5;
            darkBar = 0f;
            lightBar = 0f;
            // The character will spawn at the last given checkpoint.
        }
    }
    private void GameOver()
    {
        Application.Quit();
    }
    private void AddScore(int points)
    {
        Score += points;
    }
}


