using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
    private bool buttonPressed = false;
    public void PlayGame() {

        if (!buttonPressed) {
            StartCoroutine(SetActiveAndUnload());
            buttonPressed = true;
        }
        
    }

    public void QuitGame() {
        Application.Quit();
    }

    IEnumerator SetActiveAndUnload() {

        yield return SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Additive);
        
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("SampleScene"));
        SceneManager.UnloadSceneAsync("MenuScene");

    }
    
}
