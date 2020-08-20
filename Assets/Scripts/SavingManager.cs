using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingManager : MonoBehaviour
{
    float nextTime;
    public float timeGap = 0.5f;
    public GameObject gameManager;
    void Start()
    {
        nextTime = timeGap;
        //LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextTime) {
            SaveGame();
            nextTime = Time.time + timeGap;

        }
    }

    public void SaveGame() {
        gameManager.GetComponent<GameManager>().SaveBalls();
    }

    public void LoadGame() {
        gameManager.GetComponent<GameManager>().LoadBalls();
    }
}
