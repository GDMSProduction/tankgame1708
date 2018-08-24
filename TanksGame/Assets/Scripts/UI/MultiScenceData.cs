using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MultiScenceData {

    public static int userchioce;
    public static void GetColorChoice(int _userchoice)
    {
        userchioce = _userchoice;
    }
}
