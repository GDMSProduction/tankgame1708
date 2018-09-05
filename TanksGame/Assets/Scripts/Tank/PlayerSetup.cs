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
    public string playerName;
    public string NetID;
    public Color playerColor;

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

        NetID = "Player" + GetComponent<NetworkIdentity>().netId.ToString();
        GameObject _player = GameObject.Find("OnlineTank(Clone)");
        GameManager_Net.RegisterPlayer(NetID, _player);
        startPos = gameObject.transform.position;
        _player.name = playerName;
        m_PlayerNameDisplay.text = _player.name;




        /*
        MeshRenderer[] renderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < renderers.Length; i++)
        {
            // ... set their material color to the color specific to this tank.
            renderers[i].material.color = playerColor;
        }
        */
    }


    public override void OnStartClient()
    {
        base.OnStartClient();
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
