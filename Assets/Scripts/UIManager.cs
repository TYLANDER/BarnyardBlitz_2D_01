using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject menuPanel; // Reference to the MenuPanel
    public GameObject gameOverPanel; // Reference to the GameOverPanel
    public GameObject continuePanel; // Reference to the ContinuePanel
    public TextMeshProUGUI continueText; // Reference to the ContinueText

    private bool isGameOver = false;
    private float continueTimer = 5f;

    void Start()
    {
        // Hide all panels at the start
        menuPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        continuePanel.SetActive(false);
    }

    void Update()
    {
        // Toggle MenuPanel with Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }

        // Handle continue countdown if game over
        if (isGameOver)
        {
            continueTimer -= Time.unscaledDeltaTime;
            continueText.text = "Continue? " + Mathf.Ceil(continueTimer).ToString();
            if (continueTimer <= 0)
            {
                ShowContinuePanel();
            }
        }
    }

    public void ToggleMenu()
    {
        bool isActive = menuPanel.activeSelf;
        menuPanel.SetActive(!isActive);
        Time.timeScale = isActive ? 1f : 0f; // Pause or resume the game
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // Freeze the game
        isGameOver = true;
        continueTimer = 5f; // Reset the continue timer
    }

    private void ShowContinuePanel()
    {
        gameOverPanel.SetActive(false);
        continuePanel.SetActive(true);
        Time.timeScale = 0f; // Keep the game paused
    }

    public void OnContinueButton()
    {
        continuePanel.SetActive(false);
        Time.timeScale = 1f; // Resume the game
        isGameOver = false;
        // Additional logic to reset the player state
    }

    public void OnQuitButton()
    {
        Application.Quit(); // Quit the game
    }
}
