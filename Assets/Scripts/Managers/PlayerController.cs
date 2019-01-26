using Rewired;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    int playerId;
    Player player;

    bool isMoving;
    int rotationVerse;

    public void Init(int id) {
        playerId = id;
    }

    void Awake() {
    }

    void Start() {
        player = ReInput.players.GetPlayer(playerId);
        isMoving = false;
        rotationVerse = GameRandom.Core.NextSign();
    }

    void Update() {
        switch(GameManager.Instance.gameConfig.roombaConfig.inputMode) {
            case RoombaInputMode.Move:
                if(Mathf.Abs(player.GetAxis("Move")) > 0) {
                    transform.Rotate(Vector3.up * player.GetAxis("Move") * GameManager.Instance.gameConfig.roombaConfig.baseTurnSpeed * Time.deltaTime);
                }
                if(Mathf.Abs(player.GetAxis("Rotate")) > 0) {
                    transform.Translate(Vector3.forward * player.GetAxis("Rotate") * GameManager.Instance.gameConfig.roombaConfig.baseMoveSpeed * Time.deltaTime);
                }
                break;
            case RoombaInputMode.Crash:
                //TODO: input alternativo "Crash"
                if(isMoving) {
                    transform.Translate(Vector3.forward * GameManager.Instance.gameConfig.roombaConfig.baseMoveSpeed * Time.deltaTime);
                } else if(player.GetButtonDown("SelectDirection")) {
                    isMoving = true;
                    rotationVerse = 0;
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
        if(tag == "Obstacle") {
            isMoving = false;
            rotationVerse = rotationVerse != 0 ? rotationVerse : GameRandom.Core.NextSign();
        }
    }
}
