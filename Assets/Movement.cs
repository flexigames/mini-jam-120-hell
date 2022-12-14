using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;

    Rigidbody2D rigidBody;

    SpriteRenderer spriteRenderer;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        var velocity = rigidBody.velocity;

        if (velocity.x > 0)
            spriteRenderer.flipX = false;
        else if (velocity.x < 0)
            spriteRenderer.flipX = true;
    }

    public void SetDirection(Vector3 direction)
    {
        direction.Normalize();
        rigidBody.velocity = direction * speed;
    }

    public void SetTarget(Vector3 target)
    {
        Vector3 direction = target - transform.position;
        SetDirection(direction);
    }

    public void SetTarget(Transform target)
    {
        SetTarget(target.position);
    }

    public void SetTarget(GameObject target)
    {
        SetTarget(target.transform.position);
    }

    public void Stop()
    {
        rigidBody.velocity = Vector3.zero;
    }
}
