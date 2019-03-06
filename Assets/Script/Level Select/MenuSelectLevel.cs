using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSelectLevel : MonoBehaviour {
    private static MenuSelectLevel instance;
    public static MenuSelectLevel TheInstanceOfMenuSelectLevel
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MenuSelectLevel>();
            }
            return instance;
        }
    }

    public static int CurrentMenu;
    public GameObject[] AmmountOfTheButtonHolder;
    
    [Space(25)]
    public GameObject MenuSelectPuzzle;
    public GameObject MenuSelectType;
    public GameObject MainMenu;
    //public bool MoveRight


    [ContextMenu("MOVE")]
    public void Move(bool MoveRight)
    {
        if (MoveRight)
        {
            CurrentMenu++;
            if (CurrentMenu > AmmountOfTheButtonHolder.Length - 1)
                CurrentMenu = 0;
        }
        else
        {
            CurrentMenu--;

            if (CurrentMenu < 0)
                CurrentMenu = AmmountOfTheButtonHolder.Length - 1;
        }

        ShowButtons(CurrentMenu);
    }

    public void ShowButtons(int value)
    {
        CurrentMenu = value;
        switch (value)
        {
            case 0:
                AmmountOfTheButtonHolder[0].SetActive(true);
                AmmountOfTheButtonHolder[1].SetActive(false);
                AmmountOfTheButtonHolder[2].SetActive(false);
                AmmountOfTheButtonHolder[3].SetActive(false);
                AmmountOfTheButtonHolder[4].SetActive(false);

                break;
            case 1:
                AmmountOfTheButtonHolder[0].SetActive(false);
                AmmountOfTheButtonHolder[1].SetActive(true);
                AmmountOfTheButtonHolder[2].SetActive(false);
                AmmountOfTheButtonHolder[3].SetActive(false);
                AmmountOfTheButtonHolder[4].SetActive(false);

                break;
            case 2:
                AmmountOfTheButtonHolder[0].SetActive(false);
                AmmountOfTheButtonHolder[1].SetActive(false);
                AmmountOfTheButtonHolder[2].SetActive(true);
                AmmountOfTheButtonHolder[3].SetActive(false);
                AmmountOfTheButtonHolder[4].SetActive(false);

                break;
            case 3:
                AmmountOfTheButtonHolder[0].SetActive(false);
                AmmountOfTheButtonHolder[1].SetActive(false);
                AmmountOfTheButtonHolder[2].SetActive(false);
                AmmountOfTheButtonHolder[3].SetActive(true);
                AmmountOfTheButtonHolder[4].SetActive(false);
                break;
            case 4:
                AmmountOfTheButtonHolder[0].SetActive(false);
                AmmountOfTheButtonHolder[1].SetActive(false);
                AmmountOfTheButtonHolder[2].SetActive(false);
                AmmountOfTheButtonHolder[3].SetActive(false);
                AmmountOfTheButtonHolder[4].SetActive(true);
                break;
        }
    }

    public void ToMainMenu(Animator Anim)
    {
        StartCoroutine(ToMenu(Anim));
    }
    IEnumerator ToMenu(Animator anim)
    {
        anim.SetTrigger("Close");
        yield return new WaitForSeconds(2f);
        MainMenu.SetActive(true);
        MenuSelectPuzzle.SetActive(false);
        MenuSelectType.SetActive(false);
    }

    public void ToMenuSelectType(Animator Anim)
    {
        StartCoroutine(ToMenuType(Anim));
    }
    IEnumerator ToMenuType(Animator anim)
    {
        anim.SetTrigger("Close");
        yield return new WaitForSeconds(2f);
        MainMenu.SetActive(false);
        MenuSelectType.SetActive(true);
        MenuSelectPuzzle.SetActive(false);
        
    }

    public void ToMenuSelectPuzzle(Animator Anim)
    {
        StartCoroutine(ToMenuPuzzle(Anim));
    }
    IEnumerator ToMenuPuzzle(Animator anim)
    {
        anim.SetTrigger("Close");
        yield return new WaitForSeconds(2f);
        MainMenu.SetActive(false);
        MenuSelectType.SetActive(false);
        MenuSelectPuzzle.SetActive(true);

    }


}
