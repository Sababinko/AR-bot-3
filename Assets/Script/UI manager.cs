using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    [Header("Canvases")]
    public Canvas startMenuCanvas;
    public Canvas gameUICanvas;
    public Canvas endMenuCanvas;
    
    [Header("UI Elements")]
    public TextMeshProUGUI finalScoreText;
    
    [Header("References")]
    public GameManager gameManager;
    
    [Header("First Selected Buttons")]
    public Button startMenuFirstButton;
    public Button endMenuFirstButton;

    void Start()
    {
        Debug.Log("UIManager Start");
        ShowStartMenu();
    }

    public void ShowStartMenu()
    {
        Debug.Log("Showing Start Menu");
        
        if (startMenuCanvas != null)
            startMenuCanvas.gameObject.SetActive(true);
        else
            Debug.LogError("startMenuCanvas is NULL!");
            
        if (gameUICanvas != null)
            gameUICanvas.gameObject.SetActive(false);
        else
            Debug.LogError("gameUICanvas is NULL!");
            
        if (endMenuCanvas != null)
            endMenuCanvas.gameObject.SetActive(false);
        else
            Debug.LogError("endMenuCanvas is NULL!");
        
        // Auto-select first button
        SelectButton(startMenuFirstButton);
    }

    public void ShowGameUI()
    {
        Debug.Log("Showing Game UI");
        
        if (startMenuCanvas != null)
            startMenuCanvas.gameObject.SetActive(false);
            
        if (gameUICanvas != null)
            gameUICanvas.gameObject.SetActive(true);
            
        if (endMenuCanvas != null)
            endMenuCanvas.gameObject.SetActive(false);
        
        // Clear selection during gameplay
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void ShowEndMenu(int finalScore)
    {
        Debug.Log("=== ShowEndMenu called with score: " + finalScore + " ===");
        
        if (startMenuCanvas == null)
        {
            Debug.LogError("startMenuCanvas is NULL!");
        }
        else
        {
            startMenuCanvas.gameObject.SetActive(false);
            Debug.Log("Start menu hidden");
        }
        
        if (gameUICanvas == null)
        {
            Debug.LogError("gameUICanvas is NULL!");
        }
        else
        {
            gameUICanvas.gameObject.SetActive(false);
            Debug.Log("Game UI hidden");
        }
        
        if (endMenuCanvas == null)
        {
            Debug.LogError("endMenuCanvas is NULL!");
        }
        else
        {
            endMenuCanvas.gameObject.SetActive(true);
            Debug.Log("End menu shown - active: " + endMenuCanvas.gameObject.activeSelf);
        }
        
        if (finalScoreText == null)
        {
            Debug.LogError("finalScoreText is NULL!");
        }
        else
        {
            finalScoreText.text = "Final Score: " + finalScore;
            Debug.Log("Final score text updated to: " + finalScoreText.text);
        }
        
        // Auto-select first button
        SelectButton(endMenuFirstButton);
    }
    
    void SelectButton(Button button)
    {
        if (button != null)
        {
            EventSystem.current.SetSelectedGameObject(button.gameObject);
            Debug.Log("Selected button: " + button.gameObject.name);
        }
    }

    // Button Functions
    public void OnPlayButtonClicked()
    {
        Debug.Log("Play button clicked");
        ShowGameUI();
        
        if (gameManager == null)
        {
            Debug.LogError("gameManager is NULL!");
        }
        else
        {
            gameManager.StartGame();
        }
    }

    public void OnPlayAgainButtonClicked()
    {
        Debug.Log("Play Again button clicked");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnQuitButtonClicked()
    {
        Debug.Log("Quit button clicked");
        
        if (gameManager == null)
        {
            Debug.LogError("gameManager is NULL!");
        }
        else
        {
            gameManager.ResetGame();
        }
        
        ShowStartMenu();
    }
}