using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    Player player;
    Movement movement;

    public Weapon weapon;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        movement = GetComponent<Movement>();
        SetUpMaxDistanceToPlayer();
    }

    void Update()
    {
        if (Game.isPaused)
            return;

        HandleMovement();

        UpdateMaxDistance();
    }

    void HandleMovement()
    {
        MoveTowardsClosestEnemy();
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
            movement.SetTarget(closestEnemy);
        }
    }

    void SetUpMaxDistanceToPlayer()
    {
        var distanceJoint = GetComponent<DistanceJoint2D>();
        distanceJoint.distance = player.followerRange;
        distanceJoint.connectedBody = player.GetComponent<Rigidbody2D>();
    }

    void UpdateMaxDistance()
    {
        var distanceJoint = GetComponent<DistanceJoint2D>();
        distanceJoint.distance = player.followerRange;
    }
}
