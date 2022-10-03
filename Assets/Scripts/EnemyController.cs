using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public KillsController killsController;
    public Transform playerPosition;
    public Transform shootingPointPosition;
    public GameObject shootObject;
    public GameObject avatar;
    private EnemyAnimationController avatarAnimation;
    private AudioSource deathEffect;
    
    private NavMeshAgent ai;
    public float minShootingDistance = 100f;
   
    private Rigidbody rigidBody;
    private bool isDeath = false;

    void Start()
    {
        ai = gameObject.GetComponent<NavMeshAgent>();
        rigidBody = gameObject.GetComponent<Rigidbody>();
        deathEffect = GetComponent<AudioSource>();
        avatarAnimation = avatar.GetComponent<EnemyAnimationController>();
        StartCoroutine(shoot());
    }

    void Update()
    {
        ai.SetDestination(playerPosition.position);
    }

    IEnumerator shoot()
    {
        while (!isDeath)
        {
            if (
                Vector3.Distance( playerPosition.transform.position, transform.position)< minShootingDistance)
            {
                GameObject shootClone = Instantiate(shootObject, shootingPointPosition.position, shootingPointPosition.rotation);
        
                Rigidbody shootRigidBody = shootClone.GetComponent<Rigidbody>();
                shootClone.AddComponent<EnemyShootScript>();


                Vector3 shootForce = playerPosition.position - rigidBody.position;
                shootForce.y = 0;
                shootRigidBody.AddForce(shootForce * 70);
            }
            yield return new WaitForSeconds(2f);
        }
    }
    
    public void receiveDamage()
    {
        if (!isDeath)
        {
            RickSpeakingScript.playAudioWhenKill();
            killsController.addKill(); 
            avatarAnimation.death();
            isDeath = true;
            deathEffect.Play();
            StartCoroutine(destroyGameObject());
        }
      
    }

    IEnumerator destroyGameObject()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    


}