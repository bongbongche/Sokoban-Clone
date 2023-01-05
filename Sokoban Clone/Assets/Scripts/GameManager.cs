using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI movesText;
    public TextMeshProUGUI pushesText;
    public TextMeshProUGUI clearText;
    public Button resetButton;
    public bool isGameActive;

    private int moves;
    private int pushes;
    // Start is called before the first frame update
    void Start()
    {
        moves = 0;
        pushes = 0;
        isGameActive = true;
}

// Update is called once per frame
void Update()
    {
        
    }

    public void UpdateMoves()
    {
        moves += 1;
        movesText.text = "Moves: " + moves;
    }    
    
    public void UpdatePushes()
    {
        pushes += 1;
        pushesText.text = "Pushes: " + pushes;
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        clearText.gameObject.SetActive(false);
    }

    public void ClearGame()
    {
        clearText.gameObject.SetActive(true);
        GameObject.Find("Player").SetActive(false);
    }
}
