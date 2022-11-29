using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectionController : MonoBehaviour
{
    public SpriteRenderer sr;
    public RawImage characterImage;
    public List<Sprite> skins = new List<Sprite>();
    private int selectedSkin = 0;
    public GameObject playerSkin;
    public int playerNumber;

    public void NextOption()
    {
        SoundManager.PlaySound("ui_selection");
        selectedSkin++;
        if (selectedSkin > skins.Count-1)
        {
            selectedSkin = 0;
        }
        sr.sprite = skins[selectedSkin];
        characterImage.texture = skins[selectedSkin].texture;
    }

    public void BackOption()
    {
        SoundManager.PlaySound("ui_selection");
        selectedSkin--;
        if (selectedSkin < 0)
        {
            selectedSkin = skins.Count-1;
        }
        sr.sprite = skins[selectedSkin];
        characterImage.texture = skins[selectedSkin].texture;
    }

    public void ConfirmSelection()
    {
        SoundManager.PlaySound("ui_selection");
        Button[] buttons = transform.GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        //PrefabUtility.SaveAsPrefabAsset(playerSkin, "Assets/Resources/PlayerSkins/selectedSkin" + playerNumber + ".prefab");
        //SceneManager.LoadScene("AquariumTemplate");
    }
}
