using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    public GameObject[] scoreBars = new GameObject[4];
    public float scoreBarHeightMax = 300f;
    public float scoreTweenDurationMax = 6f;

    //TODO: debug
    public List<PlayerData> debugPlayerData;
    void Awake() {
        //TODO: debug
        GameData.playerData = debugPlayerData;
    }

    IEnumerator Start() {
        //yield return StartCoroutine(menuUIManager.FadeIn(1));
        for(int i = 0; i < GameData.playerData.Count; i++) {
            scoreBars[i].GetComponent<Image>().color = GameData.playerData[i].trailColor;
            RectTransform rectTransform = scoreBars[i].GetComponent<RectTransform>();
            scoreBars[i].transform.parent.gameObject.SetActive(true);
            float tweenDuration = scoreTweenDurationMax * GameData.playerData[i].tilesCleanedPercentage;
            rectTransform.DOSizeDelta(new Vector2(rectTransform.rect.width, scoreBarHeightMax * GameData.playerData[i].tilesCleanedPercentage), tweenDuration);
            StartCoroutine(scoreBars[i].GetComponentInChildren<ScoreLabelController>().CountTo(Mathf.RoundToInt(GameData.playerData[i].tilesCleanedPercentage * 10000) / 100f, tweenDuration));
        }
        yield return new WaitForSeconds(10f);
        //yield return StartCoroutine(menuUIManager.FadeOut(1));

        //LoadScene;
    }
}