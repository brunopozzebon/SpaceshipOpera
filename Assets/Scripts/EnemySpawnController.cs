using System.Collections;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    private const float DEFAULT_SPAWN_TIME = 2f;
    private const float MIN_SPAWN_TIME = 0.8f;
    private const float MAX_SPAWN_TIME = 6f;
    public const int MAX_ENEMYES = 40;

    private float spawnTimeDecrement = 1.0f;

    public GameObject enemy;
    public Transform[] spots = new Transform[3];

    public GameObject[] portals = new GameObject[3];
    private Animator[] portalAnimations = new Animator[3];
    public Transform player;
    public static bool spawnEnds = false;

    private GameObject enemyCopy;
    
    void Start()
    {
        portalAnimations[0] = portals[0].GetComponent<Animator>();
        portalAnimations[1] = portals[1].GetComponent<Animator>();
        portalAnimations[2] = portals[2].GetComponent<Animator>();
        
        StartCoroutine(createEnemy());
    }
    
    IEnumerator createEnemy()
    {
        while (canSpawn())
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length > MAX_ENEMYES)
            {
                yield return new WaitForSeconds(MAX_SPAWN_TIME);
            }
            else
            {
                int randomIndex = Random.Range(0, 3);
                
                Transform spotPosition = spots[randomIndex];
                
                Quaternion _lookRotation = 
                    Quaternion.LookRotation((player.position - spotPosition.transform.position).normalized);
        
                spotPosition.rotation = _lookRotation;
                portalAnimations[randomIndex].SetBool("isOpen",true );
              
                StartCoroutine(portalGoAway(randomIndex));
                StartCoroutine(createEnemy(enemy, spotPosition));
                
                float nextSpawnTime = 1/ (SongController.getSongIntensityMedia() * 14);
                nextSpawnTime -= spawnTimeDecrement;
                spawnTimeDecrement += 0.02f;
                
                float spawnTimeSanitized =  Mathf.Clamp(nextSpawnTime, MIN_SPAWN_TIME, MAX_SPAWN_TIME);
                
                yield return new WaitForSeconds(float.IsNaN(spawnTimeSanitized)?DEFAULT_SPAWN_TIME:spawnTimeSanitized);
            }
        }

        spawnEnds = true;
    }

    bool canSpawn()
    {
        if (KillsController.kills >= KillsController.KILLS_TO_WIN)
        {
            return false;
        }

        return true;
    }
    
    IEnumerator createEnemy(GameObject enemy, Transform spotPosition)
    {
        yield return new WaitForSeconds(1.0f);
        GameObject copy = Instantiate(enemy, spotPosition.position, spotPosition.rotation);
        copy.SetActive(true);
    }

    IEnumerator portalGoAway(int index)
    {
        yield return new WaitForSeconds(1.5f);
        portalAnimations[index].SetBool("isOpen",false );

    }
}
