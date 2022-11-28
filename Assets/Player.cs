using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    public float followerRange;

    public float pickupRange;

    public GameObject choicesUi;

    int level = 1;

    int xp = 0;

    public TextMeshProUGUI lifeText;

    public GameObject xpBar;

    public GameObject followerPrefab;

    Movement movement;

    void Start()
    {
        Sounds.Play("Evil_Laugh");
        movement = GetComponent<Movement>();
    }

    void Update()
    {
        if (Game.isPaused)
            return;

        HandleMovement();

        UpdateUI();

        PullDropsWithinRange();
    }

    void HandleMovement()
    {
        Vector3 direction = new Vector3(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"),
            0
        );

        movement.SetDirection(direction);
    }

    void UpdateUI()
    {
        lifeText.text =
            "Life: " + GetComponent<Health>().health + "/" + GetComponent<Health>().maxHealth;
        xpBar.transform.localScale = new Vector3((float)xp / getLevelUpCost(), 1, 1);
    }

    public void AddXP(int amount)
    {
        xp += amount;
        if (xp >= getLevelUpCost())
        {
            xp -= getLevelUpCost();
            LevelUp();
        }
    }

    void LevelUp()
    {
        Sounds.Play("Pause");
        level++;
        UpdateUI();
        Game.isPaused = true;
        Time.timeScale = 0;
        choicesUi.SetActive(true);
    }

    public void SpawnFollower()
    {
        Instantiate(followerPrefab, transform.position, Quaternion.identity);
    }

    int getLevelUpCost()
    {
        return 5 + (level - 1) * 10;
    }

    void PullDropsWithinRange()
    {
        var pickups = GameObject.FindGameObjectsWithTag("Drop");

        foreach (var pickup in pickups)
        {
            float distance = Vector3.Distance(transform.position, pickup.transform.position);
            if (distance < pickupRange)
            {
                pickup.transform.position = Vector3.MoveTowards(
                    pickup.transform.position,
                    transform.position,
                    (movement.speed + 1) * Time.deltaTime
                );
            }
        }
    }

    public void OnDeath()
    {
        SceneManager.LoadScene("GameOver");
    }
}
