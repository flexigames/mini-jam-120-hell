using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum WeaponState
{
    Idle,
    Firing,
    Returning
}

public class Weapon : MonoBehaviour
{
    public static float damage = 10f;
    public float coolDown;
    public float range;
    public float speed;

    Collider2D weaponCollider;

    WeaponState state = WeaponState.Idle;

    float remainingCoolDown = 0f;

    float firingAngle;
    Vector3 startPoint;

    void Start()
    {
        weaponCollider = GetComponent<Collider2D>();
        weaponCollider.enabled = false;
    }

    void Update()
    {
        if (state == WeaponState.Idle)
            HandleIdle();

        if (state == WeaponState.Firing)
            HandleFiring();

        if (state == WeaponState.Returning)
            HandleReturning();
    }

    void HandleIdle()
    {
        var (closestEnemy, enemyDistance) = FindClosestEnemy();

        if (closestEnemy == null)
            return;

        var angle = PointTowardsEnemy(closestEnemy);

        if (enemyDistance <= range && remainingCoolDown <= 0f)
        {
            Fire(angle);
            remainingCoolDown = coolDown;
        }

        if (remainingCoolDown > 0f)
            remainingCoolDown -= Time.deltaTime;
    }

    void HandleFiring()
    {
        transform.position +=
            new Vector3(
                Mathf.Cos(firingAngle * Mathf.Deg2Rad),
                Mathf.Sin(firingAngle * Mathf.Deg2Rad),
                0
            )
            * speed
            * Time.deltaTime;

        if (Vector3.Distance(transform.position, startPoint) >= range)
        {
            weaponCollider.enabled = false;
            state = WeaponState.Returning;
        }
    }

    void HandleReturning()
    {
        transform.localPosition = Vector3.MoveTowards(
            transform.localPosition,
            new Vector3(0, 0, 0),
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.localPosition, new Vector3(0, 0, 0)) <= 0.01f)
            state = WeaponState.Idle;
    }

    void Fire(float angle)
    {
        Sounds.Play("Sword_Slash");
        state = WeaponState.Firing;
        weaponCollider.enabled = true;

        startPoint = transform.position;
        firingAngle = angle;
    }

    float PointTowardsEnemy(GameObject enemy)
    {
        var direction = enemy.transform.position - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

        return angle;
    }

    (GameObject, float) FindClosestEnemy()
    {
        float closestDistance = float.MaxValue;
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
            return (null, 0f);

        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return (closestEnemy, closestDistance);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var parent = other.transform.parent;

        if (parent == null)
            return;

        if (parent.CompareTag("Enemy") && state == WeaponState.Firing)
        {
            Sounds.Play("Bump");
            parent.GetComponent<Health>().TakeDamage(Weapon.damage);
        }
    }
}
