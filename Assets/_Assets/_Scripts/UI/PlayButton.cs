using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public AudioSource buttonSound;
    public void LoadScene(string AquariumTemplate)
    {
        buttonSound.Play(0);
        StartCoroutine(FadeLoadingScreen(0.5f, AquariumTemplate));
    }

    // Defer the loading of the scene by some seconds to allow the ui sound to be played
    // This can also be used to have a fade in animation transition
    // more info: https://gamedevbeginner.com/how-to-load-a-new-scene-in-unity-with-a-loading-screen/#fade_loading_screen
    IEnumerator FadeLoadingScreen(float duration, string newScene)
    {
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene(newScene);
    }

    public void PlaySound()
    {
        buttonSound.Play(0);
    }

    public void QuitGame()
    {
        buttonSound.Play();
        Application.Quit();
    }
}
