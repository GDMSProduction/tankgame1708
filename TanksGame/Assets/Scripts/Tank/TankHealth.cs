using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class TankHealth : NetworkBehaviour
{
    public float m_StartingHealth = 100f;          
    public Slider m_Slider;                        
    public Image m_FillImage;                      
    public Color m_FullHealthColor = Color.green;  
    public Color m_ZeroHealthColor = Color.red;    
    public GameObject m_ExplosionPrefab;


    private AudioSource m_ExplosionAudio;
    private ParticleSystem m_ExplosionParticles;
    [SerializeField]
    [SyncVar (hook = "OnChangeHealth")] private float m_CurrentHealth;
    private bool m_Dead;


    private void Awake()
    {
        // Instantiate the explosion prefab and get a reference to the particle system on it.
        m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
        // Get a reference to the audio source on the instantiated prefab.
        m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();
        // Disable the prefab so it can be activated when it's required.
        m_ExplosionParticles.gameObject.SetActive(false);
    }


    private void OnEnable()
    {

        // When the tank is enabled, reset the tank's health and whether or not it's dead.
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;
        // Update the health slider's value and color.
        SetHealthUI();
        //testdamage();
    }
    

    public void TakeDamage(float amount)
    {
        if (GameManager.IsOnline)
            if(!isServer)
                return;
    
        // Adjust the tank's current health, update the UI based on the new health and check whether or not the tank is dead.

        // Reduce current health by the amount of damage done.
        m_CurrentHealth -= amount;


        // If the current health is at or below zero and it has not yet been registered, call OnDeath.
        SetHealthUI();

        if (m_CurrentHealth <= 0f && !m_Dead)
        { OnDeath(); }
    }
    
    void OnChangeHealth(float Health)
    {
        m_CurrentHealth = Health;
        SetHealthUI();

        if (m_CurrentHealth <=0f && !m_Dead)
        {   OnDeath();  }
    }

    private void SetHealthUI()
    {
        // Adjust the value and colour of the slider.
        // Set the slider's value appropriately.
        m_Slider.value = m_CurrentHealth;

        // Interpolate the color of the bar between the choosen colours based on the current percentage of the starting health.
        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
    }


    private void OnDeath()
    {
        // Play the effects for the death of the tank and deactivate it.

        // Set the flag so that this function is only called once.
        m_Dead = true;

        // Move the instantiated explosion prefab to the tank's position and turn it on.
        m_ExplosionParticles.transform.position = transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);

        // Play the particle system of the tank exploding.
        m_ExplosionParticles.Play();

        // Play the tank explosion sound effect.
        m_ExplosionAudio.Play();

        // Turn the tank off.
        gameObject.SetActive(false);
    }
}