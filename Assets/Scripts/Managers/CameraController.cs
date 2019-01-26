using UnityEngine;

public class CameraController : MonoBehaviour {
    Vector3 offset;
    public float damping = 1;
    
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