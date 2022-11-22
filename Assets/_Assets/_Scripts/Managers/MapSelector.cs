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
        if (curMapIndex < 3)
        {
            curMapIndex = SceneManager.sceneCountInBuildSettings-1;
        }
    }

    public void Next()
    {
        curMapIndex++;
        if(curMapIndex > SceneManager.sceneCountInBuildSettings-1)
        {
            curMapIndex = 3;
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
        switch (curMapIndex-2)
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
                mapNameText.text = "Ocean Volcano";
                previewObject.texture = previewImages[4];
                break;
            case 6:
                mapNameText.text = "Ocean Special";
                previewObject.texture = previewImages[5];
                break;
            case 7:
                mapNameText.text = "Waterfall 1";
                previewObject.texture = previewImages[6];
                break;
            case 8:
                mapNameText.text = "Waterfall 2";
                previewObject.texture = previewImages[7];
                break;
            case 9:
                mapNameText.text = "Waterfall 3";
                previewObject.texture = previewImages[8];
                break;
            case 10:
                mapNameText.text = "Waterfall Special";
                previewObject.texture = previewImages[9];
                break;
            case 11:
                mapNameText.text = "Space 1";
                previewObject.texture = previewImages[10];
                break;
            case 12:
                mapNameText.text = "Space 2";
                previewObject.texture = previewImages[11];
                break;
            case 13:
                mapNameText.text = "Space 3";
                previewObject.texture = previewImages[12];
                break;
            case 14:
                mapNameText.text = "Space 4";
                previewObject.texture = previewImages[13];
                break;
        }
    }
}
