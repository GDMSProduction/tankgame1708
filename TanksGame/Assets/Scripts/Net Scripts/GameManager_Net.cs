using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[Serializable]
public class GameManager_Net : NetworkBehaviour {

    public int m_NumRoundsToWin = 2;
    public float m_StartDelay = 3f;
    public float m_EndDelay = 3f;
    public static bool drawLeaderboard = false;
    public Text m_MessageText;
    
    private int m_RoundNumber;
    private WaitForSeconds m_StartWait;
    private WaitForSeconds m_EndWait;
    private const string PLAYER_ID_PREFIX = "Player ";
    public static Dictionary<string, GameObject> players = new Dictionary<string, GameObject>();
    private string m_RoundWinner;
    private string m_GameWinner;

    private void OnPlayerDisconnected(NetworkPlayer player)
    {
        foreach (var playerID in players)
        {
            if (Getplayer(playerID.Key) == null)
            {
                players.Remove(playerID.Key);
            }
        }
    }

    private void Start()
    {
        players.Clear();
        m_RoundWinner = null;
        m_GameWinner = null;

        m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);


        //Start Game Loop
        StartCoroutine(GameLoop());        
    }


    private IEnumerator GameLoop()
    {
        // Start off by running the 'RoundStarting' coroutine but don't return until it's finished.
        yield return StartCoroutine(RoundStarting());

        // Once the 'RoundStarting' coroutine is finished, run the 'RoundPlaying' coroutine but don't return until it's finished.
        yield return StartCoroutine(RoundPlaying());

        // Once execution has returned here, run the 'RoundEnding' coroutine, again don't return until it's finished.
        yield return StartCoroutine(RoundEnding());

        // This code is not run until 'RoundEnding' has finished.  At which point, check if a game winner has been found.
        if (m_GameWinner != null)
        {
            //Disconnect Players            
            m_MessageText.text = m_GameWinner + " Wins!";
            SceneManager.LoadScene("OnlineLobby");
        }

        else
        {
            // If there isn't a winner yet, restart this coroutine so the loop continues.
            // Note that this coroutine doesn't yield.  This means that the current version of the GameLoop will end.
            StartCoroutine(GameLoop());
        }
        
    }

    private IEnumerator RoundStarting()
    {
        ResetAllTanks();

        // As soon as the round starts reset the tanks and make sure they can't move.
        DisableTankControl();

         // Increment the round number and display text showing the players what round it is.
        m_RoundNumber++;
        m_MessageText.text = "ROUND " + m_RoundNumber;
        yield return m_StartWait;
    }

    private IEnumerator RoundPlaying()
    {
        EnableTankControl();

        m_MessageText.text = string.Empty;

        while (!OneTankLeft())
        {
            yield return null;
        }
    }

    private IEnumerator RoundEnding()
    {
        DisableTankControl();
        m_RoundWinner = null;

        foreach (var player in players)
        {
            if (Getplayer(player.Key).activeSelf)
            {
                m_RoundWinner = Getplayer(player.Key).name;
                m_MessageText.text = "Winner: " + m_RoundWinner;
                Getplayer(player.Key).GetComponent<TankHealth>().WonRound();

                if (Getplayer(player.Key).GetComponent<TankHealth>().GetWins() == m_NumRoundsToWin)
                {
                    m_GameWinner = Getplayer(player.Key).name;
                }
            }
        }



        yield return m_EndWait;
    }

    private bool OneTankLeft()
    {
        int numTanksLeft = 0;

        if (players.Count > 1)
        {
            foreach (var player in players)
            {
                if (Getplayer(player.Key).activeSelf)
                {
                    numTanksLeft++;
                }
            }
            return numTanksLeft <= 1;
        }
            
        else
            return false;
    }


    private void ResetAllTanks()
    {
        foreach (var player in players)
        {
            GameObject cPlayer = Getplayer(player.Key);
            cPlayer.transform.position = cPlayer.GetComponent<PlayerSetup>().startPos;
            cPlayer.SetActive(false);
            cPlayer.SetActive(true);
        }
    }

    private void EnableTankControl()
    {
        foreach (var player in players)
        {
            Getplayer(player.Key).GetComponent<TankMovement>().enabled = true;
        }
    }

    private void DisableTankControl()
    {
        foreach (var player in players)
        {
            Getplayer(player.Key).GetComponent<TankMovement>().enabled = false;
        }
    }









    private void SetCameraTargets()
    {
        Transform[] targets = new Transform[players.Count];

        for (int i = 0; i < targets.Length; i++)
        {
            //players["Player " + i.ToString()].
        }
    }

    public static void RegisterPlayer(string _netid, GameObject tank_Instance)
    {
       // m_Tanks.a
        players.Add(_netid, tank_Instance);
    }

    public static void UnregisterPlayer(string _PlayerID)
    {
        players.Remove(_PlayerID);
    }

    public static GameObject Getplayer(string _playerID)
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
                GUILayout.Label(_playerID + " - Wins: " + players[_playerID].GetComponent<TankHealth>().GetWins());
            }
            GUILayout.EndArea();
        }
    }
}
