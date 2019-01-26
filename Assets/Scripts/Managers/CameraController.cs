using UnityEngine;

public class CameraController : MonoBehaviour {
    Vector3 offset;
    public float damping = 1;

    public void Init(int playerId, int numPlayers) {
        Camera camera = gameObject.GetComponent<Camera>();
        if(camera != null) {
            float cameraRectWidth = numPlayers > 2 ? 0.5f : 1f;
            float cameraRectHeight = numPlayers > 1 ? 0.5f : 1f;
            float cameraRectX = 0f;
            float cameraRectY = 0f;
            switch(playerId) {
                case 0:
                    if(numPlayers > 1) {
                        cameraRectY = 0.5f;
                    }
                    break;
                case 1:
                    if(numPlayers > 2) {
                        cameraRectY = 0.5f;
                        cameraRectX = 0.5f;
                    }
                    break;
                case 2:
                    if(numPlayers == 3) {
                        cameraRectX = 0.25f;
                    }
                    break;
                case 3:
                    cameraRectX = 0.5f;
                    break;
            }
            camera.rect = new Rect(cameraRectX, cameraRectY, cameraRectWidth, cameraRectHeight);
            camera.enabled = true;
        }
    }

    void Start() {
        offset = transform.parent.position - transform.position;
    }
    
    void LateUpdate() {
        float currentAngle = transform.eulerAngles.y;
        float desiredAngle = transform.parent.eulerAngles.y;
        float angle = Mathf.LerpAngle(currentAngle, desiredAngle, Time.deltaTime * damping);

        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        transform.position = transform.parent.position - (rotation * offset);

        transform.LookAt(transform.parent);
    }
}