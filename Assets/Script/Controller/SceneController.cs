using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    public AudioSource AudioSrc;
    public Animator PanelTransisiAnim;
    public GameObject puzzleManajerGameObject;

    private void Awake()
    {
        //get refrence to audio source
        if (AudioSrc == null)
            AudioSrc = GetComponent<AudioSource>();

        if (PanelTransisiAnim == null)
            PanelTransisiAnim = GameObject.Find("Panel Transisi").GetComponent<Animator>();
    }
    private void Start()
    {
        if (AdsController.TheInstanceOfAdsController!= null)
        {
            AdsController.TheInstanceOfAdsController.ShowBanner();
        }
    }
    // to next level
    public void NextLevel()
    {
        if (AdsController.TheInstanceOfAdsController != null)
        {
            AdsController.TheInstanceOfAdsController.ShowInterstitial();
        }
        StartCoroutine(nextLvl());
    }

    IEnumerator nextLvl()
    {
        puzzleManajerGameObject.SetActive(false);
        PanelTransisiAnim.SetTrigger("Close");
        yield return new WaitForSeconds(1f);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        if (GameMaster.LevelNumber < GameMaster.TheInstanceOfGameMaster.levelType[GameMaster.levelToLoad].ButtonImage.Length - 1)
        {
            GameMaster.LevelNumber++;
            SceneManager.LoadScene(Application.loadedLevel);
        }
        else
        {
            SceneManager.LoadScene("Main_Menu");
        }

        
    }
    // to restart level
    public void RestartLevel()
    {
        if (AdsController.TheInstanceOfAdsController != null)
        {
            AdsController.TheInstanceOfAdsController.ShowInterstitial();
        }
        StartCoroutine(restart());
    }
    IEnumerator restart()
    {
        puzzleManajerGameObject.SetActive(false);
        PanelTransisiAnim.SetTrigger("Close");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(Application.loadedLevel);
    }
    // to load scene with name 
    public void LoadScene(string SceneName)
    {
        if (AdsController.TheInstanceOfAdsController != null)
        {
            AdsController.TheInstanceOfAdsController.ShowInterstitial();
        }
        StartCoroutine(load(SceneName));
    }
    IEnumerator load(string name)
    {
        puzzleManajerGameObject.SetActive(false);
        PanelTransisiAnim.SetTrigger("Close");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(name);
    }
    // to exit Game
    public void ExitGame()
    {
        StartCoroutine(Exit());
    }
    IEnumerator Exit()
    {
        puzzleManajerGameObject.SetActive(false);
        PanelTransisiAnim.SetTrigger("Close");
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }
    // playing sound effect
    public void PlaySfx(AudioClip Sfx)
    {
        if (AudioSrc.isPlaying)
        {
            AudioSrc.Stop();
        }
        AudioSrc.PlayOneShot(Sfx);
    }
    // To Showing Video Ads
    public void ShowingVideoAds()
    {
        if (AdsController.TheInstanceOfAdsController != null)
        {
            AdsController.TheInstanceOfAdsController.ShowAdsVideo();
        }
    }
    // For Delete All Player Progress Data
    [ContextMenu("Delete All Data")]
    public void DeleteAllProgress()
    {
        PlayerPrefs.DeleteAll();
    }
}
