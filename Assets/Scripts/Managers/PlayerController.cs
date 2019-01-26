using Rewired;
using UnityEngine;

public class PlayerController : MonoBehaviour {
   public int playerId;
    Player player;
    TrailRenderer trailRenderer;

    bool isMoving;
    int rotationVerse;

    public void Init(int id) {
        playerId = id;
    }

    void Awake() {
        trailRenderer = GetComponentInChildren<TrailRenderer>();
    }

    void Start() {
        player = ReInput.players.GetPlayer(playerId);
        if(GameManager.Instance.gameConfig.roombaConfig.trailColor?.Length > playerId) {
            trailRenderer.startColor = GameManager.Instance.gameConfig.roombaConfig.trailColor[playerId];
            trailRenderer.endColor = trailRenderer.startColor;
        }
        isMoving = false;
        rotationVerse = GameRandom.Core.NextSign();

        CameraController cameraController = gameObject.GetComponentInChildren<CameraController>();
        if(cameraController != null) {
            cameraController.Init(playerId, GameManager.Instance.totalPlayers);
            cameraController.enabled = true;
        }
    }

    void Update() {
        switch(GameManager.Instance.gameConfig.roombaConfig.inputMode) {
            case RoombaInputMode.Move:
                if(Mathf.Abs(player.GetAxis("Move")) > 0) {
                    transform.Translate(Vector3.forward * player.GetAxis("Move") * GameManager.Instance.gameConfig.roombaConfig.baseMoveSpeed * Time.deltaTime);
                }
                if(Mathf.Abs(player.GetAxis("Rotate")) > 0) {
                    transform.Rotate(Vector3.up * player.GetAxis("Rotate") * GameManager.Instance.gameConfig.roombaConfig.baseTurnSpeed * Time.deltaTime);
                }
                break;
            case RoombaInputMode.Crash:
                if(isMoving) {
                    transform.Translate(Vector3.forward * GameManager.Instance.gameConfig.roombaConfig.baseMoveSpeed * Time.deltaTime);
                } else if(player.GetButtonDown("SelectDirection")) {
                    ToggleCrashMovement(true);
                } else {
                    transform.Rotate(Vector3.up * rotationVerse * GameManager.Instance.gameConfig.roombaConfig.baseTurnSpeed * Time.deltaTime);
                }
                break;
        }
    }

    void OnCollisionEnter(Collision collision) {
        OnCollision(collision.gameObject.tag);
    }
    void OnCollisionStay(Collision collision) {
        OnCollision(collision.gameObject.tag);
    }

    void OnCollision(string tag) {
        if(GameManager.Instance.gameConfig.roombaConfig.inputMode == RoombaInputMode.Crash && tag == "Obstacle") {
            ToggleCrashMovement(false);
        }
    }

    void ToggleCrashMovement(bool validMove) {
        isMoving = validMove;
        rotationVerse = validMove ? 0 : rotationVerse != 0 ? rotationVerse : GameRandom.Core.NextSign();
        //trailRenderer.emitting = validMove;
    }
}
