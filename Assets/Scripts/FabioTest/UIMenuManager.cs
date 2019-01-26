using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIMenuManager : MonoBehaviour
{
    public Image fadePanel;

    public Animator[] RoombaReady;



    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    public void ReadyPressed(int playerIndex)
    {
        RoombaReady[playerIndex].SetTrigger("Cheers");
    }
}
