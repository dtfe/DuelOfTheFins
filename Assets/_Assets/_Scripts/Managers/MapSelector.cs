using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MapSelector : MonoBehaviour
{
    public int curMapIndex = 1;
    public TMP_Text mapNameText;
    public Texture2D[] previewImages;
    public RawImage previewObject;
    public AudioSource buttonSound;

    public void Previous()
    {
        curMapIndex--;
        if (curMapIndex < 2)
        {
            curMapIndex = SceneManager.sceneCountInBuildSettings;
        }
    }

    public void Next()
    {
        curMapIndex++;
        if(curMapIndex > SceneManager.sceneCountInBuildSettings)
        {
            curMapIndex = 2;
        }
    }

    public void ConfirmSelection()
    {
        MusicManager.instance.StopMusic();
        SoundManager.PlaySound("ui_selection");
        StartCoroutine(DeferLoadingScreen(0.5f));
        //SceneManager.LoadScene(curMapIndex);
    }
    IEnumerator DeferLoadingScreen(float duration)
    {
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene(curMapIndex);
    }
    private void Update()
    {
        switch (curMapIndex-1)
        {
            case 1:
                mapNameText.text = "Aquarium 1";
                previewObject.texture = previewImages[0];
                break;
            case 2:
                mapNameText.text = "Aquarium 2";
                previewObject.texture = previewImages[1];
                break;
            case 3:
                mapNameText.text = "Ocean 1";
                previewObject.texture = previewImages[2];
                break;
            case 4:
                mapNameText.text = "Ocean 2";
                previewObject.texture = previewImages[3];
                break;
            case 5:
                mapNameText.text = "River 1";
                previewObject.texture = previewImages[4];
                break;
            case 6:
                mapNameText.text = "River 2";
                previewObject.texture = previewImages[5];
                break;
            case 7:
                mapNameText.text = "River 3";
                previewObject.texture = previewImages[6];
                break;
            case 8:
                mapNameText.text = "Space 1";
                previewObject.texture = previewImages[7];
                break;
        }
    }
}
