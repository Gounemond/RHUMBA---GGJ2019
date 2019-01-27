using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {
    public UIMenuManager menuUIManager;

    public GameObject[] scoreBars = new GameObject[4];
    public float scoreBarHeightMax = 300f;
    public float scoreTweenDurationMax = 6f;
    
    IEnumerator Start() {
        yield return StartCoroutine(menuUIManager.FadeIn(1));
        for(int i = 0; i < GameData.playerData?.Count; i++) {
            scoreBars[i].GetComponent<Image>().color = GameData.playerData[i].graphics.trailColor;
            RectTransform rectTransform = scoreBars[i].GetComponent<RectTransform>();
            scoreBars[i].transform.parent.gameObject.SetActive(true);
            float tweenDuration = scoreTweenDurationMax * GameData.playerData[i].tilesCleanedPercentage;
            rectTransform.DOSizeDelta(new Vector2(rectTransform.rect.width, scoreBarHeightMax * GameData.playerData[i].tilesCleanedPercentage), tweenDuration);
            StartCoroutine(scoreBars[i].GetComponentInChildren<ScoreLabelController>().CountTo(Mathf.RoundToInt(GameData.playerData[i].tilesCleanedPercentage * 10000) / 100f, tweenDuration));
        }
        yield return new WaitForSeconds(10f);
        yield return StartCoroutine(menuUIManager.FadeOut(1));

        SceneManager.LoadScene(0);
    }
}