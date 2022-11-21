using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public float speed;
    public float life;

    public TextMeshProUGUI lifeText;

    void Update()
    {
        HandleMovement();

        UpdateLifeText();
    }

    void HandleMovement()
    {
        Vector3 direction = new Vector3(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"),
            0
        );

        if (direction.magnitude < 0.01f)
            return;

        direction.Normalize();

        transform.position += direction * speed * Time.deltaTime;
    }

    void UpdateLifeText()
    {
        lifeText.text = life.ToString("0") + " HP";
    }

    public void TakeDamage(float damage)
    {
        life -= damage;
    }
}
