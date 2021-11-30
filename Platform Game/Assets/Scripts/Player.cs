using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private float speed = 5.0f;
    private float gravity = 1.0f;
    private float jumpHeight = 15.0f;
    private float dyTemp;
    private bool canDoubleJump = true;

    private int coins = 0;
    private UIManager uiManager;

    private int lives = 3;

    [SerializeField]
    private GameObject startPosition;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        uiManager.LivesDisplay(lives);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, 0, 0);
        direction *= speed;

        if (!controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space) && canDoubleJump)
            {
                dyTemp = jumpHeight;
                canDoubleJump = false;
            }
            else
            {
                dyTemp -= gravity;
            }
        }
        else
        {
            // When the caracter is grounded (lands) then they can double jump again.
            canDoubleJump = true;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                dyTemp = jumpHeight;
            }
        }

        direction.y = dyTemp;

        controller.Move(direction * Time.deltaTime);

        if (transform.position.y < -10.0f)
        {
            lives--;
            uiManager.LivesDisplay(lives);
            transform.position = startPosition.transform.position;
            if (lives < 1)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    public void AddCoin()
    {
        coins++;
        
        if (uiManager != null)
        {
            uiManager.CoinsDisplay(coins);
        }
    }
}
