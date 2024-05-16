using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateAttacks : MonoBehaviour
{
    public bool chaosBlastAttack = true;
    private bool chaosControlActive = false;
    public bool powerUpActive = true;
    public AudioClip chaosControlSound;
    public List<AudioClip> chaosBlastSounds;
    private AudioSource SoundsSource;
    public GameObject chaosBlastSphereForm;
    public SkinnedMeshRenderer locationOfSkin1;
    public SkinnedMeshRenderer locationOfSkin2;
    public Material normalSkin;
    public Material ultimateEvilSkin;
    public Material ultimateGoodSkin;
    private bool changeSkin = false;
    public GameObject checkPoints;
    public Transform[] targetPosition;
    public float chaosControlSpeed = 2.0f;
    public float arrivalThreshold = 0.1f;
    private int currentTargetIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
         SoundsSource = GetComponent<AudioSource>();
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

            if (chaosBlastAttack)
            {
                if (!changeSkin)
                {
                    changeSkin = true;
                    locationOfSkin1.material = ultimateEvilSkin;
                    locationOfSkin2.material = ultimateEvilSkin;
                }
                // To use either Chaos Control or Chaos Blast, depending on the color of the bar being hit by the most.
                if (Input.GetKeyDown(KeyCode.M))
                {
                    // Chaos Blast is being used.
                    GameObject blast = Instantiate(chaosBlastSphereForm, transform);
                    blast.transform.parent = null;
                    SoundsSource.PlayOneShot(chaosBlastSounds[0]); // This is an array.
                    EndOfUltimate();
                    changeSkin = false;
                }

            }
            else
            {
                if (!changeSkin)
                {
                    changeSkin = true;
                    locationOfSkin1.material = ultimateGoodSkin;
                    locationOfSkin2.material = ultimateGoodSkin;
                }
                if (Input.GetKeyDown(KeyCode.M))
                {
                    SoundsSource.PlayOneShot(chaosControlSound);
                    EndOfUltimate();
                    changeSkin = false;
                    chaosControlActive = true;
                }
            }
        }
        if (chaosControlActive)
        {
            FlyToNextPosition();
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
    
    private void FlyToNextPosition()
    {
        
        // Debug.Log("Fly");
        if (currentTargetIndex > targetPosition.Length)
        {
            Vector3 flyPosition = targetPosition[0].position;
            // Debug.Log(flyPosition);
            Debug.Log(currentTargetIndex);

            if (Vector3.Distance(transform.position, flyPosition) > arrivalThreshold)
            {
                transform.position = Vector3.MoveTowards(transform.position, flyPosition, chaosControlSpeed * Time.deltaTime);
                // Debug.Log("Current Target Index");
                // yield return null; // returns none.
            }
            else
            {
                currentTargetIndex++; // Move to the next position.
            }
        }
        if (currentTargetIndex < targetPosition.Length)
        {
            // yield return new WaitForSeconds(1f); // Timer to wait for the next jump to the next checkpoint.
        }
    }
    

    private void EndOfUltimate()
    {
        powerUpActive = false;
        changeSkin = false; // Changes the skin for the aura attacks.
        locationOfSkin1.material = normalSkin;
        locationOfSkin2.material = normalSkin;
        GameManager.instance.ClearAttackBars(); // This clears the attack call phases for light and dark attacks.
    }
}
