using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CountdownManager : MonoBehaviour
{
    [Header("Controls part")]
    public Image fadePanel;
    public Image controlScreen;
    public Animator cameraAnimator;
    public Camera cinematicCamera;

    [Header("Countdown")]
    public Image[] countdownImage;
    public AudioSource audioCountdown;
    public AudioClip[] sfx_countdown;
    public AudioClip sfx_StartGame;


    public GameObject player0Score;
    public GameObject player1Score;
    public GameObject player2Score;
    public GameObject player3Score;



    // Start is called before the first frame update
    void Start()
    {      
    }

    public IEnumerator Init()
    {
        // Fade in, show controls
        yield return StartCoroutine(FadeIn(0.5f));
        yield return StartCoroutine(FadeOutControls(0.5f));
        yield return new WaitForSeconds(2);
        cameraAnimator.SetTrigger("StartCamera");
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(FadeInControls(1));

        yield return new WaitForSeconds(1.35f);

        cinematicCamera.enabled = false;
        // Very good, now countdown the main manager will make the player spawn, then countdown will be done
    }

    public IEnumerator Countdown()
    {
        for (int i = 0; i < countdownImage.Length; i++)
        {
            countdownImage[i].enabled = true;
            audioCountdown.PlayOneShot(sfx_countdown[i]);
            countdownImage[i].transform.DOScale(2, 0.5f);
            yield return new WaitForSeconds(0.9f);
            countdownImage[i].enabled = false;
            countdownImage[i].transform.DOScale(1, 0.1f);
            yield return new WaitForSeconds(0.1f);
        }

        if (GameData.playerData?.Count == 2)
        {
            player0Score.gameObject.SetActive(true);
            player1Score.gameObject.SetActive(true);
        }
        else if (GameData.playerData?.Count == 3)
        {
            player0Score.gameObject.SetActive(true);
            player1Score.gameObject.SetActive(true);
            player2Score.gameObject.SetActive(true);
        }
        else if (GameData.playerData?.Count == 4)
        {
            player0Score.gameObject.SetActive(true);
            player1Score.gameObject.SetActive(true);
            player2Score.gameObject.SetActive(false);
            player3Score.gameObject.SetActive(true);
        }
    }

    public void PlayGoSound()
    {
        audioCountdown.PlayOneShot(sfx_StartGame);
    }

    public void PlayCameraEndCinematic()
    {
        cinematicCamera.enabled = true;
        cameraAnimator.SetTrigger("EndCinematic");
        cinematicCamera.depth = 0;

            player0Score.gameObject.SetActive(false);
            player1Score.gameObject.SetActive(false);
            player2Score.gameObject.SetActive(false);
            player3Score.gameObject.SetActive(false);
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Calling this will make the fade panel turn clear in given time
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public IEnumerator FadeIn(float time)
    {
        fadePanel.enabled = true;
        fadePanel.DOColor(Color.clear, time);
        yield return new WaitForSeconds(time);
   
    }

    /// <summary>
    /// Calling this will make the fade panel turn black in given time
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public IEnumerator FadeOut(float time)
    {
        fadePanel.DOColor(Color.black, time);
        yield return new WaitForSeconds(time);
        fadePanel.enabled = false;
    }

    /// <summary>
    /// Calling this will make the control screen panel turn clear in given time
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public IEnumerator FadeInControls(float time)
    {
        controlScreen.DOColor(Color.clear, time);
        yield return new WaitForSeconds(time);
        controlScreen.enabled = false;
    }

    /// <summary>
    /// Calling this will make the control screen panel appear in given time
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    public IEnumerator FadeOutControls(float time)
    {
        controlScreen.enabled = true;
        controlScreen.DOColor(Color.white, time);
        yield return new WaitForSeconds(time);
    }
}
