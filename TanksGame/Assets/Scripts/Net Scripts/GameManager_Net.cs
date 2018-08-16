using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;

[Serializable]
public class GameManager_Net : NetworkBehaviour {

    public int m_NumRoundsToWin = 5;
    public float m_StartDelay = 3f;
    public float m_EndDelay = 3f;
    public static bool drawLeaderboard = false;
    public Text m_MessageText;

    private TankManager[] m_Tanks;
    private int m_RoundNumber;
    private WaitForSeconds m_StartWait;
    private WaitForSeconds m_EndWait;
    private const string PLAYER_ID_PREFIX = "Player ";
    private static Dictionary<string, TankHealth> players = new Dictionary<string, TankHealth>();
    private string m_RoundWinner;
    private string m_GameWinner;


    private void Start()
    {
        m_RoundWinner = null;
        m_GameWinner = null;

        SpawnAllTanks();
        SetCameraTargets();

        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);
        //StartCoroutine(GameLoop());
        //Start Game Loop
    }

    public void Update()
    {
        if (players.Count == 0)
        {


        }
    }
    public static int limit()
    {
        return players.Count;
    }

    private void SpawnAllTanks()
    {

    }

    private void SetCameraTargets()
    {
        Transform[] targets = new Transform[players.Count];

        for (int i = 0; i < targets.Length; i++)
        {
            //players["Player " + i.ToString()].
        }
    }

    public static void RegisterPlayer(string _netid, TankHealth _player)
    {
       // m_Tanks.a
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
