using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public float speed;

    Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();
        transform.position += direction * speed * Time.deltaTime;
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

        if (closestEnemy != null)
        {
            Vector3 direction = closestEnemy.transform.position - transform.position;
            direction.Normalize();

            var newPosition = transform.position + direction * speed * Time.deltaTime;

            if (Vector3.Distance(newPosition, player.transform.position) <= player.followerRange)
            {
                transform.position = newPosition;
            }
        }
    }
}
