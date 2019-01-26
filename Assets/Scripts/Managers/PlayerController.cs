using UnityEngine;

public class PlayerController : MonoBehaviour {
    bool isMoving;
    int rotationVerse;

    void Start() {
        isMoving = false;
        rotationVerse = GameRandom.Core.NextSign();
    }

    void Update() {
        float translation = GameManager.Instance.gameConfig.roombaConfig.baseMoveSpeed * Time.deltaTime;
        float rotation = GameManager.Instance.gameConfig.roombaConfig.baseTurnSpeed * Time.deltaTime;
        switch(GameManager.Instance.gameConfig.roombaConfig.inputMode) {
            case RoombaInputMode.Move:
                if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0) {
                    transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * rotation);
                }
                if(Mathf.Abs(Input.GetAxis("Vertical")) > 0) {
                    transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * translation);
                }
                break;
            case RoombaInputMode.Crash:
                //TODO: input alternativo "Crash"
                if(isMoving) {
                    transform.Translate(Vector3.forward * translation);
                } else if(Input.GetButtonDown("Fire1")) {
                    isMoving = true;
                    //transform.Translate(Vector3.forward * translation);
                } else {
                    transform.Rotate(Vector3.up * rotation * rotationVerse);
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
            rotationVerse = GameRandom.Core.NextSign();
        }
    }
}
