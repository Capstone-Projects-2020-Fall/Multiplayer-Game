using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hero : MonoBehaviour
{
    public float health;
    public float mana;
    private HeroHealth healthBar;
    private HeroHealth healthBarMini;


    private void Start()
    {
        healthBar = (HeroHealth)GameObject.Find("Health Bar").GetComponent("HeroHealth");
        healthBar.SetHealth(health);

        healthBarMini = (HeroHealth)GameObject.Find("Health Bar Mini").GetComponent("HeroHealth");
        healthBarMini.SetHealth(health);


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health--;
            healthBar.SetHealth(health);

            healthBarMini.SetHealth(health);
            if (health <= 0)
            {
                Time.timeScale = 0;
                SceneManager.LoadScene("Defeat");
            }
        }
    }
}
