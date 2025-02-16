using UnityEngine;
using System.Collections;

public class spaceGun : MonoBehaviour
{
    public Transform firePoint;
    public PlayerMovement playerMovement;
    public GameObject laserBeamPrefab;
    public float laserBeamSpeed = 10f;
    public float laserBeamRange = 100f;
    public float rotationLimit = 15f;
    private float lastFireTime = 0f;
    private float fireCooldown = 0.5f;
    public Transform gunPivot;

    public SpriteRenderer spriteRenderer;


    private void Update()
    { 
    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Vector2 direction = mousePosition - gunPivot.position;
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

    if (angle > 90 || angle < -55)
    {
        spriteRenderer.flipY = true;
    }
    else
    {
        spriteRenderer.flipY = false;
    }

    
    float limitedAngle = Mathf.Clamp(angle, -rotationLimit, rotationLimit); 

    gunPivot.rotation = Quaternion.AngleAxis(limitedAngle, Vector3.forward);

    if (Input.GetButtonDown("Fire1"))
    {
        if (Time.time - lastFireTime >= fireCooldown){
            FireLaser();
            lastFireTime = Time.time;
        }
    }

    }

public IEnumerator startFiring(GameObject lazer)
{
    yield return new WaitForSeconds(2f);
    Destroy(lazer);

}
    private void FireLaser()
    {
    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Vector2 fireDirection = mousePosition - firePoint.position;

    
    GameObject laserBeam = Instantiate(laserBeamPrefab, firePoint.position, firePoint.rotation);
    Rigidbody2D rb = laserBeam.GetComponent<Rigidbody2D>();

    rb.linearVelocity = fireDirection.normalized * laserBeamSpeed;

    StartCoroutine(startFiring(laserBeam));
    }
}
