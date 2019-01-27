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
        foreach(PlayerData playerData in GameData.playerData) {
            GameObject scoreBar = scoreBars[playerData.playerId];
            scoreBar.GetComponent<Image>().color = playerData.graphics.trailColor;
            scoreBar.transform.parent.gameObject.SetActive(true);
            float tweenDuration = scoreTweenDurationMax * playerData.tilesCleanedPercentage;
            RectTransform rectTransform = scoreBar.GetComponent<RectTransform>();
            rectTransform.DOSizeDelta(new Vector2(rectTransform.rect.width, scoreBarHeightMax * playerData.tilesCleanedPercentage), tweenDuration);
            StartCoroutine(scoreBar.GetComponentInChildren<ScoreLabelController>().CountTo(Mathf.RoundToInt(playerData.tilesCleanedPercentage * 10000) / 100f, tweenDuration));
        }
        yield return new WaitForSeconds(10f);
        yield return StartCoroutine(menuUIManager.FadeOut(1));

        SceneManager.LoadScene(0);
    }
}