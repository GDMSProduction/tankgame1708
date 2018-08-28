using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[RequireComponent(typeof(TankHealth))]
public class PlayerSetup : NetworkBehaviour
{



    [SerializeField]
    public Text m_PlayerNameDisplay;
    [SerializeField]
    Behaviour[] ToDisable;
    Camera sceneCamera;
    public Vector3 startPos;

    [SerializeField]
    string remotelayername;

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
        {
            Disablescript();
            Assignlayer();
        }
        else
        {

            sceneCamera = Camera.main;
            if (sceneCamera != null)
                sceneCamera.gameObject.SetActive(false);
        }

        string _netid = GetComponent<NetworkIdentity>().netId.ToString();
        GameObject _player = GameObject.Find("OnlineTank(Clone)");
        GameManager_Net.RegisterPlayer(_netid, _player);
        SetPlayerName();
        startPos = gameObject.transform.position;
    }


    public override void OnStartClient()
    {
        base.OnStartClient();
    }

    private void SetPlayerName()
    {
        m_PlayerNameDisplay.text = "Player " + GetComponent<NetworkIdentity>().netId.ToString();
    }

    void Assignlayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remotelayername);
    }

    void Disablescript()
    {
        for (int i = 0; i < ToDisable.Length; i++)
        {
            ToDisable[i].enabled = false;
        }

    }

    private void OnDisable()
    {
        if (sceneCamera != null)
            sceneCamera.gameObject.SetActive(true);

        //GameManager_Net.UnregisterPlayer(transform.name);
    }

    private void OnEnable()
    {
        if (isLocalPlayer)
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
                sceneCamera.gameObject.SetActive(false);
        }
    }

}
