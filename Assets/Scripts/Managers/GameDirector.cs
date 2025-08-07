using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public Player player;
    public LevelManager levelManager;
    public EnemyManager enemyManager;
    public List<Enemy> enemies;

    void Start()
    {
        levelManager.RestartLevel();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            levelManager.RestartLevel();// Resume game if paused
        }
    }

    public void LevelCompleted(string mesaj = "Kazandın!")
    {
        Cursor.lockState = CursorLockMode.None; // Mouse'u serbest bırak
        UIManager.Instance.ShowLevelCompletedUI(mesaj);
        Debug.Log("Level Completed!");
    }
}
