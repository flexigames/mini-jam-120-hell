using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float coolDown;
    public float health;

    float remainingCoolDown = 0f;

    bool isAttacking = false;

    Movement movement;

    void Start()
    {
        movement = GetComponent<Movement>();
    }

    void Update()
    {
        HandleMovement();

        if (remainingCoolDown > 0f)
            remainingCoolDown -= Time.deltaTime;
    }

    void HandleMovement()
    {
        if (isAttacking)
        {
            movement.Stop();
            return;
        }

        var target = FindClosestTarget();

        if (target == null)
        {
            movement.Stop();
            return;
        }

        movement.SetTarget(target);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "Follower")
            return;

        isAttacking = true;

        Attack(other.gameObject);
    }

    void Attack(GameObject target)
    {
        if (remainingCoolDown > 0f)
            return;

        var health = target.GetComponent<Health>();

        if (health != null)
        {
            health.TakeDamage(1f);
            remainingCoolDown = coolDown;
        }
    }

    void OnTriggerExit2D()
    {
        isAttacking = false;
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
