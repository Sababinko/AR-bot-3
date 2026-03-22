using UnityEngine;

public class BotHealth : MonoBehaviour
{
    private int headHealth = 1;
    private int chestHealth = 2;
    private int limbHealth = 3;
    
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void TakeDamage(string bodyPart)
    {
        bool isDead = false;

        if (bodyPart == "Head")
        {
            headHealth--;
            if (headHealth <= 0) isDead = true;
        }
        else if (bodyPart == "Chest")
        {
            chestHealth--;
            if (chestHealth <= 0) isDead = true;
        }
        else if (bodyPart == "Limbs")
        {
            limbHealth--;
            if (limbHealth <= 0) isDead = true;
        }

        if (isDead)
        {
            gameManager.BotKilled();
            Destroy(gameObject);
        }
    }
}