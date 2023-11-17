using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealthSystem : MonoBehaviour
{
    [SerializeField] private AIHealthBar aIHealthBar;

    [SerializeField] private int health;

    



    private void OnEnable()
    {
        aIHealthBar.Init(health);
    }

    public void TakeDamage(int damage)
    {
        if (aIHealthBar.TakeDamage(damage))
        {
            OnDeath();
        }

        Debug.Log("Take " + damage + " damage!");
    }

    private void OnDeath()
    {
        Destroy(gameObject);
    }
}
