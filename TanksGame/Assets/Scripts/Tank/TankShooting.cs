using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
using UnityEngine.Networking;

public class TankShooting : NetworkBehaviour
{
    public int m_PlayerNumber = 1;       
    public GameObject m_Shell;            
    public Transform m_FireTransform;    
    public Slider m_AimSlider;           
    public AudioSource m_ShootingAudio;  
    public AudioClip m_ChargingClip;     
    public AudioClip m_FireClip;         
    public float m_MinLaunchForce = 15f; 
    public float m_MaxLaunchForce = 30f; 
    public float m_MaxChargeTime = 0.75f;
    public float m_FireDelay = 0.5f;//delay value

    private string m_FireButton;         
    private float m_CurrentLaunchForce;  
    private float m_ChargeSpeed;         
    private bool m_Fired;
   // private WaitForSeconds m_Delay;//delay
    private float start;

    private void OnEnable()
    {
        // When the tank is turned on, reset the launch force and the UI
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
        //m_Delay = new WaitForSeconds(m_FireDelay);
    }


    private void Start()
    {
        
        // The fire axis is based on the player number.
        m_FireButton = "Fire" + m_PlayerNumber;

        // The rate that the launch force charges up is the range of possible forces by the max charge time.
        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;

        if (GameManager.IsSinglePlayer && m_PlayerNumber != 1) {  m_FireButton = "Enemy"; }
    }


    private void Update()
    {
        if (!isLocalPlayer) { return; }

        // The slider should have a default value of the minimum launch force.
        m_AimSlider.value = m_MinLaunchForce;
            
        // If the max force has been exceeded and the shell hasn't yet been launched...
        if (m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired)
        {
            // ... use the max force and launch the shell.
            m_CurrentLaunchForce = m_MaxLaunchForce;
            Fire();
        }

        // Otherwise, if the fire button has just started being pressed...
        //else if (Input.GetButtonDown(m_FireButton))
        else if (CrossPlatformInputManager.GetButtonDown(m_FireButton))
        {
            if (Time.realtimeSinceStartup >= start + m_FireDelay)
            {
                // ... reset the fired flag and reset the launch force.
                m_Fired = false;
                m_CurrentLaunchForce = m_MinLaunchForce;

                // Change the clip to the charging clip and start it playing.
                m_ShootingAudio.clip = m_ChargingClip;
                m_ShootingAudio.Play();
                Debug.Log("fire");
            }
            else
            {
                Debug.Log("delay happened");
            }
        }

        // Otherwise, if the fire button is being held and the shell hasn't been launched yet...
        //else if (Input.GetButton(m_FireButton) && !m_Fired)
        else if (CrossPlatformInputManager.GetButton(m_FireButton) && !m_Fired)
        {
            // Increment the launch force and update the slider.
            m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;

            m_AimSlider.value = m_CurrentLaunchForce;
        }

        // Otherwise, if the fire button is released and the shell hasn't been launched yet...
        //else if (Input.GetButtonUp(m_FireButton) && !m_Fired)
        else if (CrossPlatformInputManager.GetButtonUp(m_FireButton) && !m_Fired)
        {
              
                start = Time.realtimeSinceStartup;
                // ... launch the shell.
                Fire();
                
               
        }
    }
    
    [Command]
    void CmdFire()
    {
        // Set the fired flag so only Fire is only called once.
        m_Fired = true;

        // Create an instance of the shell and store a reference to it's rigidbody.
        GameObject shellInstance =
            (GameObject)Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation);

        // Set the shell's velocity to the launch force in the fire position's forward direction.
        shellInstance.GetComponent<Rigidbody>().velocity = m_CurrentLaunchForce * m_FireTransform.forward;
     
        // Spawn the shell on the clients
        NetworkServer.Spawn(shellInstance);

        // Change the clip to the firing clip and play it.
        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();

        // Reset the launch force.  This is a precaution in case of missing button events.
        m_CurrentLaunchForce = m_MinLaunchForce;
    
    }

    private void Fire()
    {
        CmdFire();
    }
}