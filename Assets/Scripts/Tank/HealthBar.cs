using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Slider easeHealthBar;

    public Transform camera;

    private float maxHealth = 100f;
    private float health;

    private float lerpSpeed = 0.02f;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        health = maxHealth;

        healthBar.maxValue = health;
        healthBar.value = health;

        easeHealthBar.maxValue = health;
        easeHealthBar.value = health;
    }

    public bool TakeDamage(int damage)
    {
        health -= damage;
        healthBar.value = health;
        if(health <= 0)
        {
            return true;
        }
        return false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(50);
            Debug.Log("Take damage");
        }

        if(Mathf.Abs(easeHealthBar.value - health) > 0.01f)
        {
            easeHealthBar.value = Mathf.Lerp(easeHealthBar.value, health, lerpSpeed);
            //Debug.Log(easeHealthBar.value);
        }
        //transform
    }

    /// <summary>
    /// allway look at the camera
    /// </summary>
    private void LateUpdate()
    {
        transform.LookAt(camera);
    }
}
