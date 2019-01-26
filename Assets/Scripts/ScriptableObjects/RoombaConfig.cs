using UnityEngine;

[CreateAssetMenu(menuName = "SO Configs/" + nameof(RoombaConfig), fileName = nameof(RoombaConfig))]
public class RoombaConfig : ScriptableObject {
    public GameObject prefab;
    public RoombaInputMode inputMode;
    public float baseMoveSpeed;
    public float baseTurnSpeed;
}