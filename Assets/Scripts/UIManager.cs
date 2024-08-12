using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject menuPanel; // Reference to the Menu panel, toggled by pressing Escape
    public GameObject gameOverPanel; // Reference to the Game Over panel, shown when the player dies
    public GameObject continuePanel; // Reference to the Continue panel, shown after the Game Over panel
    public TextMeshProUGUI continueText; // Reference to the Continue text, displaying the countdown
    public TextMeshProUGUI creditsText; // Reference to the Credits text, displaying the number of credits

    private PlayerController playerController; // Reference to the PlayerController script
    public int credits; // Number of credits the player has to continue after dying
    private bool isGameOver = false; // Flag to check if the game is in a Game Over state
    private float continueTimer = 3f; // Timer for the countdown before showing the Continue screen

    void Start()
    {
        // Called when the script instance is first loaded
        menuPanel.SetActive(false); // Hide the Menu panel at the start
        gameOverPanel.SetActive(false); // Hide the Game Over panel at the start
        continuePanel.SetActive(false); // Hide the Continue panel at the start

        // Find and reference the PlayerController script in the scene
        playerController = FindObjectOfType<PlayerController>();
        UpdateCreditsText(credits); // Initialize credits text
    }

    void Update()
    {
        // Handle the menu toggling with the Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }

        // Handle the countdown for showing the Continue screen
        if (isGameOver)
        {
            continueTimer -= Time.unscaledDeltaTime; // Decrease the timer (unscaled for real-time countdown)
            continueText.text = "Continue? " + Mathf.Ceil(continueTimer).ToString(); // Update the Continue text
            if (continueTimer <= 0)
            {
                ShowContinueScreen(); // Show the Continue screen when the timer reaches 0
            }
        }
    }

    public void ShowGameOverPanel()
    {
        // Show the Game Over panel and pause the game
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // Freeze the game
        isGameOver = true; // Set the game over flag
        continueTimer = 5f; // Reset the continue timer to 5 seconds
    }

    public void ShowContinueScreen()
    {
        // Transition from Game Over screen to Continue screen
        gameOverPanel.SetActive(false); // Hide the Game Over panel
        continuePanel.SetActive(true); // Show the Continue panel
    }

    public void OnContinueButton()
    {
        Debug.Log("Continue button clicked.");
        if (credits > 0)
        {
            credits--; // Deduct one credit
            UpdateCreditsText(credits); // Update the UI display for credits
            playerController.currentHealth = playerController.maxHealth; // Restore player's health to max
            continuePanel.SetActive(false); // Hide the Continue panel
            gameOverPanel.SetActive(false); // Ensure the Game Over panel is also hidden
            Time.timeScale = 1f; // Resume the game
            isGameOver = false; // Reset the game over flag
            Debug.Log("Game resumed with " + credits + " credits left.");
        }
        else
        {
            Debug.Log("No credits left.");
            // Handle what happens when there are no credits left
        }
    }


    public void OnQuitButton()
    {
        // Handle quitting the game when the Quit button is pressed
        Application.Quit(); // Quit the application
    }

    public void UpdateCreditsText(int credits)
    {
        if (creditsText != null)
        {
            creditsText.text = "Credits: " + credits;
        }
        else
        {
            Debug.LogError("creditsText is not assigned in the UIManager script!");
        }
    }

    public void ToggleMenu()
    {
        // Toggle the Menu panel and pause/resume the game
        bool isActive = menuPanel.activeSelf; // Check if the Menu panel is currently active
        menuPanel.SetActive(!isActive); // Toggle the Menu panel's active state
        Time.timeScale = isActive ? 1f : 0f; // Pause or resume the game based on the menu state
    }


}
