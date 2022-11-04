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

    public void Previous()
    {
        curMapIndex--;
        if (curMapIndex < 2)
        {
            curMapIndex = SceneManager.sceneCountInBuildSettings-1;
        }
    }

    public void Next()
    {
        curMapIndex++;
        if(curMapIndex > SceneManager.sceneCountInBuildSettings-1)
        {
            curMapIndex = 2;
        }
    }

    public void ConfirmSelection()
    {
        SceneManager.LoadScene(curMapIndex);
    }

    private void Update()
    {
        switch (curMapIndex)
        {
            case 2:
                mapNameText.text = "Aquarium 1";
                previewObject.texture = previewImages[0];
                break;
            case 3:
                mapNameText.text = "Ocean 1";
                previewObject.texture = previewImages[1];
                break;
            case 4:
                mapNameText.text = "River 1";
                previewObject.texture = previewImages[2];
                break;
            case 5:
                mapNameText.text = "River 2";
                previewObject.texture = previewImages[3];
                break;
            case 6:
                mapNameText.text = "Space 1";
                previewObject.texture = previewImages[4];
                break;     
        }
    }
}
