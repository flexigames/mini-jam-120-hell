using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float coolDown;

    public GameObject deathDrop;

    float remainingCoolDown = 0f;

    bool isAttacking = false;

    Movement movement;

    float timeUntilActive = 1f;

    void Start()
    {
        movement = GetComponent<Movement>();

        GetComponent<Collider2D>().enabled = false;
        gameObject.tag = "Untagged";
        ChangeAlpha(0.5f);
    }

    void ChangeAlpha(float alpha)
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(
            spriteRenderer.color.r,
            spriteRenderer.color.g,
            spriteRenderer.color.b,
            alpha
        );
    }

    void Update()
    {
        if (Game.isPaused)
            return;

        if (timeUntilActive > 0f)
        {
            timeUntilActive -= Time.deltaTime;

            if (timeUntilActive <= 0f)
            {
                GetComponent<Collider2D>().enabled = true;
                gameObject.tag = "Enemy";
                ChangeAlpha(1f);
            }

            return;
        }

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

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag != "Player")
            return;

        isAttacking = true;

        Attack(other.gameObject);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        isAttacking = false;
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

    GameObject FindClosestTarget()
    {
        var followers = GameObject.FindGameObjectsWithTag("Follower");
        var player = GameObject.FindGameObjectWithTag("Player");

        float closestDistance = Vector3.Distance(transform.position, player.transform.position);
        var target = player;

        return target;
    }

    public void OnDeath()
    {
        Destroy(gameObject);
        if (deathDrop != null)
            Instantiate(deathDrop, transform.position, Quaternion.identity);
    }
}
