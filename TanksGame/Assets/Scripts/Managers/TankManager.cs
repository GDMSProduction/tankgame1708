﻿using System;
using UnityEngine;

[Serializable]
public class TankManager
{
    // This class is to manage various settings on a tank.
    // It works with the GameManager class to control how the tanks behave
    // and whether or not players have control of their tank in the 
    // different phases of the game.

    public Color m_PlayerColor;                             // This is the color this tank will be tinted.
    public Transform m_SpawnPoint;                          // The position and direction the tank will have when it spawns.
    public int m_PlayerNumber;                              // This specifies which player this the manager for.
    public string m_ColoredPlayerText;                      // A string that represents the player with their number colored to match their tank.
    public GameObject m_Instance;                           // A reference to the instance of the tank when it is created.
    public int m_Wins;                                      // The number of wins this player has so far.
    public string Name = "default";
    //TankColor usercolor = new TankColor();
    //TankColor2 usercolor2 = new TankColor2();
    //TankColor3 usercolor3 = new TankColor3();
    //TankColor4 usercolor4 = new TankColor4();
    public int userchoice;
    //public bool EngineAudio;
    private TankMovement m_Movement;                        // Reference to tank's movement script, used to disable and enable control.
    public TankShooting m_Shooting;                        // Reference to tank's shooting script, used to disable and enable control.
    private GameObject m_CanvasGameObject;                  // Used to disable the world space UI during the Starting and Ending phases of each round.


    public void Setup()
    {

        
        // Get references to the components.
        m_Movement = m_Instance.GetComponent<TankMovement>();
       // m_Movement.audio = EngineAudio;
        m_Shooting = m_Instance.GetComponent<TankShooting>();
        m_CanvasGameObject = m_Instance.GetComponentInChildren<Canvas>().gameObject;

        // Set the player numbers to be consistent across the scripts.
        m_Movement.m_PlayerNumber = m_PlayerNumber;
        m_Shooting.m_PlayerNumber = m_PlayerNumber;

        // Create a string using the correct color that says 'PLAYER 1' etc based on the tank's color and the player's number.
        m_ColoredPlayerText = "PLAYER " + m_PlayerNumber + "";

        // Get all of the renderers of the tank.
        MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer>();


        if (m_PlayerNumber == 1)
        {
            if (MultiScenceData.userchoice != -1)
            {
                m_PlayerColor = MultiScenceData.usercolor1;
            }
            // Go through all the renderers...
            for (int i = 0; i < renderers.Length; i++)
            {
                // ... set their material color to the color specific to this tank.
                renderers[i].material.color = m_PlayerColor;
            }
        }
        else if (m_PlayerNumber == 2)
        {
            if (MultiScenceData.userchoice2 != -1)
            {
                m_PlayerColor = MultiScenceData.usercolor2;
            }
            // Go through all the renderers...
            for (int i = 0; i < renderers.Length; i++)
            {
                // ... set their material color to the color specific to this tank.
                renderers[i].material.color = m_PlayerColor;
            }
        }
        else if (m_PlayerNumber == 3)
        {
            if (MultiScenceData.userchoice3 != -1)
            {
                m_PlayerColor = MultiScenceData.usercolor3;
            }
            // Go through all the renderers...
            for (int i = 0; i < renderers.Length; i++)
            {
                // ... set their material color to the color specific to this tank.
                renderers[i].material.color = m_PlayerColor;
            }
        }
        else if (m_PlayerNumber == 4)
        {
            if (MultiScenceData.userchoice4 != -1)
            {
                m_PlayerColor = MultiScenceData.usercolor4;
            }
            // Go through all the renderers...
            for (int i = 0; i < renderers.Length; i++)
            {
                // ... set their material color to the color specific to this tank.
                renderers[i].material.color = m_PlayerColor;
            }
        }
        else
        {
            // Go through all the renderers...
            for (int i = 0; i < renderers.Length; i++)
            {
                // ... set their material color to the color specific to this tank.
                renderers[i].material.color = m_PlayerColor;
            }
        }
    }


    // Used during the phases of the game where the player shouldn't be able to control their tank.
    public void DisableControl()
    {
        m_Movement.enabled = false;
        m_Shooting.enabled = false;

        m_CanvasGameObject.SetActive(false);
    }


    // Used during the phases of the game where the player should be able to control their tank.
    public void EnableControl()
    {

        m_Movement.enabled = true;
        m_Shooting.enabled = true;

        m_CanvasGameObject.SetActive(true);
    }


    // Used at the start of each round to put the tank into it's default state.
    public void Reset()
    {
        m_Instance.transform.position = m_SpawnPoint.position;
        m_Instance.transform.rotation = m_SpawnPoint.rotation;

        m_Instance.SetActive(false);
        m_Instance.SetActive(true);
    }
}