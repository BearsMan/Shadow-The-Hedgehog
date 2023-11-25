using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    public int rings = 5; // Collected at the start of the stage.
    private float eplisedTime = 0f; // This show has time it has been since the stage has started.
    public float lightBar = 0f; // For all light attack types. (blue bar)
    public float darkBar = 0f; // For evil attack types. (red bar)
    public int lives = 0; // Checks how many lives that the character has.

    [Header("Stage Totals")]
    public int ringTotal; // Total of rings collected at the end of each stage. 100 points are collected per enemy killed, a little extra is collected per object that is thrown.
    public int heroScore; // Total of hero points collected at the end of each stage, 100 points are collected per enemy killed, a little extra is collected per object that is thrown.
    public int darkScore; // Total of dark points collected at the end of each stage. 100 points are collected per time damage that the player takes, a little extra is collected per object that is thrown.
    public int totalScore; // Total time for each stage.

    [Header("UI Visuals")]

    public Slider lightBarSlider; // This is used for "Chaos Control" or "Chaos Spear" attacks. "Note" Must be filled before the attacks can be called out.
    public Slider darkBarSlider; // This is used for "Chaos Blast." "Note." Must be filled before the attack can be called out.
    public TextMeshProUGUI livesUI; // This is used to show how lives the player has left.
    public TextMeshProUGUI scoreText; // This is used to show the scores for each stage that you completed.
    public TextMeshProUGUI timerUI; // Shows the timer from the UI and the times will be added together based on the scores of the HeroScore, DarkScore, NormalScore, etc.

    // Stage scores and timers.
    private int normalScore;
    private int timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HUDTimer(); // Updates the UI timer once per frame while the game is being played.
                    // If pausing the game, the timer will freeze until the user is ready to continue playing again.
                    // If the user decides to restart the level, the timer will start back from 00:00:00.
                    // When the user quits the stage to go back to the main menu, the timer will reset back to default, and the user will have to play the entire level again.
    }
    public void PlayerDamage(float damage)
    {
        rings -= 10;
        darkBar += damage; // When the player is damaged, the red bar will increase the time ready to activate Chaos Blast.
        darkScore++;

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
        lightBar += damage; // When the enemies are damaged, the blue bar will increase the time ready to activate Chaos Control.
        heroScore++; // Scores are calculated when the enemies are killed in battle, either by a weapon or an object.
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
        // Ends the stage when the player dies, and you can select which option to do next. Which is either, continue from the last checkpoint, restart the level, or exit the game, and go back to the main menu.
    }
    private void AddScore(int points)
    {
        normalScore += points;
    }
    private void HUDTimer()
    {
        eplisedTime = Time.realtimeSinceStartup;
        string formattedTime = FormatTime(eplisedTime);
        timerUI.text = formattedTime;
    }
    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f); // Shown in the UI.
        int seconds = Mathf.FloorToInt(time % 60); // Shown in the UI.
        int ms = Mathf.FloorToInt(time * 1000) % 100; // Shown in the UI. (using a remainder)
        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, ms); // Returns timer format in minutes, seconds, and milliseconds.
    }
}


