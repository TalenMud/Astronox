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
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Acid"))
        {
            TakeDamage(1); // Initial damage
            if (!isTakingDamage) // Prevent multiple coroutines from stacking
            {
                isTakingDamage = true;
                damageCoroutine = StartCoroutine(DamageOverTime());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Acid"))
        {
            isTakingDamage = false;
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine); // Stop damage coroutine
                damageCoroutine = null;
            }
        }
    }
}
