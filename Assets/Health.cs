using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float health;

    private Shader shaderGUItext;
    private Shader shaderSpritesDefault;

    SpriteRenderer spriteRenderer;

    void Start()
    {
        Reset();
        spriteRenderer = GetComponent<SpriteRenderer>();
        shaderGUItext = Shader.Find("GUI/Text Shader");
        shaderSpritesDefault = spriteRenderer.material.shader;
    }

    public void Reset()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        FlashDamage();

        var enemy = GetComponent<Enemy>();
        if (enemy != null && health <= 0)
            enemy.OnDeath();

        var player = GetComponent<Player>();
        if (player != null)
        {
            Sounds.Play("Hurt");
            if (health <= 0)
                player.OnDeath();
        }
    }

    void FlashDamage()
    {
        StartCoroutine(FlashDamageCoroutine());
    }

    IEnumerator FlashDamageCoroutine()
    {
        spriteRenderer.material.shader = shaderGUItext;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material.shader = shaderSpritesDefault;
    }
}
