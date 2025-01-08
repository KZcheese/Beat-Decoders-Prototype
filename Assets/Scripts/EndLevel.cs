using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public string nextLevel;
    public void ShowScreen(bool visible)
    {
        gameObject.SetActive(visible);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }
}