﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

[Serializable]
public class GameManager_Net : MonoBehaviour {

    private const string PLAYER_ID_PREFIX = "Player ";

    private static Dictionary<string, TankHealth> players = new Dictionary<string, TankHealth>();

    public TankManager[] m_PlayerInstances;
    public static bool drawLeaderboard = false;


    public static void RegisterPlayer(string _netid, TankHealth _player)
    {
        string _playerID = PLAYER_ID_PREFIX + _netid;
        players.Add(_playerID, _player);
        _player.transform.name = _playerID;
    }

    public static void UnregisterPlayer(string _PlayerID)
    {
        players.Remove(_PlayerID);
    }

    public static TankHealth Getplayer(string _playerID)
    {
        return players[_playerID];
    }
    private void OnGUI()
    {
        if (drawLeaderboard)
        {
            GUILayout.BeginArea(new Rect(200, 200, 200, 500));
            GUILayout.BeginVertical();
            foreach (string _playerID in players.Keys)
            {
                GUILayout.Label(_playerID + " - " + players[_playerID].transform.name);
            }
            GUILayout.EndArea();
        }
    }
}
