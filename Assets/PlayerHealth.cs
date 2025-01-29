using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 5;
    public Image healthBar;
    public float currentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
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
        if (currentHealth > 100)
        {
            currentHealth = 100;
        }
        healthBar.fillAmount = Mathf.Clamp(currentHealth / maxHealth, 0, 1);

    }
    
    private void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.CompareTag("Acid") ){
            TakeDamage(1);
        }
    

}}
