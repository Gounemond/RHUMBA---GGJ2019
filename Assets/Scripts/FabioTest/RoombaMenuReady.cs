using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using DG.Tweening;
using UnityEngine.UI;
using DG.Tweening;

public class RoombaMenuReady : MonoBehaviour
{

    // Callback signature
    // Has: Return of type 'void' and 1 parameter of type 'string'
    public delegate void ActionInt(int playerId);

    public event ActionInt OnRoombaReady;
    public event ActionInt OnGameStart;

    public Image myBottle;

    [Tooltip("Insert here which number this player should be!")]
    public int playerId = 0;     // Rewired player of this controller

    private Player _rewiredPlayer;

    void Awake()
    {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        _rewiredPlayer = ReInput.players.GetPlayer(playerId);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_rewiredPlayer.GetButtonDown("PlayerReady"))
        {
            // TODO: animations
            // TODO: audio
            if (OnRoombaReady != null)
            {
                OnRoombaReady(playerId);
            }
        }

        if (_rewiredPlayer.GetButtonDown("StartGame"))
        {
            // TODO: animations
            // TODO: audio
            if (OnGameStart != null)
            {
                OnGameStart(playerId);
            }
        }
    }
}
