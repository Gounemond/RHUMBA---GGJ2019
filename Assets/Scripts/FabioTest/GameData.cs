using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameData : MonoBehaviour
{
    public static List<PlayerData> playerData;
}

[Serializable]
public class PlayerData : IComparable<PlayerData>
{
    public int playerId;                 // ID of the player to use with Rewired
    public float tilesCleanedPercentage; // Percentage of the tiles cleaned by the player
    public RoombaGraphics graphics;      // Graphic elements of the different roombas

    /// <summary>
    /// We assume that a new player has not yet cleaned any tiles :)
    /// </summary>
    /// <param name="newPlayerId"></param>
    public PlayerData(int newPlayerId)
    {
        playerId = newPlayerId;
        tilesCleanedPercentage = 0f;
    }

    //This method is required by the IComparable
    //interface. 
    // Let's... compare on ID for now
    public int CompareTo(PlayerData other)
    {
        if (other == null)
        {
            return 1;
        }

        // Elements get ordered naturally based on playerId
        return (int)(playerId - other.playerId);
    }
}