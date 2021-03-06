﻿using UnityEngine;
using UnityEngine.Networking;

public class ShellExplosion : NetworkBehaviour
{
    public LayerMask m_TankMask;
    public LayerMask m_RemoteTankMask;
    public ParticleSystem m_ExplosionParticles;       
    public AudioSource m_ExplosionAudio;              
    public float m_MaxDamage = 100f;                  
    public float m_ExplosionForce = 1000f;            
    public float m_MaxLifeTime = 2f;                  
    public float m_ExplosionRadius = 5f;


    private const string PLAYER_TAG = "Player";

    private void Start()
    {
        // If it isn't destroyed by then, destroy the shell after it's lifetime.
        Destroy(gameObject, m_MaxLifeTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        // Find all the tanks in an area around the shell and damage them.

        // Collect all the colliders in a sphere from the shell's current position to a radius of the explosion radius.
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_TankMask);
        Collider[] remotecolliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_RemoteTankMask);
        // Go through all the colliders...
        for (int i = 0; i < colliders.Length; i++)
        {
            // ... and find their rigidbody.
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

            // If they don't have a rigidbody, go on to the next collider.
            if (!targetRigidbody)
                continue;

            // Add an explosion force.
            targetRigidbody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);

            // Find the TankHealth script associated with the rigidbody.
            TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();

            // If there is no TankHealth script attached to the gameobject, go on to the next collider.
            if (!targetHealth)
                continue;


            
            // Calculate the amount of damage the target should take based on it's distance from the shell.
            float damage = CalculateDamage(targetRigidbody.position);

            // Deal this damage to the tank.

            //gets playerID and appends damage to them
            if (colliders[i].GetComponent<Collider>().tag == PLAYER_TAG)
            {
                if (!isServer)
                    //remotecolliders[i].GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
                    targetHealth.CmdTakeDamage(colliders[i].GetComponent<PlayerSetup>().NetID, damage);
                //remotecolliders[i].gameObject.GetComponent<NetworkIdentity>().RemoveClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
                else
                    targetHealth.RpcTakeDamage(colliders[i].GetComponent<PlayerSetup>().NetID, damage);
            }
            else
            {
                targetHealth.TakeDamage(damage);
            }

            
        }

        for (int i = 0; i < remotecolliders.Length; i++)
        {
            // ... and find their rigidbody.
            Rigidbody targetRigidbody = remotecolliders[i].GetComponent<Rigidbody>();

            // If they don't have a rigidbody, go on to the next collider.
            if (!targetRigidbody)
                continue;

            // Add an explosion force.
            targetRigidbody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius);

            // Find the TankHealth script associated with the rigidbody.
            TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();
           //TankHealth targetHealth;
            // If there is no TankHealth script attached to the gameobject, go on to the next collider.
            if (!targetHealth)
                continue;



            // Calculate the amount of damage the target should take based on it's distance from the shell.
            float damage = CalculateDamage(targetRigidbody.position);

            // Deal this damage to the tank.

            //gets playerID and appends damage to them
            if (remotecolliders[i].GetComponent<Collider>().tag == PLAYER_TAG)
            {
                if(!isServer)
                //remotecolliders[i].GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
                targetHealth.CmdTakeDamage(remotecolliders[i].GetComponent<PlayerSetup>().NetID, damage);
                //remotecolliders[i].gameObject.GetComponent<NetworkIdentity>().RemoveClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
                else
                targetHealth.RpcTakeDamage(remotecolliders[i].GetComponent<PlayerSetup>().NetID, damage);
            }
            else
            {
                targetHealth.TakeDamage(damage);
            }

        }
        if (GameManager.IsOnline)
        {
            if (!isServer)
            {
                // Unparent the particles from the shell.
                m_ExplosionParticles.transform.parent = null;

                // Play the particle system.
                m_ExplosionParticles.Play();

                // Play the explosion sound effect.
                m_ExplosionAudio.Play();

                // Once the particles have finished, destroy the gameobject they are on.
#pragma warning disable CS0618 // Type or member is obsolete
                Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);
#pragma warning restore CS0618 // Type or member is obsolete

                // Destroy the shell.
                Destroy(gameObject);
            }
            else
                RpcShellstuff();
        }
        else
        {
            // Unparent the particles from the shell.
            m_ExplosionParticles.transform.parent = null;

            // Play the particle system.
            m_ExplosionParticles.Play();

            // Play the explosion sound effect.
            m_ExplosionAudio.Play();

            // Once the particles have finished, destroy the gameobject they are on.
#pragma warning disable CS0618 // Type or member is obsolete
            Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);
#pragma warning restore CS0618 // Type or member is obsolete

            // Destroy the shell.
            Destroy(gameObject);
        }
    }

    [Command]
    void CmdShellstuff()
    {
        RpcShellstuff();
    }



    [ClientRpc]
    void RpcShellstuff()
    {
        // Unparent the particles from the shell.
        m_ExplosionParticles.transform.parent = null;

        // Play the particle system.
        m_ExplosionParticles.Play();

        // Play the explosion sound effect.
        m_ExplosionAudio.Play();
            
        // Once the particles have finished, destroy the gameobject they are on.
#pragma warning disable CS0618 // Type or member is obsolete
        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);
#pragma warning restore CS0618 // Type or member is obsolete

        // Destroy the shell.
        Destroy(gameObject);
    }


    private float CalculateDamage(Vector3 targetPosition)
    {
        // Calculate the amount of damage a target should take based on it's position.

        // Create a vector from the shell to the target.
        Vector3 explosionToTarget = targetPosition - transform.position;

        // Calculate the distance from the shell to the target.
        float explosionDistance = explosionToTarget.magnitude;

        // Calculate the proportion of the maximum distance (the explosionRadius) the target is away.
        float relativeDistance = (m_ExplosionRadius - explosionDistance) / m_ExplosionRadius;

        // Calculate damage as this proportion of the maximum possible damage.
        float damage = relativeDistance * m_MaxDamage;

        // Make sure that the minimum damage is always 0.
        damage = Mathf.Max(0f, damage);

        return damage;
    }
}