using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public string nextLevel;

    private void Start()
    {
        gameObject.SetActive(false); // start disabled
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }
}