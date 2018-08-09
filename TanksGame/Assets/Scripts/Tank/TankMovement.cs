using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class TankMovement : NetworkBehaviour
{
    public int m_PlayerNumber = 1;         
    public float m_Speed = 12f;            
    public float m_TurnSpeed = 180f;       
    public AudioSource m_MovementAudio;    
    public AudioClip m_EngineIdling;       
    public AudioClip m_EngineDriving;      
    public float m_PitchRange = 0.2f;
    
    private string m_MovementAxisName;     
    private string m_TurnAxisName;
    private string m_TurretAxis;
    private Rigidbody m_Rigidbody;         
    private float m_MovementInputValue;    
    private float m_TurnInputValue;        
    private float m_OriginalPitch;
    private float m_Turretinputvalue;
    private GameObject m_turret;
    


    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        //m_turret = this.gameObject.transform.GetChild(0).GetChild(3).gameObject;
        m_turret = m_Rigidbody.transform.Find("TankRenderers").Find("TankTurret").gameObject;
    }


    private void OnEnable ()
    {
        m_Rigidbody.isKinematic = false;
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
        m_Turretinputvalue = 0f;
    }


    private void OnDisable ()
    {
        m_Rigidbody.isKinematic = true;
    }


    private void Start()
    {
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;
        m_TurretAxis = "TurretMovement" + m_PlayerNumber;
        m_OriginalPitch = m_MovementAudio.pitch;

        if (GameManager.IsSinglePlayer && m_PlayerNumber != 1)
        {
            m_MovementAxisName = "Enemy";
            m_TurnAxisName = "Enemy";
            m_TurretAxis = "Enemy";
        }
    }

    private void Update()
    {
        if (GameManager.IsOnline)
            if (!isLocalPlayer) { return; }

        m_MovementInputValue = CrossPlatformInputManager.GetAxis(m_MovementAxisName);
        m_TurnInputValue = CrossPlatformInputManager.GetAxis(m_TurnAxisName);
        m_Turretinputvalue = CrossPlatformInputManager.GetAxis(m_TurretAxis);
        EngineAudio();
    }

    private void EngineAudio()
    {
        if (Mathf.Abs(m_MovementInputValue) < 0.1f && Mathf.Abs(m_TurnInputValue) < 0.1f)
        {
            if (m_MovementAudio.clip == m_EngineDriving)
            {
                m_MovementAudio.clip = m_EngineIdling;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }

        else
        {
            if (m_MovementAudio.clip == m_EngineIdling)
            {
                m_MovementAudio.clip = m_EngineDriving;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
    }


    private void FixedUpdate()
    {
        // Move and turn the tank.
        Move();
        Turn();
        TurnTurret();
    }


    private void Move()
    {
        Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }

    private void Turn()
    {
        // Adjust the rotation of the tank based on the player's input.
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }

   private void TurnTurret()
    {
       float turret = m_Turretinputvalue * m_TurnSpeed * Time.deltaTime;
       Quaternion turnRotation = Quaternion.Euler(0f, turret, 0f);
       //m_turret.MoveRotation(m_turret.rotation * turnRotation);
       m_turret.transform.rotation = m_turret.transform.rotation * turnRotation;
       //m_turret.transform.SetPositionAndRotation(m_Rigidbody.position, m_turret.transform.rotation * turnRotation);
    }
};