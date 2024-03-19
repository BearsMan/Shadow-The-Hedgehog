using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
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
    public TextMeshProUGUI ringUI; // Shows the number of rings that the player currently has.

    [Header("Gameplay")]
    public GameObject player;
    private Transform spawnLocation;
    public bool isInSuperForm = false;
    // Stage scores and timers.
    private int normalScore;
    private int timer;
    [Header("GameObject")]
    public GameObject ringsPrefab;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy (gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        ringUI.text = rings.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        HUDTimer(); /*Updates the UI timer once per frame while the game is being played.
                    If pausing the game, the timer will freeze until the user is ready to continue playing again.
                    If the user decides to restart the level, the timer will start back from 00:00:00.
                    When the user quits the stage to go back to the main menu, the timer will reset back to default, and the user will have to play the entire level again.
                    */
        if (isInSuperForm)
        {
            SuperFormSettings();
        }
    }
    public void PlayerDamage(float damage, Vector3 player)
    {
        darkBar += damage; // When the player is damaged, the red bar will increase the time ready to activate Chaos Blast.
        darkScore++;
        LoseRing(player);
        ringUI.text = rings.ToString();
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
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        player.GetComponent<CharacterAnimationController>().Death();
    }
    private void GameOver()
    {
        /*
        Ends the stage when the player dies, and you can select which option to do next.
        Which is either to continue from the last checkpoint, restart the level, or exit the game and go back to the main menu.
        */
    }
    private void AddScore(int points)
    {
        normalScore += points;
    }
    public void SpawnRings(int rings, Vector3 player)
    {
        for (int i = 0; i < rings; i++)
        {
            Vector3 offSet = player + new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
           // Vector3 initialPosition = player;
            float angle = 45f * Mathf.Deg2Rad;
            Vector3 initialVelocity = new Vector3(0 * Mathf.Cos(angle), 5f * Mathf.Sin(angle), 0);
            GameObject newRings = Instantiate(ringsPrefab, offSet, (Quaternion.identity));
            Ring thisRing = newRings.GetComponent<Ring>();
            thisRing.FlashAndDisappear();
        }
    }
    public void AddRings()
    {
        rings++;
        ringUI.text = rings.ToString();
    }
    public void LoseRing(Vector3 player)
    {
        if (rings < 10)
        {
            SpawnRings(rings, player);
            rings -= rings;
        }
        else if (rings >= 10)
        {
            rings -= 10;
            SpawnRings(10, player);
        }
        else if (rings <= 0)
        {
            OnDeath();
        }
        ringUI.text = rings.ToString();
    }
    public void AmmoCounter(int count)
    {
        // The weapon ammo counter will reset each time a new weapon is picked up by the user.
    }
    private void HUDTimer()
    {
        eplisedTime = Time.realtimeSinceStartup;
        string formattedTime = FormatTime(eplisedTime);
        timerUI.text = formattedTime;
    }
    private string FormatTime(float time)
    {
        ringUI.text = rings.ToString();
        int minutes = Mathf.FloorToInt(time / 60f); // Shown in the UI.
        int seconds = Mathf.FloorToInt(time % 60); // Shown in the UI.
        int ms = Mathf.FloorToInt(time * 1000) % 100; // Shown in the UI. (using a remainder)
        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, ms); // Returns timer format in minutes, seconds, and milliseconds.
    }
    public void SetCheckPoint(Transform location)
    {
        spawnLocation = location;
        rings += 10;
        ringUI.text = rings.ToString();
    }
    private void SuperFormSettings()
    {
        isInSuperForm = false;
        rings = 50;
        ringUI.text = rings.ToString();
        InvokeRepeating("RingDrain", Time.deltaTime, 3f); // This will toggle and repeat.
    }
    private void RingDrain()
    {
        if (rings <= 0)
        {
            CancelInvoke();
            OnDeath();
        }
        ringUI.text = rings.ToString();
        rings -= 1; // Subtract per every 3 seconds.
    }
}