using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 5;
    public Image healthBar;
    public float currentHealth;
    private bool isTakingDamage = false;
    private Coroutine damageCoroutine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }
    
    private IEnumerator DamageOverTime()
    {
        while (isTakingDamage)
        {
            yield return new WaitForSeconds(1f); // Damage every 2 seconds
            TakeDamage(1);
        }
    }


    void TakeDamage(int amount){
        currentHealth -= amount;

        if(currentHealth <= 0){
         // death screen ...
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
            TakeDamage(1);
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
            isTakingDamage = false;
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine); 
                damageCoroutine = null;
            }
        }

        
    }


    private void OnColliderEnter2D(Collider2D enemyCollider)
    {
        if (enemyCollider.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(1);
            if (!isTakingDamage) 
            {
                isTakingDamage = true;
                damageCoroutine = StartCoroutine(DamageOverTime());
            }
        }
    }

    private void OnColliderExit2D(Collider2D enemyCollider)
    {
        if (enemyCollider.gameObject.CompareTag("Enemy"))
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
