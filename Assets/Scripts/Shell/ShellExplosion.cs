using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    /*
    public LayerMask m_TankMask;
    public ParticleSystem m_ExplosionParticles;       
    public AudioSource m_ExplosionAudio;              
    public float m_MaxDamage = 100f;                  
    public float m_ExplosionForce = 1000f;            
    public float m_MaxLifeTime = 2f;                  
    public float m_ExplosionRadius = 5f;              


    private void Start()
    {
        Destroy(gameObject, m_MaxLifeTime);
    }
    */

    [SerializeField] private ParticleSystem explosionParticles;

    [SerializeField] private int damage = 100;

    [SerializeField] private float timeToLive;
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= timeToLive)
        {
            OnDeath();
        }
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Enemy"))
        {
            // find AIHealthSystem
            AIHealthSystem aIHealthSystem = other.transform.GetComponent<AIHealthSystem>();

            if (aIHealthSystem)
            {
                aIHealthSystem.TakeDamage(damage);
            }
        }

        OnDeath();
        /*
        // collect all the collider in a sphere from the shell's current position
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, tankMask);

        //Debug.Log(colliders.Length);

        if(colliders.Length > 0)
        {
            OnDeath();
            Debug.Log(colliders.Length);
        }

        for(int i = 0; i < colliders.Length; i++)
        {
            
            // find rigidbody
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

            // if dont have a rigidbody, go on to the next collider
            if (!targetRigidbody)
            {
                continue;
            }

            // add an explosion force
            //targetRigidbody.AddExplosionForce(explosionForce, transform.position, explosionRadius);

            // find tankhealth script
            TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();

            // if there is no TankHealth script, go on the next collider
            if (!targetHealth)
            {
                continue;
            }
            //Debug.Log(1123);

            // calculator a mount of damage the target should take based on it's distance from the shell
            float damage = CalculateDamage(targetRigidbody.position);

            Debug.Log(damage);
            // deal this damage to the tank
            targetHealth.TakeDamage(damage);

            
        }

        */
    }

    /*
    private float CalculateDamage(Vector3 targetPosition)
    {
        // Calculate the amount of damage a target should take based on it's position.

        // create a vector from the shell to the target
        Vector3 explosionToTarget = targetPosition - transform.position;

        // calculator the distance from the shell to the target
        float explosionDistance = explosionToTarget.magnitude;

        // calculator the proportion of the maximom distance
        float relativeDistance = (explosionRadius - explosionDistance) / explosionRadius;

        // calculator damage as this proportion of the maximum possible damage
        float damage = relativeDistance * maxDamage;

        // make sure the minimum damage is always 0
        damage = Mathf.Max(0f, damage);

        return damage;
    }
    */
    private void OnDeath()
    {
        // unparent the particles from the shell
        explosionParticles.transform.parent = null;

        // play the particle system
        explosionParticles.Play();

        // destroy the particles have finished
        ParticleSystem.MainModule mainModule = explosionParticles.main;
        Destroy(explosionParticles.gameObject, mainModule.duration);

        // destroy the shell
        Destroy(gameObject);
    }
}