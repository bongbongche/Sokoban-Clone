using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public int goals;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        goals = 0;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(goals == transform.childCount)
        {
            gameManager.ClearGame();
        }
    }
}
