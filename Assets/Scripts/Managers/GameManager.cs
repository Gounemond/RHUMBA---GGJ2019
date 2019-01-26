using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    public GameConfig gameConfig;

    public int totalPlayers;

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
        if(totalPlayers == 0) {
            if(Input.GetKeyDown(KeyCode.Alpha1)) {
                SpawnPlayers(1);
            }
            if(Input.GetKeyDown(KeyCode.Alpha2)) {
                SpawnPlayers(2);
            }
            if(Input.GetKeyDown(KeyCode.Alpha3)) {
                SpawnPlayers(3);
            }
            if(Input.GetKeyDown(KeyCode.Alpha4)) {
                SpawnPlayers(4);
            }
        }
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void SpawnPlayers(int numPlayers) {
        float cameraRectWidth = numPlayers > 2 ? 0.5f : 1f;
        float cameraRectHeight = numPlayers > 1 ? 0.5f : 1f;

        for(int i = 0; i < numPlayers; i++) {
            GameObject gameObject = InstantiateAsset(gameConfig.roombaConfig.prefab);
            if(gameObject != null) {
                gameObject.transform.position = new Vector3(50 + 10 * i, gameObject.transform.position.y, -160);
                PlayerController playerController = gameObject.GetComponent<PlayerController>();
                if(playerController != null) {
                    playerController.Init(i);
                    playerController.enabled = true;
                }
                /*Camera playerCamera = gameObject.GetComponentInChildren<Camera>();
                if(playerCamera != null) {
                    if(i == 0) {
                        playerCamera.rect = new Rect(0f, 0f, cameraRectWidth, cameraRectHeight);
                    }
                    playerCamera.rect = new Rect(0.5f * i, 0.5f * i, cameraRectWidth, cameraRectHeight);
                }*/
            }
        }
        totalPlayers = numPlayers;
    }

    GameObject InstantiateAsset(GameObject asset) {
        GameObject gameObject = null;
        if(asset != null) {
            try {
                gameObject = Instantiate(asset);
            } catch(Exception ex) {
                Debug.Log("Cannot instantiate " + asset.name);
                Debug.LogException(ex);
            }
        }
        return gameObject;
    }
}