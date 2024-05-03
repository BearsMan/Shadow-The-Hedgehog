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
    public SkinnedMeshRenderer locationofSkin1;
    public SkinnedMeshRenderer locationOfSkin2;
    public Material normalSkin;
    public Material ultimateSkin;
    private bool changeSkin = false;
    // Start is called before the first frame update
    void Start()
    {
        chaosBlastSoundsSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (powerUpActive)
        {
            if (!changeSkin)
            {
                changeSkin = true;
                locationofSkin1.material = ultimateSkin;
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
                }
                    
                powerUpActive = false; // Set to true when timer for power up is active.
                changeSkin = false; // Changes the skin for the aura attacks.
                locationofSkin1.material = normalSkin;
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
}
