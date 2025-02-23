using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 10;
    public Image healthBar;
    public float currentHealth;
    private bool isTakingDamage = false;
    private Coroutine damageCoroutine;
    private HashSet<Collider2D> touchingAcid = new HashSet<Collider2D>();

    void Start()
    {
        currentHealth = maxHealth;
    }
    
    private IEnumerator DamageOverTime()
    {
        while (touchingAcid.Count > 0) 
        {
            yield return new WaitForSeconds(0.25f);
            TakeDamage(1);
        }
        isTakingDamage = false; 
        damageCoroutine = null;  
    }
    


    void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if(currentHealth <= 0)
        {
         SceneManager.LoadScene("DeathScreen");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healthBar.fillAmount = Mathf.Clamp(currentHealth / maxHealth, 0, 1);
    }
    
    
    private void OnTriggerEnter2D(Collider2D hurtingCollider)
    {
        if (hurtingCollider.gameObject.CompareTag("Acid"))
        {
            
            touchingAcid.Add(hurtingCollider); 

            if (!isTakingDamage)
            {
                isTakingDamage = true;
                damageCoroutine = StartCoroutine(DamageOverTime());
            }
        
        }
    }

    private void OnTriggerExit2D(Collider2D hurtingCollider)
    {
        if (hurtingCollider.gameObject.CompareTag("Acid"))
        {
           touchingAcid.Remove(hurtingCollider);

            if (touchingAcid.Count == 0 && damageCoroutine != null) 
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
                isTakingDamage = false;
            }
        }

        
    }


    private void OnCollisionEnter2D(Collision2D enemyCollision)
{
    if (enemyCollision.gameObject.CompareTag("Enemy"))
    {
        TakeDamage(1);

        Rigidbody2D playerRb = GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            Vector2 knockbackDirection = (transform.position - enemyCollision.transform.position).normalized;
            float knockbackStrength = 3f; // Adjusted knockback strength
            playerRb.AddForce(knockbackDirection * knockbackStrength, ForceMode2D.Impulse);
        }

        if (!isTakingDamage)
        {
            isTakingDamage = true;
            damageCoroutine = StartCoroutine(DamageOverTime());
        }
    }
}


    private void OnCollisionExit2D(Collision2D enemyCollision)
    {
        if (enemyCollision.gameObject.CompareTag("Enemy"))
        {
            isTakingDamage = false;
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine); 
                damageCoroutine = null;
            }
        }

        
    }
}
