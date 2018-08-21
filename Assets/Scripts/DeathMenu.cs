using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] private Level level;

    private void Start()
    {
        level = GameObject.FindGameObjectWithTag("Level").GetComponent<Level>();
    }

    public void RestartLevel()
    {
        level.RestartLevel();
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void OpenThisMenu()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
    }
}