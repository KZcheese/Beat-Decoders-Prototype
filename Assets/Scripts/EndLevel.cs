using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public string nextLevel;
    private bool _visible;

    private void Start()
    {
        gameObject.SetActive(_visible);
    }

    public void ShowScreen(bool visible)
    {
        _visible = visible;
        gameObject.SetActive(_visible);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }
}