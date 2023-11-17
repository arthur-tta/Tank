using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rigidbody;
    private float attackRange;

    private Vector3 startPosition;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Init(Transform fireTranform, float speed, float attackRange, Vector3 startPosition)
    {
        gameObject.SetActive(true);

        transform.position = fireTranform.position;
        transform.rotation = fireTranform.rotation;
        rigidbody.velocity = fireTranform.forward * speed;

        this.attackRange = attackRange;
        this.startPosition = startPosition;
    }

    private void Update()
    {
        // dan bay qua tam se phat no
        if(Vector3.Distance(transform.position, startPosition) >= attackRange)
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
}
