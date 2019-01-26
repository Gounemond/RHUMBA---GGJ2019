using UnityEngine;

public class PlayerController : MonoBehaviour {
    void Update() {
        switch(GameManager.Instance.gameConfig.roombaConfig.inputMode) {
            case RoombaInputMode.Move:
                float translation = GameManager.Instance.gameConfig.roombaConfig.baseMoveSpeed * Time.deltaTime;
                float rotation = GameManager.Instance.gameConfig.roombaConfig.baseTurnSpeed * Time.deltaTime;

                if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0) {
                    transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * rotation);
                }
                if(Mathf.Abs(Input.GetAxis("Vertical")) > 0) {
                    transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * translation);
                }
                break;
            case RoombaInputMode.Crash:
                //TODO: input alternativo "Crash"
                break;
        }
    }
}
