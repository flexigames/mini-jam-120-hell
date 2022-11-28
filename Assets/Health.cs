using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float health;

    void Start()
    {
        Reset();
    }

    public void Reset()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
