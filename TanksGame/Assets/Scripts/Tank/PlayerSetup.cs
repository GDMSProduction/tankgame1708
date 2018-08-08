using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

    [SerializeField]
    Behaviour[] ToDisable;
    Camera sceneCamera;

	// Use this for initialization
	void Start ()
    {
        if (!isLocalPlayer)
        {
            for (int i = 0; i < ToDisable.Length; i++)
            {
                ToDisable[i].enabled = false;
            }
        }
        else
        {
            sceneCamera = Camera.main;
            if (sceneCamera != null)
            sceneCamera.gameObject.SetActive(false);
        }
	}

    private void OnDisable()
    {
        if (sceneCamera != null)
            sceneCamera.gameObject.SetActive(true);
    }
}
