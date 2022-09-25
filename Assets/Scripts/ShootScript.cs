using System.Collections;
using UnityEngine;

public class ShootScript : MonoBehaviour
{

    private void Start()
    {
        AudioSource shootSound = GetComponent<AudioSource>();
        shootSound.Play();
        
        StartCoroutine(SelfDestruct());
    }
    
    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter(Collider collider)
    {
       
        if (collider.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = collider.gameObject.GetComponent<EnemyController>();
            enemy.receiveDamage();
            Destroy(gameObject);
        }
       
    }
}
