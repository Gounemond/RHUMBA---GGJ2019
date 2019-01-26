using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Rewired;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private Player _rewiredPlayer;

    [Tooltip("I roomba nel menu che hanno il check input")]
    public RoombaMenuReady[] roombaPlayer;

    // Start is called before the first frame update
    IEnumerator Start()
    {

        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
