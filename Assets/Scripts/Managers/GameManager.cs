using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    public GameConfig gameConfig;

    void Awake() {
        DontDestroyOnLoad(this);
        Instance = this;
        GameRandom.Core = new DefaultRandom();
    }

    void Start() {
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}