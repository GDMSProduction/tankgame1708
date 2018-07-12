using UnityEngine;
using UnityEngine.UI;

namespace completed
{
    public class TankShooting : MonoBehaviour
    {
        public int m_PlayerNumber = 1;
        public Rigidbody m_Shell;
        public Transform m_FireTransform;
        public Slider m_AimSlider;
        public AudioSource m_ShootingAudio;
        public AudioClip m_ChargingClip;
        public AudioClip m_FireClip;
        public float m_MinLaunchForce = 15f;
        public float m_MaxLaunchForce = 30f;
        public float m_MaxChargeTime = 0.75f;

        private string m_FireButton;
        private float m_CurrentLaunchForce;
        private float m_ChargeSpeed;
        private bool m_Fired;

        private void OnEnable()
        {
            m_CurrentLaunchForce = this.m_MinLaunchForce;
        }
    }
}