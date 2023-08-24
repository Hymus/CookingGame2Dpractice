using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControl : MonoBehaviour
{
    [SerializeField] GameObject loadSceneCurtainOBj;

    public void StartGameButton()
    {
        Invoke(nameof(LoadScene), 2);
        loadSceneCurtainOBj.SetActive(true);
        loadSceneCurtainOBj.GetComponent<Animator>().SetBool("CloseScene", true);
    }

    void LoadScene()
    {
        SceneManager.LoadScene("Map1");
    }

    public void ExitGameButton()
    {
        Application.Quit();
    }
}
