using System.Collections;
using System.Collections.Generic;
using System;
using DG.Tweening;
using Rewired;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private Player _rewiredPlayer;

    [Tooltip("I roomba nel menu che hanno il check input")]
    public RoombaMenuReady[] roombaPlayer;

    private void Awake()
    {
        ReInput.ControllerConnectedEvent += ControllerConnected;
        ReInput.ControllerDisconnectedEvent += ControllerDisconnected;
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        GameData.playerData = new List<PlayerData>();
        for (int i = 0; i < roombaPlayer.Length; i++)
        {
            // Every time a player joins and confirms his participation, we add him in the playerdata with his choice
            roombaPlayer[i].OnRoombaReady += AddPlayerRoomba;
        }

        // Wait until all the connected players have choosen and confirmed a microwave
        while (GameData.playerData.Count </* ReInput.controllers.GetJoysticks().Length && GameData.playerData.Count < */2)
        {
            yield return null;
        }
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPlayerRoomba(int playerId)
    {
        GameData.playerData.Add(new PlayerData(playerId));
    }

    public void ControllerConnected(ControllerStatusChangedEventArgs args)
    {
        Debug.Log("Bella");
    }

    public void ControllerDisconnected(ControllerStatusChangedEventArgs args)
    {
        Debug.Log("Addio");
    }
}
