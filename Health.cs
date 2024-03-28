using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public GameObject UI;
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
            StartCoroutine(Delay());
        }
        healthAnim.SetTrigger("HitBox");
        if (!hasHealthSlider)
        {
            UI = Instantiate(UI);
            healthSlider = UI.GetComponentInChildren<Slider>();
            hasHealthSlider = true;
        }


        else
        {
            healthSlider.value = health;
        }
    }
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1);
        Destroy (gameObject);
    }
}
