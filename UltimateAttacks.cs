using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateAttacks : MonoBehaviour
{
    public bool chaosBlastAttack = true; // Check has to be manually set.
    public bool powerUpActive = true;
    public List<AudioClip> chaosBlastSounds;
    private AudioSource chaosBlastSoundsSource;
    public GameObject chaosBlastSphereForm;
    public SkinnedMeshRenderer locationOfSkin1;
    public SkinnedMeshRenderer locationOfSkin2;
    public Material normalSkin;
    public Material ultimateSkin;
    private bool changeSkin = false;
    public GameObject checkPoints;
    public Transform[] targetPosition;
    public float chaosControlSpeed = 2.0f;
    public float arrivalThreshold = 0.1f;
    private int currentTargetIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        chaosBlastSoundsSource = GetComponent<AudioSource>();
        /*
        for (int i = 0; i < checkPoints.transform.childCount; i++)
        {
            Transform childTransform = checkPoints.transform.GetChild(i).transform; // This is filled with child objects.
            targetPosition[i] = childTransform;
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (powerUpActive)
        {
            if (!changeSkin)
            {
                changeSkin = true;
                locationOfSkin1.material = ultimateSkin;
                locationOfSkin2.material = ultimateSkin;
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                // To use either Chaos Control or Chaos Blast, depending on the color of the bar being hit by the most.
                if (chaosBlastAttack)
                {
                    // Chaos Blast is being used.
                    GameObject blast = Instantiate(chaosBlastSphereForm, transform);
                    blast.transform.parent = null;
                    chaosBlastSoundsSource.PlayOneShot(chaosBlastSounds[0]); // This is an array.
                }
                else
                {
                    // Chaos Control is being used instead.
                    // StartCoroutine(FlyToNextPosition());
                    // Debug.Log("Chaos Control!");
                }
                    
                powerUpActive = false; // Set to true when timer for power up is active.
                changeSkin = false; // Changes the skin for the aura attacks.
                locationOfSkin1.material = normalSkin;
                locationOfSkin2.material = normalSkin;
                GameManager.instance.ClearAttackBars(); // This clears the attack call phases for light and dark attacks.

            }
        }
    }
    // Setup Ultimate Attacks for Shadow
    public void SuperAttack(bool chaosBlast)
    {
        chaosBlastAttack = chaosBlast;
        if (chaosBlast)
        {
            // Make sure that the red bar is completely filled or else the attack cannot be called.
        }
        else
        {
            // Use Chaos Control when the blue bar is filled, depending on how the player reacts.
        }
    }
    /*
    private IEnumerator FlyToNextPosition()
    {
        
        Debug.Log("Fly");
        while (currentTargetIndex > targetPosition.Length)
        {
            Vector3 flyPosition = targetPosition[currentTargetIndex].position;
            // Debug.Log(flyPosition);

            while (Vector3.Distance(transform.position, flyPosition) > arrivalThreshold)
            {
                transform.position = Vector3.MoveTowards(transform.position, flyPosition, chaosControlSpeed * Time.deltaTime);
                // Debug.Log("Current Target Index");
                yield return null; // returns none.
            }
        }
        currentTargetIndex++; // Move to the next position.
        if (currentTargetIndex < targetPosition.Length)
        {
            yield return new WaitForSeconds(1f); // Timer to wait for the next jump to the next checkpoint.
        }
    }
    */
}
