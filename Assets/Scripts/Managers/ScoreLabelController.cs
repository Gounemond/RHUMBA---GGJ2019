using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreLabelController : MonoBehaviour {
    Text _text;
    [SerializeField]
    float _score = 0f;
    
    void Awake() {
        _text = GetComponent<Text>();
    }
    
    public IEnumerator CountTo(float target, float duration) {
        float start = _score;
        for(float timer = 0; timer < duration; timer += Time.deltaTime) {
            float progress = timer / duration;
            _score = (int) Mathf.Lerp(start, target, progress);
            _text.text = _score + "%";
            yield return null;
        }
        _score = target;
        _text.text = _score + "%";
    }
}