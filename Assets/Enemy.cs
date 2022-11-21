using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float coolDown;
    public float health;

    GameObject player;

    float remainingCoolDown = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Vector3 direction = player.transform.position - transform.position;

        direction.Normalize();

        transform.position += direction * speed * Time.deltaTime;

        if (remainingCoolDown > 0f)
            remainingCoolDown -= Time.deltaTime;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && remainingCoolDown <= 0)
        {
            other.GetComponent<Player>().TakeDamage(1);

            remainingCoolDown = coolDown;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
            Destroy(gameObject);
    }
}
