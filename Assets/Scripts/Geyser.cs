using UnityEngine;

public class Geyser : MonoBehaviour
{
    public float upwardForce = 50f;
    public LayerMask phaseablePlatforms;
    private bool playerInGeyser = false;
    private GameObject player;
    private Rigidbody2D playerRb;
    private Collider2D playerCollider;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInGeyser = true;
            player = other.gameObject;
            playerRb = player.GetComponent<Rigidbody2D>();
            playerCollider = player.GetComponent<Collider2D>();
            
            
            Physics2D.IgnoreLayerCollision(player.layer, LayerMask.NameToLayer("PhasableObjects"), true);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (playerInGeyser && other.CompareTag("Player"))
        {
            
            playerRb.AddForce(Vector2.up * upwardForce, ForceMode2D.Force);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInGeyser = false;
            
            Physics2D.IgnoreLayerCollision(player.layer, LayerMask.NameToLayer("PhasableObjects"), false);
        }
    }
} 