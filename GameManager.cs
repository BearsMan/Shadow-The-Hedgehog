using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int rings = 5; // Collected at the start of the stage.
    private float eplisedTime = 0f; // This show has time it has been since the stage has started.
    public float lightBar = 0f; // For all light attack types (blue bar).
    public float darkBar = 0f; // For evil attack types (red bar).
    public int lives = 0; // Checks how many lives that the character has.
    [Header("UI Elements")]
    public Slider lightBarSlider; // This is used for "Chaos Control" or "Chaos Spear" attacks. "Note." Must be filled before the attacks can be called out.
    public Slider darkBarSlider; // This is used for "Chaos Blast." "Note." Must be filled before the attack can be called out.
    public TextMeshProUGUI livesUI; // This is used to show how lives the player has left.
    public TextMeshProUGUI scoreText;
    private int score;
    private int timer;
    public TextMeshProUGUI timerUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HUDTimer();
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
        
    }
    private void AddScore(int points)
    {
        score += points;
    }
    private void HUDTimer()
    {
        eplisedTime += Time.deltaTime;
        string formattedTime = FormatTime(eplisedTime);
        timerUI.text = formattedTime;
    }
    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60);
        int ms = Mathf.FloorToInt(time / 1000) % 1000;
        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, ms);
        
    }
}


