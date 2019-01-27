using Rewired;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public int playerId;
    int _index;
    Player player;
    MeshRenderer meshRenderer;
    TrailRenderer trailRenderer; 

    bool isMoving;
    int rotationVerse;

    bool batteryEnded = false;

    public void Init(int index) {
        _index = index;
        playerId = GameData.playerData[index].playerId;
    }

    void Awake() {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
    }

    void Start() {
        if(MainGameManager.Instance.gameConfig.roombaConfig.graphics?.Length > playerId) {
            GameData.playerData.Where(pd => pd.playerId == playerId).FirstOrDefault().graphics = MainGameManager.Instance.gameConfig.roombaConfig.graphics[playerId];
            meshRenderer.material = MainGameManager.Instance.gameConfig.roombaConfig.graphics[playerId].material;
            trailRenderer.startColor = MainGameManager.Instance.gameConfig.roombaConfig.graphics[playerId].trailColor;
            trailRenderer.endColor = trailRenderer.startColor;
        }
        isMoving = false;
        rotationVerse = GameRandom.Core.NextSign();

        CameraController cameraController = gameObject.GetComponentInChildren<CameraController>();
        if (cameraController != null)
        {
            cameraController.Init(_index, GameData.playerData.Count);
            cameraController.enabled = true;
        }
    }

    public void EnableMovement()
    {
        player = ReInput.players.GetPlayer(playerId);
    }

    public void DisableMovement()
    {
        batteryEnded = true;
        player = null;
    }

    void Update()
    {
        if (player != null)
        {
            switch (MainGameManager.Instance.gameConfig.roombaConfig.inputMode)
            {
                case RoombaInputMode.Move:
                    if (Mathf.Abs(player.GetAxis("Move")) > 0)
                    {
                        transform.Translate(Vector3.forward * player.GetAxis("Move") * MainGameManager.Instance.gameConfig.roombaConfig.baseMoveSpeed * Time.deltaTime);
                    }
                    if (Mathf.Abs(player.GetAxis("Rotate")) > 0)
                    {
                        transform.Rotate(Vector3.up * player.GetAxis("Rotate") * MainGameManager.Instance.gameConfig.roombaConfig.baseTurnSpeed * Time.deltaTime);
                    }
                    break;
                case RoombaInputMode.Crash:
                    if (isMoving)
                    {
                        transform.Translate(Vector3.forward * MainGameManager.Instance.gameConfig.roombaConfig.baseMoveSpeed * Time.deltaTime);
                    }
                    else if (player.GetButtonDown("SelectDirection"))
                    {
                        ToggleCrashMovement(true);
                    }
                    else
                    {
                        transform.Rotate(Vector3.up * rotationVerse * MainGameManager.Instance.gameConfig.roombaConfig.baseTurnSpeed * Time.deltaTime);
                    }
                    break;
            }
        }
        else
        {
            switch (MainGameManager.Instance.gameConfig.roombaConfig.inputMode)
            {
                case RoombaInputMode.Move:

                case RoombaInputMode.Crash:
                {
                    if (!batteryEnded)
                    {
                        transform.Rotate(Vector3.up * rotationVerse * MainGameManager.Instance.gameConfig.roombaConfig.baseTurnSpeed * Time.deltaTime);
                    }
                }
                break;
            }
        }
    }
    void OnCollisionEnter(Collision collision) {
        OnCollision(collision.gameObject.tag);
    }
    void OnCollisionStay(Collision collision) {
        OnCollision(collision.gameObject.tag);
    }

    void OnCollision(string tag) {
        if(MainGameManager.Instance.gameConfig.roombaConfig.inputMode == RoombaInputMode.Crash && tag == "Obstacle") {
            ToggleCrashMovement(false);
        }
    }

    void ToggleCrashMovement(bool validMove) {
        isMoving = validMove;
        rotationVerse = validMove ? 0 : rotationVerse != 0 ? rotationVerse : GameRandom.Core.NextSign();
        //trailRenderer.emitting = validMove;
    }
}
