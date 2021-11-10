using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // The text objects.
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text gameOverText;
    [SerializeField]
    private Text restartText;

    // Lives objects.
    [SerializeField]
    private Sprite[] liveSprites;
    [SerializeField]
    private Image livesImg;

    // The Game Manager object.
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: 0";

        // Initiate the gameManager object.
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("gameManager variable is null.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int lives)
    {
        livesImg.sprite = liveSprites[lives];
        if (lives <= 0)
        {
            gameManager.GameOver();
            gameOverText.gameObject.SetActive(true);
            StartCoroutine(GameOverEffect());
            restartText.gameObject.SetActive(true);
        }
    }

    IEnumerator GameOverEffect()
    {
        string text = gameOverText.text;
        while (true)
        {
            gameOverText.text = text;
            yield return new WaitForSeconds(0.5f);
            gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
