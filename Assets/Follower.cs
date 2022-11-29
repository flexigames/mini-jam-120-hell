using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    Player player;
    Movement movement;

    public Weapon weapon;

    GameObject currentTarget;

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

        CheckCurrentTargetDistance();
    }

    void CheckCurrentTargetDistance()
    {
        if (currentTarget == null)
            return;

        var distance = Vector3.Distance(transform.position, currentTarget.transform.position);

        if (distance > 1.5)
        {
            currentTarget = null;
        }
    }

    void HandleMovement()
    {
        MoveTowardsClosestEnemy();
    }

    void MoveTowardsClosestEnemy()
    {
        if (currentTarget != null)
        {
            movement.SetTarget(currentTarget);
            return;
        }

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
            currentTarget = closestEnemy;
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
