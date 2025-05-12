using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int health = 100;
    public DisplayBar healthBar;
    private Rigidbody2D rb;
    public float knockbackForce = 5f;
    public GameObject playerDeathEffect;
    public static bool hitRecently = false;
    public float hitRecoveryTime = 0.2f;

    public AudioSource playerAudio;
    public AudioClip deathSound;
    public AudioClip hitSound;
    // public AudioClip playerDeathSound;
    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found on the player");
        }

        healthBar.SetMaxValue(health);
        hitRecently = false;
    }

    public void Knockback(Vector3 enemyPosition)
    {
        if (hitRecently)
        {
            return;
        }

        hitRecently = true;

        if (gameObject.activeSelf)
        {
            StartCoroutine(RecoverFromHit());
        }

        Vector2 direction = transform.position - enemyPosition;
        direction.Normalize();
        direction.y = direction.y * 0.5f + 0.5f;
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
    }

    IEnumerator RecoverFromHit()
    {
        yield return new WaitForSeconds(hitRecoveryTime);
        hitRecently = false;

        animator.SetBool("hit", false);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetValue(health);

        animator.SetBool("hit", true);

        if (health <= 0)
        {
            Die();
        }
        else
        {
            playerAudio.PlayOneShot(hitSound);
        }
    }

    public void Die()
    {
        ScoreManager.gameOver = true;
        GameObject deathEffect = Instantiate(playerDeathEffect, transform.position, Quaternion.identity);
        // playerAudio.PlayOneShot(deathSound);
        // Destroy(deathEffect, 2f);

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}