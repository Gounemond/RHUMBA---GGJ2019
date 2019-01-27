using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TilesManager : MonoBehaviour
{
    public int player0Tiles;
    public int player1Tiles;
    public int player2Tiles;
    public int player3Tiles;
    public int dirtyTiles;
    public int totalTiles;

    public Text player0Score;
    public Text player1Score;
    public Text player2Score;
    public Text player3Score;
    void Start()
    {
        player0Tiles = 0;
        player1Tiles = 0;
        player2Tiles = 0;
        player3Tiles = 0;
        totalTiles = GameObject.FindGameObjectsWithTag("Tile").Length;
    }

    void Update()  {}

    public void UpdateTilesCOunt(int oldcolour, int newcolour)
    {
        switch (oldcolour)
        {
            case 0:
                player0Tiles--;
                break;
            case 1:
                player1Tiles--;
                break;
            case 2:
                player2Tiles--;
                break;
            case 3:
                player3Tiles--;
                break;
            case 4:
                dirtyTiles--;
                break;
        }

        switch (newcolour)
        {
            case 0:
                player0Tiles++;
                break;
            case 1:
                player1Tiles++;
                break;
            case 2:
                player2Tiles++;
                break;
            case 3:
                player3Tiles++;
                break;
            case 4:
                dirtyTiles++;
                break;
        }

        if(GameData.playerData.Any(pd => pd.playerId == 0))
            player0Score.text = (player0Tiles * 100 / totalTiles).ToString("0\\%");
        if(GameData.playerData.Any(pd => pd.playerId == 1))
            player1Score.text = (player1Tiles * 100 / totalTiles).ToString("0\\%");
        if(GameData.playerData.Any(pd => pd.playerId == 2))
            player2Score.text = (player2Tiles * 100 / totalTiles).ToString("0\\%");
        if(GameData.playerData.Any(pd => pd.playerId == 3))
            player3Score.text = (player3Tiles * 100 / totalTiles).ToString("0\\%");
        
        return;
    }

    public void SaveFinalScore() {
        foreach(PlayerData pd in GameData.playerData) {
            switch(pd.playerId) {
                case 0:
                    pd.tilesCleanedPercentage = (float) player0Tiles / totalTiles;
                    break;
                case 1:
                    pd.tilesCleanedPercentage = (float) player1Tiles / totalTiles;
                    break;
                case 2:
                    pd.tilesCleanedPercentage = (float) player2Tiles / totalTiles;
                    break;
                case 3:
                    pd.tilesCleanedPercentage = (float) player3Tiles / totalTiles;
                    break;
            }
        }
    }
}
