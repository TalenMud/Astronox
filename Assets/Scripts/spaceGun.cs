using UnityEngine;
using System.Collections;

public class spaceGun : MonoBehaviour
{
    public HotbarManager hotbarManager;
    public PlayerMovement playerMovement;
    public GameObject laserBeamPrefab;
    public float laserBeamSpeed = 10f;
    public float laserBeamRange = 100f;
    public float rotationLimit = 15f;
    public Transform gunPivot;

    public SpriteRenderer spriteRenderer;


    private void Update()
    { 
    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Vector2 direction = mousePosition - gunPivot.position;
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    
    float limitedAngle = Mathf.Clamp(angle, -rotationLimit, rotationLimit); 

    gunPivot.rotation = Quaternion.AngleAxis(limitedAngle, Vector3.forward);

    if (Input.GetButtonDown("Fire1"))
    {
        FireLaser();
    }

    }

public IEnumerator startFiring(GameObject lazer)
{
    yield return new WaitForSeconds(2f);
    Destroy(lazer);

}
    private void FireLaser()
    {
        
    GameObject laserBeam = Instantiate(laserBeamPrefab, transform.position, transform.rotation);
    Rigidbody2D rb = laserBeam.GetComponent<Rigidbody2D>();

    if (!playerMovement.isFacingRight)
    {
        rb.linearVelocity = -transform.right * laserBeamSpeed; // Fire left
    }
    else
    {
        rb.linearVelocity = transform.right * laserBeamSpeed; // Fire right
    }

    StartCoroutine(startFiring(laserBeam));

    }
}
