using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // The speed variable.
    public float horizontalSpeed = 2.5f;
    public float verticalSpeed = 1.5f;
    
    // The laser and triple shot game objects.
    [SerializeField]
    private GameObject laserPrefab = null;
    [SerializeField]
    private GameObject tripleShotPrefab = null;

    // The Spawn Manager object.
    private SpawnManager spawnManager = null;

    // The lives variable.
    private int lives = 3;
    
    // The fire range variables.
    private float fireRate = 0.5f;
    private float lastFired = -0.5f;

    // Powerup upgrades.
    private bool tripleShotActive = false;
    private bool shieldActive = false;

    // The Shield object.
    [SerializeField]
    private GameObject shieldObject = null;

    private int score;

    // The UIManager object.
    private UIManager uiManager;

    // The Left and Right Engine damage objects.
    [SerializeField]
    private GameObject leftEngine;
    [SerializeField]
    private GameObject rightEngine;

    // The Audio Source object.
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        //transform.rotation = new Quaternion(7, 0, -3, 0);

        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        if (spawnManager == null)
        {
            Debug.LogError("spawnManager variable is null.");
        }

        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (uiManager == null)
        {
            Debug.LogError("uiManager variable is null.");
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("audioSource variable is null.");
        }
    }

    // Update is called once per frame
    void Update()
    {
    	playerMovement();
    	laserFiring();
    }
    
    // Player movement.
    void playerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
    	float verticalInput = Input.GetAxis("Vertical");
    	transform.Translate(Vector3.right * horizontalSpeed * horizontalInput * Time.deltaTime);
    	transform.Translate(Vector3.up * verticalSpeed * verticalInput * Time.deltaTime);
    	
    	// Restrict the translation to a specific range.
    	float xValue = Mathf.Clamp(transform.position.x, -9.1f, 9.1f);
    	transform.position = new Vector3(xValue, transform.position.y, transform.position.z);
    	float yValue = Mathf.Clamp(transform.position.y, -4.2f, 0.2f);
    	transform.position = new Vector3(transform.position.x, yValue, transform.position.z);
    }
    
    // Laser firing.
    void laserFiring()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > lastFired + fireRate)
    	{
            if (tripleShotActive)
            {
                Instantiate(tripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
            }
            audioSource.Play();
    	    lastFired = Time.time;
    	}
    }

    public void Damage()
    {
        if (shieldActive)
        {
            shieldActive = false;
            shieldObject.SetActive(false);
            return;
        }

        lives--;
        uiManager.UpdateLives(lives);

        if (lives == 2)
        {
            leftEngine.gameObject.SetActive(true);
        }
        if (lives == 1)
        {
            rightEngine.SetActive(true);
        }
        if (lives <= 0)
        {
            Destroy(this.gameObject);
            spawnManager.PlayerDied();
        }
    }

    public void ActivateTripleShot()
    {
        tripleShotActive = true;
        StartCoroutine(DeactivateTripleShot());
    }

    IEnumerator DeactivateTripleShot()
    {
        yield return new WaitForSeconds(5.0f);
        tripleShotActive = false;
    }

    public void ActivateSpeed(float newHorizontalSpeed, float newVerticalSpeed)
    {
        horizontalSpeed = newHorizontalSpeed;
        verticalSpeed = newVerticalSpeed;
        StartCoroutine(DeactivateSpeed());
    }

    IEnumerator DeactivateSpeed()
    {
        yield return new WaitForSeconds(5.0f);
        horizontalSpeed = 1.5f;
        verticalSpeed = 2.5f;
    }

    public void ActivateShield()
    {
        shieldActive = true;
        shieldObject.SetActive(true);
        StartCoroutine(DeactivateShield());
    }

    IEnumerator DeactivateShield()
    {
        yield return new WaitForSeconds(5.0f);
        shieldActive = false;
        shieldObject.SetActive(false);
    }

    public void AddScore()
    {
        score++;
        uiManager.UpdateScore(score);
    }
}
