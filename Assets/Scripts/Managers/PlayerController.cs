using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float currentMoveSpeed;
    public float currentTurnSpeed;

    void Awake() {
    }

    // Start is called before the first frame update
    void Start() {
        RoombaConfig roombaConfig = GameManager.Instance.gameConfig.roombaConfig;
        currentMoveSpeed = roombaConfig.baseMoveSpeed;
        currentTurnSpeed = roombaConfig.baseTurnSpeed;
    }

    // Update is called once per frame
    void Update() {
        //TODO: input alternativo "Crash"
        if(GameManager.Instance.gameConfig.roombaConfig.inputMode == RoombaInputMode.Move) {
            var translation = GameManager.Instance.gameConfig.roombaConfig.baseMoveSpeed * Time.deltaTime;
            var rotation = GameManager.Instance.gameConfig.roombaConfig.baseTurnSpeed * Time.deltaTime;

            if(Input.GetKey(KeyCode.UpArrow)) {
                transform.Translate(Vector3.forward * translation);
            }
            if(Input.GetKey(KeyCode.DownArrow)) {
                transform.Translate(-Vector3.forward * translation);
            }
            if(Input.GetKey(KeyCode.LeftArrow)) {
                transform.Rotate(Vector3.up, -rotation);
            }
            if(Input.GetKey(KeyCode.RightArrow)) {
                transform.Rotate(Vector3.up, rotation);
            }
        }
    }
}
