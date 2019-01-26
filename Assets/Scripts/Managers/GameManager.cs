using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    public GameConfig gameConfig;

    public int totalPlayers;

    public Transform spawnPoint;

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
        //TODO: remove
        if(Input.GetKeyDown(KeyCode.Equals) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) {
            gameConfig.roombaConfig.baseMoveSpeed += 5;
            if(gameConfig.roombaConfig.baseMoveSpeed > 50) {
                gameConfig.roombaConfig.baseMoveSpeed = 50;
            }
        }
        if(Input.GetKeyDown(KeyCode.Minus) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))) {
            gameConfig.roombaConfig.baseMoveSpeed -= 5;
            if(gameConfig.roombaConfig.baseMoveSpeed < 0) {
                gameConfig.roombaConfig.baseMoveSpeed = 0;
            }
        }
        if(Input.GetKeyDown(KeyCode.Equals) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))) {
            gameConfig.roombaConfig.baseTurnSpeed += 5;
            if(gameConfig.roombaConfig.baseTurnSpeed > 150) {
                gameConfig.roombaConfig.baseTurnSpeed = 150;
            }
        }
        if(Input.GetKeyDown(KeyCode.Minus) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))) {
            gameConfig.roombaConfig.baseTurnSpeed -= 5;
            if (gameConfig.roombaConfig.baseTurnSpeed < 0) {
                gameConfig.roombaConfig.baseTurnSpeed = 0;
            }
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
        for(int i = 0; i < numPlayers; i++) {
            GameObject gameObject = InstantiateAsset(gameConfig.roombaConfig.prefab);
            if(gameObject != null) {
                gameObject.transform.position = new Vector3(spawnPoint.position.x + 10 * i, spawnPoint.position.y, spawnPoint.position.z);
                PlayerController playerController = gameObject.GetComponent<PlayerController>();
                if(playerController != null) {
                    playerController.Init(i);
                    playerController.enabled = true;
                }
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