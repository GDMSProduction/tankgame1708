using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCount : MonoBehaviour {

    public Dropdown testDropdown;


    private void Start()
    {
        testDropdown.onValueChanged.AddListener(delegate
        {
            SetUserChoice(testDropdown.value);
        });
    }

    private void Update()
    {

    }

    public void SetUserChoice(int _choice)
    {
        MultiScenceData.playercount = _choice;
    }


}
