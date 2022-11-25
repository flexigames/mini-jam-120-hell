using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public float speed;
    public float followerRange;

    int xp = 0;

    public TextMeshProUGUI lifeText;

    public TextMeshProUGUI xpText;

    void Update()
    {
        HandleMovement();

        UpdateText();
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

    void UpdateText()
    {
        lifeText.text = "Life: " + GetComponent<Health>().health;
        xpText.text = "XP: " + xp;
    }

    public void AddXP(int amount)
    {
        xp += amount;
    }
}
