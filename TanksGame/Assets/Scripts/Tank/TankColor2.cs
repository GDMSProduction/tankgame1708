using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankColor2 : MonoBehaviour
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

      //  GetColor();
    }
    private void Update()
    {
        GetColor();
    }
    public void SetUserChoice(int _choice)
    {
        MultiScenceData.userchioce2 = _choice;
    }

    public void GetColor()
    {
        int _userchoice = MultiScenceData.userchioce2;

        if (_userchoice == 0)
        {

             MultiScenceData.usercolor2 = Color.red;
        }
        else if (_userchoice == 1)
        {

             MultiScenceData.usercolor2 = Color.black;
        }
        else if (_userchoice == 2)
        {

             MultiScenceData.usercolor2 = Color.blue;
        }
        else if (_userchoice == 3)
        {

             MultiScenceData.usercolor2 = Color.green;
        }
        else if (_userchoice == 4)
        {

            Color color = new Color(160.0f / 256.0f, 32.0f / 256.0f, 240.0f / 256.0f);
             MultiScenceData.usercolor2 = color;
        }
        else
        {
             MultiScenceData.usercolor2 = Color.white;
        }
    }
}
