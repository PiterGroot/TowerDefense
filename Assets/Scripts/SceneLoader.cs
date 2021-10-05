using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName) {
        //SceneManager.LoadScene(sceneName);
        SceneManager.LoadSceneAsync(sceneName);
    }
    public void QuitGame() {
        print("User quit game!");
        Application.Quit();
    }
}
