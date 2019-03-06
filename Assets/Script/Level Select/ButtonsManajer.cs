using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsManajer : MonoBehaviour {
    [HideInInspector]
    public int WhatLevelTypeYouWant;
    public Sprite LockImage;
    public AudioClip ButtonSfx;
    private AudioSource AudioSrc;
    [Space(25)]
    public List<LevelSelector> levelType;
    [Space(10)]
    public Button Btn4x4SelectType;
    public Image Btn4x4SelectTypeLockImage;
    public Button Btn5x5SelectType;
    public Image Btn5x5SelectTypeLockImage;
    [Space(30)]
    public List<Button> AllButton;

    private void Awake()
    {

        AudioSrc = GetComponent<AudioSource>();

        AllButton[0].interactable = true;
        AllButton[0].transform.GetChild(1).GetComponent<Image>().gameObject.SetActive(false);
        SetLevelTypeButtons();
    }

    //[ContextMenu("Create Buttons")]
    public void CreateButtons()
    {
        Debug.Log(levelType[WhatLevelTypeYouWant].LevelType);
        for (int i = 0; i < AllButton.Count; i++)
        {
            int levelToLoad = i;
            if (AllButton.Count == levelType[WhatLevelTypeYouWant].ButtonImage.Length)
            {
                AllButton[i].transform.GetChild(0).GetComponent<Image>().sprite = levelType[WhatLevelTypeYouWant].ButtonImage[i];
                AllButton[i].transform.GetChild(1).GetComponent<Image>().sprite = LockImage;
                AllButton[i].onClick.RemoveAllListeners();
                AllButton[i].onClick.AddListener(() => LoadLevel(levelToLoad));

                if (PlayerPrefs.GetInt(levelType[WhatLevelTypeYouWant].LevelType + "_" + i) == 1)
                {
                    if(i < AllButton.Count - 1)
                    {
                        AllButton[i + 1].interactable = true;
                        AllButton[i + 1].transform.GetChild(1).GetComponent<Image>().gameObject.SetActive(false);
                    }
                }
                else
                {
                    if (i < AllButton.Count - 1)
                    {
                        AllButton[i + 1].interactable = false;
                        AllButton[i + 1].transform.GetChild(1).GetComponent<Image>().gameObject.SetActive(true);
                    }
                        
                }
            }
        }
    }

    private void LoadLevel(int LevelToLoad)
    {
        //SceneNumber.TheInstanceOfSceneNumber.LevelNumberToLoad = LevelToLoad;
        //SceneNumber.TheInstanceOfSceneNumber.LevelTypeToLoad = WhatLevelTypeYouWant;
        GameMaster.LevelNumber = LevelToLoad;
        GameMaster.levelToLoad = WhatLevelTypeYouWant;
        //Puzzle.ammountBlockPerLine = 2;
        StartCoroutine(LoadScene());
    }
    public IEnumerator LoadScene()
    {
        AudioSrc.PlayOneShot(ButtonSfx);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Main_Game");
    }

    // For Select Game Type
    public void SelectTypeGame(int Type)
    {
        WhatLevelTypeYouWant = Type;
        MenuSelectLevel.TheInstanceOfMenuSelectLevel.ShowButtons(0);
        CreateButtons();
    }

    public void SetLevelTypeButtons()
    {
        if(PlayerPrefs.GetInt(levelType[0].LevelType + "_" + 99) == 1)
        {
            Btn4x4SelectType.interactable = true;
            Btn4x4SelectTypeLockImage.gameObject.SetActive(false);
        }
        else
        {
            Btn4x4SelectType.interactable = false;
            Btn4x4SelectTypeLockImage.gameObject.SetActive(true);
        }

        if (PlayerPrefs.GetInt(levelType[1].LevelType + "_" + 99) == 1)
        {
            Btn5x5SelectType.interactable = true;
            Btn5x5SelectTypeLockImage.gameObject.SetActive(false);
        }
        else
        {
            Btn5x5SelectType.interactable = false;
            Btn5x5SelectTypeLockImage.gameObject.SetActive(true);
        }
    }
}
