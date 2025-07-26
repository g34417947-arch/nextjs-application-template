using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages all UI elements including status bars, buttons, and HUD
/// Updates UI based on pet's current state
/// </summary>
public class UIManager : MonoBehaviour
{
    [Header("Status Bars")]
    [SerializeField] private Slider hungerSlider;
    [SerializeField] private Slider happinessSlider;
    [SerializeField] private Slider hygieneSlider;
    [SerializeField] private Slider energySlider;
    [SerializeField] private Slider healthSlider;
    
    [Header("Text Elements")]
    [SerializeField] private Text petNameText;
    [SerializeField] private Text petAgeText;
    [SerializeField] private Text coinsText;
    [SerializeField] private Text levelText;
    
    [Header("Action Buttons")]
    [SerializeField] private Button feedButton;
    [SerializeField] private Button sleepButton;
    [SerializeField] private Button playButton;
    [SerializeField] private Button cleanButton;
    [SerializeField] private Button healButton;
    
    [Header("Minigame Button")]
    [SerializeField] private Button minigameButton;
    
    private PetStats petStats;
    private PetController petController;
    
    private void Start()
    {
        petStats = FindObjectOfType<PetStats>();
        petController = FindObjectOfType<PetController>();
        
        if (petStats == null)
        {
            Debug.LogError("PetStats not found in scene!");
            return;
        }
        
        SetupUI();
        UpdateAllUI();
    }
    
    /// <summary>
    /// Sets up UI elements and event listeners
    /// </summary>
    private void SetupUI()
    {
        // Subscribe to stat changes
        petStats.OnHungerChanged += UpdateHungerUI;
        petStats.OnHappinessChanged += UpdateHappinessUI;
        petStats.OnHygieneChanged += UpdateHygieneUI;
        petStats.OnEnergyChanged += UpdateEnergyUI;
        petStats.OnHealthChanged += UpdateHealthUI;
        petStats.OnCoinsChanged += UpdateCoinsUI;
        petStats.OnLevelChanged += UpdateLevelUI;
        petStats.OnAgeChanged += UpdateAgeUI;
        
        // Setup button listeners
        if (feedButton != null)
            feedButton.onClick.AddListener(() => petStats.Feed());
        
        if (sleepButton != null)
            sleepButton.onClick.AddListener(() => petStats.Sleep());
        
        if (playButton != null)
            playButton.onClick.AddListener(() => petStats.Play());
        
        if (cleanButton != null)
            cleanButton.onClick.AddListener(() => petStats.Clean());
        
        if (healButton != null)
            healButton.onClick.AddListener(() => petStats.Heal());
        
        if (minigameButton != null)
            minigameButton.onClick.AddListener(() => LoadMinigame());
    }
    
    /// <summary>
    /// Updates all UI elements
    /// </summary>
    private void UpdateAllUI()
    {
        UpdateHungerUI(petStats.Hunger);
        UpdateHappinessUI(petStats.Happiness);
        UpdateHygieneUI(petStats.Hygiene);
        UpdateEnergyUI(petStats.Energy);
        UpdateHealthUI(petStats.Health);
        UpdateCoinsUI(petStats.Coins);
        UpdateLevelUI(petStats.Level);
        UpdateAgeUI(petStats.PetAge);
        UpdateNameUI();
    }
    
    /// <summary>
    /// Updates hunger slider
    /// </summary>
    private void UpdateHungerUI(float value)
    {
        if (hungerSlider != null)
            hungerSlider.value = value / 100f;
    }
    
    /// <summary>
    /// Updates happiness slider
    /// </summary>
    private void UpdateHappinessUI(float value)
    {
        if (happinessSlider != null)
            happinessSlider.value = value / 100f;
    }
    
    /// <summary>
    /// Updates hygiene slider
    /// </summary>
    private void UpdateHygieneUI(float value)
    {
        if (hygieneSlider != null)
            hygieneSlider.value = value / 100f;
    }
    
    /// <summary>
    /// Updates energy slider
    /// </summary>
    private void UpdateEnergyUI(float value)
    {
        if (energySlider != null)
            energySlider.value = value / 100f;
    }
    
    /// <summary>
    /// Updates health slider
    /// </summary>
    private void UpdateHealthUI(float value)
    {
        if (healthSlider != null)
            healthSlider.value = value / 100f;
    }
    
    /// <summary>
    /// Updates coins text
    /// </summary>
    private void UpdateCoinsUI(int value)
    {
        if (coinsText != null)
            coinsText.text = $"Coins: {value}";
    }
    
    /// <summary>
    /// Updates level text
    /// </summary>
    private void UpdateLevelUI(int value)
    {
        if (levelText != null)
            levelText.text = $"Level: {value}";
    }
    
    /// <summary>
    /// Updates age text
    /// </summary>
    private void UpdateAgeUI(int value)
    {
        if (petAgeText != null)
            petAgeText.text = $"Age: {value} days";
    }
    
    /// <summary>
    /// Updates name text
    /// </summary>
    private void UpdateNameUI()
    {
        if (petNameText != null)
            petNameText.text = petStats.PetName;
    }
    
    /// <summary>
    /// Loads the minigame scene
    /// </summary>
    private void LoadMinigame()
    {
        SceneLoader.Instance.LoadMinigame();
    }
}
