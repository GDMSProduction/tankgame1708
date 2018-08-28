﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankColor3 : MonoBehaviour
{



    //color by hexadecimal

    //    blue-2A64B2

    //red-E52E28

    //purple-A400FF

    //L green-00FF17
    //D green-1D9A28
    //int userchoice =0;

    public Dropdown testDropdown;


    private void Start()
    {
        testDropdown.onValueChanged.AddListener(delegate
        {
            SetUserChoice(testDropdown.value);
        });
    }
    public void SetUserChoice(int _choice)
    {
        MultiScenceData.userchioce3 = _choice;
    }

    public Color GetColor()
    {
        int _userchoice = MultiScenceData.userchioce3;

        if (_userchoice == 0)
        {
            Color color = new Color(160.0f / 256.0f, 32.0f / 256.0f, 240.0f / 256.0f);
            return color;
            
        }
        else if (_userchoice == 1)
        {

            return Color.black;
        }
        else if (_userchoice == 2)
        {

            return Color.red;
        }
        else if (_userchoice == 3)
        {

            return Color.green;
        }
        else if (_userchoice == 4)
        {

            return Color.blue;
        }
        else
        {
            return Color.white;
        }
    }
}
