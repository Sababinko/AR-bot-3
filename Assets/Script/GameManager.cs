using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("Bot Settings")]
    public GameObject botPrefab;
    public Transform spawnPoint;
    
    [Header("Game Settings")]
    public int totalBots = 30;
    public float spawnInterval = 3f;
    
    [Header("UI")]
    public TextMeshProUGUI scoreText;
    
    [Header("UI Manager")]
    public UIManager uiManager;
    
    private int currentScore = 0;
    private int botsSpawned = 0;
    private GameObject currentBot;
    private bool botKilledInTime = false;
    private bool gameActive = false;

    void Start()
    {
        // Game doesn't start automatically - wait for Play button
    }

    public void StartGame()
    {
        currentScore = 0;
        botsSpawned = 0;
        botKilledInTime = false;
        gameActive = true;
        
        UpdateScoreUI();
        StartCoroutine(SpawnBots());
    }

    public void ResetGame()
    {
        gameActive = false;
        StopAllCoroutines();
        
        if (currentBot != null)
        {
            Destroy(currentBot);
            currentBot = null;
        }
        
        currentScore = 0;
        botsSpawned = 0;
        botKilledInTime = false;
        
        UpdateScoreUI();
    }

    IEnumerator SpawnBots()
    {
        for (int i = 0; i < totalBots; i++)
        {
            if (!gameActive) yield break;
            
            SpawnBot();
            botsSpawned++;
            botKilledInTime = false;
            
            float timeWaited = 0f;
            while (timeWaited < spawnInterval && !botKilledInTime)
            {
                yield return new WaitForSeconds(0.1f);
                timeWaited += 0.1f;
            }
            
            if (botKilledInTime)
            {
                yield return new WaitForSeconds(1f);
            }
            
            if (currentBot != null)
            {
                Destroy(currentBot);
                currentBot = null;
            }
        }
        
        gameActive = false;
        yield return new WaitForSeconds(0.5f);
        
        if (uiManager != null)
        {
            uiManager.ShowEndMenu(currentScore);
        }
    }

    void SpawnBot()
    {
        if (currentBot != null)
        {
            Destroy(currentBot);
            currentBot = null;
        }

        float randomAngle = Random.Range(-75f, 75f);
        float distance = 2f;
        
        Vector3 direction = Quaternion.Euler(0, randomAngle, 0) * spawnPoint.forward;
        Vector3 spawnPosition = spawnPoint.position + (direction * distance);
        spawnPosition.y = spawnPoint.position.y;
        
        Vector3 lookDirection = spawnPoint.position - spawnPosition;
        lookDirection.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookDirection);

        currentBot = Instantiate(botPrefab, spawnPosition, rotation);
        currentBot.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        
        Rigidbody rb = currentBot.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
        
        if (currentBot.GetComponent<BotHealth>() == null)
        {
            currentBot.AddComponent<BotHealth>();
        }
    }

    public void BotKilled()
    {
        botKilledInTime = true;
        currentScore++;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore;
        }
    }
}