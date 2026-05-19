using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private int playerLives = 0;
    [SerializeField] private int playerMoney = 0;
    [SerializeField] private AudioSource CheatsSfx;
    [SerializeField] private AudioSource GameOverSfx;

    // used for updating lives being displayed on player UI
    private GameObject playerLivesTMP;
    private TMP_Text textLives;

    // used for updating money being displayed on player UI
    private GameObject playerMoneyTMP;
    private TMP_Text textMoney;

    private bool gameover = false;

    // cheat code "AARONYAGER"
    private readonly List<KeyCode> targetSequence = new List<KeyCode>
    {
        KeyCode.A,
        KeyCode.A,
        KeyCode.R,
        KeyCode.O,
        KeyCode.N,
        KeyCode.Y,
        KeyCode.A,
        KeyCode.G,
        KeyCode.E,
        KeyCode.R
    };

    private List<KeyCode> currentSequence = new List<KeyCode>();


    // Start is called before the first frame update
    void Start()
    {
        // retreive TMP text
        playerLivesTMP = GameObject.FindGameObjectWithTag("PlayerLives");
        playerMoneyTMP = GameObject.FindGameObjectWithTag("PlayerMoney");

        // set TMP text to starting player status from PlayerStatus settings
        textLives = playerLivesTMP.GetComponent<TMP_Text>();
        textLives.text = playerLives.ToString();
        textMoney = playerMoneyTMP.GetComponent<TMP_Text>();
        textMoney.text = playerMoney.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        foreach (KeyCode key in targetSequence)
        {
            if (Input.GetKeyDown(key))
            {
                currentSequence.Add(key);
                CheatsCheckSequence();
                return; // Prevent checking multiple keys in one frame
            }
        }

        // If any other key is pressed, reset the sequence
        if (Input.anyKeyDown && !targetSequence.Contains(CheatsGetCurrentKeyDown()))
        {
            CheatsResetSequence();
        }
    }

    public int getPlayerLives()
    {
        return playerLives;
    }

    public int getPlayerMoney()
    {
        return playerMoney;
    }

    public void takeDamage(int value)
    {
        if (playerLives != 0)
        {
            playerLives -= value;
            textLives.text = playerLives.ToString();
        }
        

        if (playerLives == 0 && gameover == false)
        {
            gameover = true;
            // gameover music + change scene
            StartCoroutine(GameOver());
        }
    }

    public void enemyReward(int value)
    {
        playerMoney += value;

        // check if hit over max,
        if (playerMoney > 99)
        {
            playerMoney = 99;
        }

        textMoney.text = playerMoney.ToString();
    }

    public void towerBought(int value)
    {
        playerMoney -= value;

        // set new PlayerMoney value
        textMoney.SetText(playerMoney.ToString());
    }

    public void towerSold(int value)
    {
        playerMoney += value;

        // check if hit over max,
        if (playerMoney > 99)
        {
            playerMoney = 99;
        }

        // set new PlayerMoney value
        textMoney.SetText(playerMoney.ToString());
    }

    private void CheatsCheckSequence()
    {
        // Compare the current sequence to the target sequence
        for (int i = 0; i < currentSequence.Count; i++)
        {
            if (currentSequence[i] != targetSequence[i])
            {
                CheatsResetSequence(); // Reset if the sequence is incorrect
                return;
            }
        }

        // If the sequence is complete and correct
        if (currentSequence.Count == targetSequence.Count)
        {
            Debug.Log("Cheat Code Activated!");

            // set lives and money to 99
            playerLives = 99;
            playerMoney = 99;

            // set to display lives and money 
            textMoney.SetText(playerMoney.ToString());
            textLives.SetText(playerLives.ToString());
            // audio feedback "cheatcode activated"
            CheatsSfx.Play();
        }
    }

    private void CheatsResetSequence()
    {
        currentSequence.Clear();
    }

    private KeyCode CheatsGetCurrentKeyDown()
    {
        // Returns the current key that is down
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                return key;
            }
        }
        return KeyCode.None;
    }

    IEnumerator GameOver()
    {
        GameOverSfx.Play();
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("Game Over Scene");
        //game over scene is loaded
    }

}
