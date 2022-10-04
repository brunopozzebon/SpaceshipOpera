using System.Collections;
using UnityEngine;

public class ShootScript : MonoBehaviour
{

    private bool alreadyShoot;
    private Rigidbody rigidbody;
    private MeshRenderer meshRenderer;
    private void Start()
    {
        AudioSource shootSound = GetComponent<AudioSource>();
        
        shootSound.Play();
        rigidbody = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        StartCoroutine(SelfDestruct());
    }
    
    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter(Collider collider)
    {
       
        if (collider.gameObject.CompareTag("Enemy") && !alreadyShoot)
        {
            EnemyController enemy = collider.gameObject.GetComponent<EnemyController>();
            enemy.receiveDamage();

            alreadyShoot = true;
            rigidbody.velocity = new Vector3(0, 0, 0);
            meshRenderer.enabled = false;

            if (transform.childCount >= 1)
            {
                ParticleSystem particles = transform.GetChild(0).GetComponent <ParticleSystem>();
                particles.transform.position = collider.transform.position;
            
                particles.Play();
            }
            
          
            StartCoroutine(deleteShoot());
        }
    }

    IEnumerator deleteShoot()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
