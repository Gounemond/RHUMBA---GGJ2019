using System.Collections;
using System.Collections.Generic;
using System;
using DG.Tweening;
using Rewired;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Tooltip("I roomba nel menu che hanno il check input")]
    public RoombaMenuReady[] roombaPlayer;
    public UIMenuManager menuUIManager;

    public AudioSource sfxROOOOMBAAAH;

    private bool _gameStart = false;

    private void Awake()
    {
        ReInput.ControllerConnectedEvent += ControllerConnected;
        ReInput.ControllerDisconnectedEvent += ControllerDisconnected;
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return StartCoroutine(menuUIManager.FadeIn(1));
        GameData.playerData = new List<PlayerData>();
        for (int i = 0; i < roombaPlayer.Length; i++)
        {
            // Every time a player joins and confirms his participation, we add him in the playerdata with his choice
            roombaPlayer[i].OnRoombaReady += AddPlayerRoomba;
            roombaPlayer[i].OnGameStart += OnGameStart;
        }

        // Wait until all the connected players have choosen and confirmed a microwave
        while (GameData.playerData.Count < roombaPlayer.Length && !_gameStart)
        {
            yield return null;
        }
        sfxROOOOMBAAAH.Play();
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(menuUIManager.FadeOut(1));

        SceneManager.LoadScene(1);
        //LoadScene;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPlayerRoomba(int playerId)
    {
        menuUIManager.ReadyPressed(playerId);
        GameData.playerData.Add(new PlayerData(playerId));
    }

    public void OnGameStart(int playerId)
    {
        _gameStart = true;
    }

    public void ControllerConnected(ControllerStatusChangedEventArgs args)
    {
        //
    }

    public void ControllerDisconnected(ControllerStatusChangedEventArgs args)
    {
        //
    }
}
