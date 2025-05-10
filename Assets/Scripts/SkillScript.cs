using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SkillScript : MonoBehaviour
{
    public Button button;
    public RawImage imagesToShow;
    public Sprite unlockedSprite;
    public TextMeshProUGUI unlockText;
    public TextMeshProUGUI unlockPayText;
    public String afterUnlockText;
    public String afterUnlockPayText;

    public bool isFinalButton = false;
    public Button finalButton;
    public int isFinalButtonActive;

    public GameObject skillTree;
    public GameObject startMenu;


    void Start()
    {
        button.onClick.AddListener(() => onClick());
    }

    void onClick()
    {
        imagesToShow.gameObject.SetActive(true);


        Image btnImage = button.GetComponent<Image>();
        if (btnImage != null && unlockedSprite != null)
        {
            btnImage.sprite = unlockedSprite;
        }
        if(finalButton){
            finalButton.gameObject.SetActive(true);
        }

        //unlockText.gameObject.SetActive(false);
        unlockText.text = afterUnlockText;
        unlockPayText.text = afterUnlockPayText;
        unlockPayText.color = Color.green;
        RectTransform rect = unlockPayText.GetComponent<RectTransform>();
        Vector2 pos = rect.anchoredPosition;
        pos.y = -60f;
        rect.anchoredPosition = pos;
        
        if(finalButton){
            finalButton.gameObject.SetActive(true);
            isFinalButtonActive++;
        }
    }

    public void backToGame() {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        skillTree.SetActive(false);
        startMenu.SetActive(false);
    }
}