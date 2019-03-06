using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {
    private static GameMaster instance;
    public static GameMaster TheInstanceOfGameMaster
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameMaster>();
            }
            return instance;
        }
    }

    public static int LevelNumber;

    public static int levelToLoad;

    public LevelSelector[] levelType;
    [Tooltip("This Image For Background Panel")]
    public Image BgImage, FinalResultImage;
    [HideInInspector]
    public Sprite levelSpriteImage;

    public AudioSource audioSrc;
    public AudioClip WinClip;

    public GameObject AllGameObject;
    public GameObject PuzzleManajer;
    public GameObject MenuWinGame;

    private void Awake()
    {
        if (audioSrc == null)
            audioSrc = GetComponent<AudioSource>();

        InitializePuzzle();
        Debug.Log(levelType[levelToLoad].LevelType + "_" + LevelNumber);
        //if(LevelNumber == 99)
        //{
        //    Debug.Log(levelType[levelToLoad].ButtonImage[LevelNumber].name);
        //}
    }

    public void InitializePuzzle()
    {
        levelSpriteImage = levelType[levelToLoad].ButtonImage[LevelNumber];
        BgImage.sprite = levelType[levelToLoad].ButtonImage[LevelNumber];
        FinalResultImage.sprite = levelType[levelToLoad].ButtonImage[LevelNumber];
    }

    public void WinGameConditions()
    {
        StartCoroutine(winGame());
    }
    IEnumerator winGame()
    {
        PlayerPrefs.SetInt(levelType[levelToLoad].LevelType + "_" + LevelNumber, 1);
        PuzzleManajer.SetActive(false);
        AllGameObject.SetActive(false);
        MenuWinGame.SetActive(true);
        yield return new WaitForSeconds(1.5f);
       
        audioSrc.PlayOneShot(WinClip);
    }
}
