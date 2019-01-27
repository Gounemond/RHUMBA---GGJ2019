using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameManager : MonoBehaviour {
    public static MainGameManager Instance { get; private set; }

    public GameConfig gameConfig;

    [Header("PreGame")]
    public CountdownManager countdownManager;
    public AudioSource audio_backgroundMusic;
    public AudioClip mus_StartingGame;
    public AudioClip mus_RoombaGame;
    public AudioClip sfx_BatteryDown;

    public List<PlayerController> roombaPlayer;

    public float matchTimeDuration = 120;
    public float currentTimer = 0;

    public Transform spawnPoint;

    void Awake() {
        DontDestroyOnLoad(this);
        Instance = this;
        GameRandom.Core = new DefaultRandom();
        roombaPlayer = new List<PlayerController>();
    }

    IEnumerator Start()
    {
        Debug.Log("Numero di players " + GameData.playerData?.Count);
        // Starting here the music (pregame clip)
        audio_backgroundMusic.Play();

        // In this phase we're doing the countdown. See countdownmanager for this part
        yield return StartCoroutine(countdownManager.Init());

        // Here we spawn the players (and we switch camera), but you won't be able to move as we're still in countdown phase
        SpawnPlayers();

        // Let's do five seconds of countdown
        yield return StartCoroutine(countdownManager.Countdown());

        // Brutally switching background music clip with the battle one
        audio_backgroundMusic.clip = mus_RoombaGame;
        audio_backgroundMusic.Play();

        countdownManager.PlayGoSound();
        // Enabling movement for each roomba
        foreach (PlayerController roomba in roombaPlayer)
        {
            roomba.EnableMovement();
            yield return null;
        }

        // Here we go with the standard game time
        yield return StartCoroutine(LoopGame());
        //yield return new WaitForSeconds(matchTimeDuration);


        Debug.Log("CAMADONNA E' FINITA");

        audio_backgroundMusic.clip = sfx_BatteryDown;
        audio_backgroundMusic.loop = false;
        audio_backgroundMusic.Play();

        foreach (PlayerController roomba in roombaPlayer)
        {
            roomba.DisableMovement();
        }

        yield return new WaitForSeconds(1);

        countdownManager.PlayCameraEndCinematic();

        yield return new WaitForSeconds(2);

        yield return StartCoroutine(countdownManager.FadeOut(1));
        SceneManager.LoadScene(2);
    }

    public IEnumerator LoopGame()
    {
        while (currentTimer < matchTimeDuration)
        {
            currentTimer += Time.deltaTime;
            yield return null;
        }
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
    }
    
    void SpawnPlayers() {
        for(int i = 0; i < GameData.playerData?.Count; i++) {
            GameObject gameObject = InstantiateAsset(gameConfig.roombaConfig.prefab);
            if(gameObject != null) {
                gameObject.transform.position = new Vector3(spawnPoint.position.x + 10 * i, spawnPoint.position.y, spawnPoint.position.z);
                PlayerController playerController = gameObject.GetComponent<PlayerController>();
                if(playerController != null) {
                    playerController.Init(GameData.playerData[i].playerId);
                    playerController.enabled = true;
                    roombaPlayer.Add(playerController);
                }
            }
        }
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