using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject PlayerBullet;
    public GameObject BulletPos;
    public float moveSpeed;
    public float maxHealth = 3; // Maximum health of the player
    float currentHealth; // Current health of the player
    public int maxBullets = 30; // Maximum number of bullets
    int remainingBullets; // Number of bullets remaining
    Rigidbody2D rb;
    Vector2 moveDirection;

    // Infinite ammo control
    bool hasInfiniteAmmo = false;
    float infiniteAmmoDuration = 15f;
    float currentInfiniteAmmoTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth; // set the current health to the maximum health
        remainingBullets = maxBullets; // set the remaining bullets to the maximum bullets
    }
    void Update()
    {
        InputManagement();

        // Shoot bullet upon pressing left mouse button if player has bullets or infinite ammo
        if (Input.GetMouseButtonDown(0) && (remainingBullets > 0 || hasInfiniteAmmo))
        {
            GameObject bullet = Instantiate(PlayerBullet);
            bullet.transform.position = BulletPos.transform.position; // Set the bullet's initial position

            if (!hasInfiniteAmmo)
                remainingBullets--; // Reduce remaining bullets only if not in infinite ammo mode

            Debug.Log("Remaining Bullets: " + remainingBullets); // Debug log remaining bullets count
        }

        // Update infinite ammo duration
        if (hasInfiniteAmmo)
        {
            currentInfiniteAmmoTime -= Time.deltaTime;
            if (currentInfiniteAmmoTime <= 0)
            {
                hasInfiniteAmmo = false;
                // Reset the player's ammo to a default value
            }
            else
            {
                Debug.Log("Infinite Ammo Active. Time Remaining: " + currentInfiniteAmmoTime); // Debug log infinite ammo status and remaining time
            }
        }
    }

    private void FixedUpdate()
    {
        Move(); // Move the player
    }

    void InputManagement() // Get the input from the player
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    void Move() // Move the player
    {
        rb.velocity = moveDirection * moveSpeed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Detect collision with the enemy bullet
        if (col.tag == "EnemyBullet")
        {
            Destroy(col.gameObject);
            currentHealth -= 1; // Decrease player's current health
            Debug.Log("Player hit by enemy bullet. Current health: " + currentHealth);

            if (currentHealth <= 0)
            {
                Destroy(gameObject); // Destroy the player if health reaches zero
                Debug.Log("Player ship destroyed.");
            }


        }

        // Detect collision with the boss bullet
        else if (col.tag == "BossBullet")
        {
            Destroy(col.gameObject);
            currentHealth -= 2.5f; // Decrease player's current health by 5
            Debug.Log("Player hit by boss bullet. Current health: " + currentHealth);

            if (currentHealth <= 0)
            {
                Destroy(gameObject); // Destroy the player if health reaches zero
                Debug.Log("Player ship destroyed.");
            }
        }

        // Detect collision with the boss bullet
        else if (col.tag == "BossBullet2")
        {
            Destroy(col.gameObject);
            currentHealth -= 3f; // Decrease player's current health by 5
            Debug.Log("Player hit by boss bullet. Current health: " + currentHealth);

            if (currentHealth <= 0)
            {
                Destroy(gameObject); // Destroy the player if health reaches zero
                Debug.Log("Player ship destroyed.");
            }
        }
        // Detect collision with the boss bullet
        else if (col.tag == "Fireball")
        {
            Destroy(col.gameObject);
            currentHealth -= 2.5f; // Decrease player's current health by 5
            Debug.Log("Player hit by fireball. Current health: " + currentHealth);

            if (currentHealth <= 0)
            {
                Destroy(gameObject); // Destroy the player if health reaches zero
                Debug.Log("Player ship destroyed.");
            }
        }
        else if (col.tag == "EnemyShip")
        {
            
            currentHealth -= 10f; // Decrease player's current health by 20
            Debug.Log("Player hit by enemy ship. Current health: " + currentHealth);

            if (currentHealth <= 0)
            {
                Destroy(gameObject); // Destroy the player if health reaches zero
                Debug.Log("Player ship destroyed.");
            }
        }

        else if (col.tag == "EnemyBullet3")
        {
            Destroy(col.gameObject);
            currentHealth -= 2f; // Decrease player's current health by 5
            Debug.Log("Player hit by fireball. Current health: " + currentHealth);

            if (currentHealth <= 0)
            {
                Destroy(gameObject); // Destroy the player if health reaches zero
                Debug.Log("Player ship destroyed.");
            }
        }
        // Handle collision with pickup objects
        if (col.CompareTag("InfiniteAmmoPickup"))
        {
            // Activate infinite ammo effect
            ActivateInfiniteAmmo();
            Destroy(col.gameObject);
            Debug.Log("Infinite Ammo Pickup Collected"); 
        }
    }

    // Method to activate infinite ammo pickup effect
    public void ActivateInfiniteAmmo()
    {
        hasInfiniteAmmo = true;
        currentInfiniteAmmoTime = infiniteAmmoDuration;
        Debug.Log("Infinite Ammo Activated"); 
    }
    public void AddBullets(int amount)
    {
        remainingBullets += amount; // Add bullets to the player's remaining bullets
    }
    public void RestoreHealth(float amount)
    {
        currentHealth += (int)amount;
        // Make sure health does not exceed the maax
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        Debug.Log("Player health restored. Current health: " + currentHealth);
    }
}