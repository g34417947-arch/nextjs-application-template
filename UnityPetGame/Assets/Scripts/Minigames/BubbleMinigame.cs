using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Simple bubble clicking minigame
/// Players click bubbles to earn coins
/// </summary>
public class BubbleMinigame : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private float gameDuration = 30f;
    [SerializeField] private int coinsPerBubble = 5;
    [SerializeField] private float bubbleSpawnRate = 1f;
    [SerializeField] private GameObject bubblePrefab;
    
    [Header("UI Elements")]
    [SerializeField] private Text timerText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Button backButton;
    
    private float timeRemaining;
    private int score;
    private bool gameActive;
    
    private PetStats petStats;
    
    private void Start()
    {
        petStats = FindObjectOfType<PetStats>();
        if (petStats == null)
        {
            Debug.LogError("PetStats not found in scene!");
            return;
        }
        
        timeRemaining = gameDuration;
        score = 0;
        gameActive = true;
        
        if (backButton != null)
            backButton.onClick.AddListener(() => EndGame());
        
        StartCoroutine(SpawnBubbles());
        UpdateUI();
    }
    
    private void Update()
    {
        if (gameActive)
        {
            timeRemaining -= Time.deltaTime;
            
            if (timeRemaining <= 0)
            {
                EndGame();
            }
            
            UpdateUI();
        }
    }
    
    /// <summary>
    /// Spawns bubbles at regular intervals
    /// </summary>
    private IEnumerator SpawnBubbles()
    {
        while (gameActive)
        {
            SpawnBubble();
            yield return new WaitForSeconds(bubbleSpawnRate);
        }
    }
    
    /// <summary>
    /// Spawns a single bubble at random position
    /// </summary>
    private void SpawnBubble()
    {
        if (bubblePrefab == null) return;
        
        Vector3 spawnPosition = new Vector3(
            Random.Range(-8f, 8f),
            Random.Range(-4f, 4f),
            0f
        );
        
        GameObject bubble = Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);
        Bubble bubbleScript = bubble.GetComponent<Bubble>();
        
        if (bubbleScript != null)
        {
            bubbleScript.Initialize(this);
        }
    }
    
    /// <summary>
    /// Called when a bubble is clicked
    /// </summary>
    public void OnBubbleClicked()
    {
        score++;
        if (petStats != null)
        {
            petStats.AddCoins(coinsPerBubble);
        }
    }
    
    /// <summary>
    /// Updates UI elements
    /// </summary>
    private void UpdateUI()
    {
        if (timerText != null)
            timerText.text = $"Time: {Mathf.CeilToInt(timeRemaining)}s";
        
        if (scoreText != null)
            scoreText.text = $"Score: {score}";
    }
    
    /// <summary>
    /// Ends the minigame and returns to main scene
    /// </summary>
    private void EndGame()
    {
        gameActive = false;
        StopAllCoroutines();
        
        // Destroy all remaining bubbles
        Bubble[] bubbles = FindObjectsOfType<Bubble>();
        foreach (Bubble bubble in bubbles)
        {
            Destroy(bubble.gameObject);
        }
        
        SceneLoader.Instance.LoadMainScene();
    }
}
