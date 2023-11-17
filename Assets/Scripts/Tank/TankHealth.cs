using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TankHealth : MonoBehaviour
{
    /*
    public float m_StartingHealth = 100f;          
    public Slider m_Slider;                        
    public Image m_FillImage;                      
    public Color m_FullHealthColor = Color.green;  
    public Color m_ZeroHealthColor = Color.red;    
    public GameObject m_ExplosionPrefab;
    */
    /*
    private AudioSource m_ExplosionAudio;          
    private ParticleSystem m_ExplosionParticles;   
    private float m_CurrentHealth;  
    private bool m_Dead;            


    private void Awake()
    {
        m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
        m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>();

        m_ExplosionParticles.gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;

        SetHealthUI();
    }
    */
    //[SerializeField] private int h


    [SerializeField] private TankHealthBar tankHealthBar;
    //[SerializeField] private TextMeshProUGUI heartTxt;

    //[SerializeField] private ParticleSystem Ex

    private void OnEnable()
    {
        tankHealthBar.Init(1000);
    }


    public void TakeDamage(float amount)
    {
        if(tankHealthBar.TakeDamage((int)amount)){
            OnDeath();
        }
    }

    private void OnDeath()
    {
        Debug.Log("OnDeath");
    }
}