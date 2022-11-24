using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float coolDown;
    public float health;

    float remainingCoolDown = 0f;

    void Update()
    {
        HandleMovement();

        if (remainingCoolDown > 0f)
            remainingCoolDown -= Time.deltaTime;
    }

    void HandleMovement()
    {
        var target = FindClosestTarget();

        if (target == null)
            return;

        Vector3 direction = target.transform.position - transform.position;

        direction.Normalize();

        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (remainingCoolDown > 0f)
            return;

        if (other.gameObject.tag != "Player" || other.gameObject.tag != "Follower")
            return;

        var health = other.GetComponent<Health>();

        if (health != null)
        {
            health.TakeDamage(1f);
            remainingCoolDown = coolDown;
        }
    }

    GameObject FindClosestTarget()
    {
        var followers = GameObject.FindGameObjectsWithTag("Follower");
        var player = GameObject.FindGameObjectWithTag("Player");

        float closestDistance = Vector3.Distance(transform.position, player.transform.position);
        var target = player;

        foreach (GameObject candidate in followers)
        {
            float distance = Vector3.Distance(transform.position, candidate.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = candidate;
            }
        }

        return target;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
            Destroy(gameObject);
    }
}
