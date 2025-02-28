using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlayerHealth : MonoBehaviour
{
    public Enemy enemyScript;
    public GameObject player;
    public float thrust;
    public float knockTime;
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
        Debug.Log("Enemy");
    
        Rigidbody2D playerRb = GetComponent<Rigidbody2D>(); 
        if (playerRb != null)
        {
            
            Vector2 difference = transform.position - enemyCollision.transform.position;
            difference = difference.normalized * thrust;
            playerRb.AddForce(difference, ForceMode2D.Impulse); 

            StartCoroutine(KnockCo(playerRb)); 
        }

        
        Rigidbody2D enemy = enemyCollision.collider.GetComponent<Rigidbody2D>();
        if (enemy != null)
        {
            Vector2 enemyDifference = enemy.transform.position - transform.position;
            enemyDifference = enemyDifference.normalized * thrust;
            enemy.AddForce(enemyDifference, ForceMode2D.Impulse);

            StartCoroutine(KnockCo(enemy));
        }
    }
}


    private IEnumerator KnockCo(Rigidbody2D enemy)
    {
        if (enemy != null)
        {
            yield return new WaitForSeconds(knockTime);
            enemy.linearVelocity = Vector2.zero;
            
        }
    }

}
