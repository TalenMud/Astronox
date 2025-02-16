using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class spawnRocket : MonoBehaviour
{
    public GameObject rocket;
    public Inventory inventory;
    public bool isInRange = false;
    public GameObject placementArrow1;
    public GameObject placementArrow2;
    public GameObject interactButton;
    private SpriteRenderer spriteRenderer;
    public GameObject rocketSpawner;
    public GameObject crafticon;
    public float rocketSpeed = 5f; 
    public GameObject tools;
    public GameObject player;
    private SpriteRenderer playerSprite;
    private Rigidbody2D playerRb;
    private Vector3 rocketCenter;
    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tools = GameObject.Find("GunPivot");
        player = GameObject.Find("Player");
        playerSprite = player.GetComponent<SpriteRenderer>();
        playerRb = player.GetComponent<Rigidbody2D>();
        rocketCenter = rocketSpawner.transform.position;
    }

    public void Update()
    {
        if (isInRange && inventory.CheckIfHasItem("CopperLaunchpad"))
        {
            placementArrow1.SetActive(true);
            placementArrow2.SetActive(true);
            interactButton.SetActive(true);
            crafticon.SetActive(true);
        }
        else
        {
            placementArrow1.SetActive(false);
            placementArrow2.SetActive(false);
            interactButton.SetActive(false);
            crafticon.SetActive(false);
        }

        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            SpawnRocket();
        }
    }

    public void SpawnRocket()
    {
        if (inventory.CheckIfHasItem("CopperLaunchpad"))
        {
            spriteRenderer.enabled = true;
            placementArrow1.SetActive(false);
            placementArrow2.SetActive(false);
            interactButton.SetActive(false);
            crafticon.SetActive(false);
            GameObject spawnedRocket = Instantiate(rocket, rocketSpawner.transform.position, Quaternion.identity);
            StartCoroutine(LaunchRocket(spawnedRocket));
        }
    }

    private IEnumerator LaunchRocket(GameObject rocketObj)
{
    HotbarManager.isRocketLeaving = true;
    
    // Hide player and tools
    player.GetComponent<SpriteRenderer>().enabled = false;
    tools.SetActive(false);
    
    // Disable player physics and movement
    player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    player.GetComponent<PlayerMovement>().enabled = false;
    
    float elapsedTime = 0f;
    
    while (elapsedTime < 3f)
    {
        // Move both rocket and player up
        rocketObj.transform.Translate(Vector2.up * rocketSpeed * Time.deltaTime);
        player.transform.position = rocketObj.transform.position; // Player follows rocket
        
        elapsedTime += Time.deltaTime;
        yield return null;
    }

    SceneManager.LoadScene("Planet_2");
    QuestManager.instance.InitializeQuests();
    HotbarManager.isRocketLeaving = false;

}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInRange = false;
        }
    }
} 