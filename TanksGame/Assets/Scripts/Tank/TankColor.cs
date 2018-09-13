using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TankColor : MonoBehaviour {
    
    [SerializeField]
    public GameObject P1;


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

        testDropdown.value = MultiScenceData.userchoice;
        testDropdown.RefreshShownValue();
        // GetColor();
    }

    private void Update()
    {
        testDropdown.RefreshShownValue();

        GetColor();
        UpdateMMTanks();

        
    }

    void UpdateMMTanks()
    {
        MeshRenderer[] renderers = P1.GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = MultiScenceData.usercolor1;
        }
    }

    public void SetUserChoice(int _choice)
    {
        MultiScenceData.userchoice = _choice;
    }

    public void GetColor()
    {
        int _userchoice = MultiScenceData.userchoice;

        if (_userchoice == 0)
        {
            
             MultiScenceData.usercolor1 = Color.blue;
        }
        else if (_userchoice == 1)
        {

             MultiScenceData.usercolor1 = Color.black;
        }
        else if (_userchoice == 2)
        {

             MultiScenceData.usercolor1 = Color.red;
        }
        else if (_userchoice == 3)
        {

             MultiScenceData.usercolor1 = Color.green;
        }
        else if(_userchoice ==4)
        {

           Color color = new Color(160.0f/256.0f, 32.0f/256.0f, 240.0f/256.0f);
             MultiScenceData.usercolor1 = color;
        }
        else
        {
             MultiScenceData.usercolor1 = Color.white;
        }
    }
}
