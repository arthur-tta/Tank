using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIHealthBar : MonoBehaviour
{
    [SerializeField] private Slider healthBar;

    [SerializeField] private float lowHealthThreshold = 50f;      // nguong mau trung binh
    [SerializeField] private float criticalHealthThreshold = 20f; // nguong mau thap

    private int maxHealth;
    private int currentHealth;


    public void Init(int maxHealth)
    {
        this.maxHealth = maxHealth;
        currentHealth = this.maxHealth;

        healthBar.maxValue = this.maxHealth;
        healthBar.value = currentHealth;
    }

    public bool TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;

        if(currentHealth <= 0)
        {
            return true;
        }
        return false;
    }

    public bool isLowHealthThreshold()
    {
        if(currentHealth * 100 / maxHealth < lowHealthThreshold)
        {
            return true;
        }
        return false;
    }

    public bool isCriticalHealthThreshold()
    {
        if (currentHealth * 100 / maxHealth < criticalHealthThreshold)
        {
            return true;
        }
        return false;
    }
}
