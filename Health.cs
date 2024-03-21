using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health = 100;
    public Slider healthSlider;
    public bool hasHealthSlider = true;
    public Animator healthAnim;
    // Start is called before the first frame update
    void Start()
    {
        healthAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnHit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            healthAnim.SetBool("Death", true);
            gameObject.GetComponent<EnemyAI>().enabled = false;
        }
        healthAnim.SetTrigger("HitBox");
        if (hasHealthSlider)
        {
            if (healthSlider == null)
            {
                healthSlider = Instantiate(healthSlider, transform);
                // Debug.Log("Spawn health bar");
            }
            else
            {
                healthSlider.value = health;
            }
            /*
            healthSlider.gameObject.GetComponentInChildren<Slider>().gameObject.SetActive(true);
            healthSlider.gameObject.gameObject.SetActive(false);
            healthSlider.enabled = true;
            */
        }
        else
        {
            healthSlider.enabled = false;
        }
    }
}
