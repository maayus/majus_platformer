using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 6;

    public List<Image> hearts;

    public Sprite full_heart;
    public Sprite half_heart;
    public Sprite null_heart;

    private int health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            if (health >= (i+1) * 2)
            {
                hearts[i].sprite = full_heart;
            }
            else if (health == (i * 2) + 1)
            {
                hearts[i].sprite = half_heart;
            }
            else
            {
                hearts[i].sprite = null_heart;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
        }
        if (health == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        UpdateHearts();

    }
}
