using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShooting : MonoBehaviour
{ 

    [SerializeField] private List<Bullet> bullets;

    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform fireTranform;
    [SerializeField] private GameObject turret;

    [SerializeField] private Transform player;

    [SerializeField] private float attackRange;

    private float timer = Mathf.Infinity;

    private bool fire;

    


    private void Update()
    {
        timer += Time.deltaTime;
        if (fire)
        {
            //turret.transform.LookAt(player);
            if(timer >= attackCooldown)
            {
                timer = 0;
                Fire();
            }
        }
    }

    public void SetFire(bool value)
    {
        fire = value;
    }

    private void Fire()
    {
        int i = GetBullet();
        bullets[i].Init(fireTranform, 10f, attackRange, transform.position);
        
      
    }

    private int GetBullet()
    {
        for(int i = 0; i < bullets.Count; i++)
        {
            if (bullets[i].gameObject.activeInHierarchy)
            {
                continue;
            }
            return i;
        }
        return 0;
    }
}
