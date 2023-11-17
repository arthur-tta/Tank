using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TankHealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Slider easeHealthBar;

    public TextMeshProUGUI hpTxt;

    private int maxHealth;
    private int currentHealth;

    private float lerpSpeed = 0.02f;


    public void Init(int maxHealth)
    {
        this.maxHealth = maxHealth;
        currentHealth = this.maxHealth;

        healthBar.maxValue = this.maxHealth;
        healthBar.value = currentHealth;

        easeHealthBar.maxValue = currentHealth;
        easeHealthBar.value = currentHealth;

        UpdateHPText();
    }

    private void UpdateHPText()
    {
        hpTxt.text = "HP: " + currentHealth + "/" + maxHealth;

    }

    public bool TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;
        if(currentHealth <= 0)
        {
            return true;
        }

        UpdateHPText();
        return false;
    }

    private void Update()
    {
        if(Mathf.Abs(easeHealthBar.value - currentHealth) > 0.01f)
        {
            easeHealthBar.value = Mathf.Lerp(easeHealthBar.value, currentHealth, lerpSpeed);
        }
    }

   
}
