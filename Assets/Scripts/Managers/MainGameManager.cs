using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainGameManager : MonoBehaviour {
    public static MainGameManager Instance { get; private set; }

    public GameConfig gameConfig;

    public int totalPlayers;

    [Header("PreGame")]
    public CountdownManager countdownManager;
    public AudioSource audio_backgroundMusic;
    public AudioClip mus_StartingGame;
    public AudioClip mus_RoombaGame;

    public List<PlayerController> roombaPlayer;

    public float matchTimeDuration = 138;

    public Transform spawnPoint;

    void Awake() {
        DontDestroyOnLoad(this);
        Instance = this;
        GameRandom.Core = new DefaultRandom();
        roombaPlayer = new List<PlayerController>();
    }

    IEnumerator Start()
    {
        audio_backgroundMusic.Play();


        yield return StartCoroutine(countdownManager.Init());
        SpawnPlayers(4);


        yield return StartCoroutine(countdownManager.Countdown());

        audio_backgroundMusic.clip = mus_RoombaGame;
        audio_backgroundMusic.Play();
        foreach (PlayerController roomba in roombaPlayer)
        {
            roomba.EnableMovement();
            yield return null;
        }

        // Here we go with the standard game time
        yield return new WaitForSeconds(matchTimeDuration);
        Debug.Log("CAMADONNA E' FINITA");

        //SceneManager.LoadScene(0);
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
                    roombaPlayer.Add(playerController);
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