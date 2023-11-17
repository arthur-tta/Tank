using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    /*
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
    */
    /*
    private string m_FireButton;         
    private float m_CurrentLaunchForce;  
    private float m_ChargeSpeed;         
    private bool m_Fired;                


    private void OnEnable()
    {
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
    }


    private void Start()
    {
        m_FireButton = "Fire" + m_PlayerNumber;

        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
    }
    */
    //[SerializeField] private float minLaunchForce;
    //[SerializeField] private float maxLaunchForce;

    //private float currentLaunchForce;

    //private bool fired;

    [Header("Attack Parameters")]
    [SerializeField] private float cooldownFire;
    private float cooldownFireTimer;

    [SerializeField] Rigidbody shell;
    [SerializeField] private Transform fireTranform;
    [SerializeField] private float launchForce;

    // auto attack
    private bool autoFire;


    private void Update()
    {
        // Track the current state of the fire button and make decisions based on the current launch force.

        cooldownFireTimer += Time.deltaTime;

        // attack normal
        if ((Input.GetButtonDown("Fire1") || autoFire) && cooldownFireTimer >= cooldownFire)
        {
            Fire();
        }


        // auto attack
        if (Input.GetKeyDown(KeyCode.F))
        {
            autoFire = !autoFire;
        }
            
    }


    private void Fire()
    {
        // Instantiate and launch the shell.
        cooldownFireTimer = 0;
        //Debug.Log("fire");

        Rigidbody shellInstance = Instantiate(shell, fireTranform.position, fireTranform.rotation) as Rigidbody;

        shellInstance.velocity = launchForce * fireTranform.forward;
    }
}