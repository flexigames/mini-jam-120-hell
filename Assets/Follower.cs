using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    Player player;
    Movement movement;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        movement = GetComponent<Movement>();
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > player.followerRange)
        {
            MoveTowardsPlayer();
            return;
        }

        MoveTowardsClosestEnemy();
    }

    void MoveTowardsPlayer()
    {
        movement.SetTarget(player.transform);
    }

    void MoveTowardsClosestEnemy()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistance = float.MaxValue;
        foreach (var enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer + 0.5 > player.followerRange)
        {
            movement.Stop();
            return;
        }

        if (closestEnemy != null)
        {
            movement.SetTarget(closestEnemy);
        }
    }
}
