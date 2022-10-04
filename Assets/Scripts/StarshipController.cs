using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class StarshipController : MonoBehaviour
{
    const float DEFAULT_PITCH_FORCE = 70f;
    const float DEFAULT_FORWARD_FORCE = 2000f;
    const float DEFAULT_SIDE_FORCE = 1000f;
    const float DEFAULT_SHOOT_VELOCITY = 12000f;
    const float DEFAULT_MAX_VELOCITY = 1000f;

    public float shipLife = 100f;
    public Transform firstShootPosition, secondShootPosition, thirdShootPosition;
    public bool shootWithFirstCannon = true;
    public Material blueMaterial, redMaterial, yellowMaterial, whiteMaterial;

    public GameObject shoot,
        bigShoot,
        leftTrail,
        rightTrail,
        lifebar,
        explosion,
        forceField,
        healhParticles,
        gameOverScreen;
    private float forward1D, horizontal1D, pitch, glideForward = 0f, glideHorizontal = 0f;
    private float pitchForce = DEFAULT_PITCH_FORCE;
    private float forwardForce = DEFAULT_FORWARD_FORCE;
    private float sideForce = DEFAULT_SIDE_FORCE;
    private float shootVelocity = DEFAULT_SHOOT_VELOCITY;
    private float maxVelocity = DEFAULT_MAX_VELOCITY;

    private Rigidbody rigidbody;
    private LifeBarController lifeBarController;
    private ParticleSystem explosionParticlesSystem;
    private TrailRenderer leftTrailRenderer, rightTrailRenderer;
    private ParticleSystem healthParticlesSystem;
    private MeshRenderer forceFieldRenderer;

    private CapsuleCollider capsuleCollider;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        lifeBarController = lifebar.GetComponent<LifeBarController>();
        explosionParticlesSystem = explosion.GetComponent<ParticleSystem>();
        leftTrailRenderer = leftTrail.GetComponent<TrailRenderer>();
        rightTrailRenderer = rightTrail.GetComponent<TrailRenderer>();
        forceFieldRenderer = forceField.GetComponent<MeshRenderer>();
        forceFieldRenderer.enabled = false;
        healthParticlesSystem = healhParticles.GetComponent<ParticleSystem>();
        healthParticlesSystem.Stop();
    }

    void FixedUpdate()
    {
        Vector3 forwardVelocityFactor, horizontalVelocityFactor;
        if (forward1D > 0.1f || forward1D < -0.1f)
        {
            forwardVelocityFactor = Vector3.forward * forward1D * forwardForce * Time.deltaTime;
            glideForward = forwardForce;
        }
        else
        {
            forwardVelocityFactor = Vector3.forward * glideForward * Time.deltaTime;
            glideForward *= 0.11f;
        }

        if (horizontal1D > 0.1f || horizontal1D < -0.1f)
        {
            horizontalVelocityFactor = Vector3.right * horizontal1D * sideForce * Time.deltaTime;
            glideHorizontal = sideForce;
        }
        else
        {
            horizontalVelocityFactor = Vector3.right * glideHorizontal * Time.deltaTime;
            glideHorizontal *= 0.11f;
        }

        if (rigidbody.velocity.magnitude < maxVelocity)
        {
            rigidbody.AddRelativeForce(forwardVelocityFactor);
            rigidbody.AddRelativeForce(horizontalVelocityFactor);
        }

        if (pitch > 0.1f || pitch < -0.1f)
        {
            transform.Rotate(Vector3.up * pitch * Time.deltaTime * pitchForce);
        }
    }

    public void onShoot(InputAction.CallbackContext context)
    {
        if (SongController.songType != SongType.CLASSIC)
        {
            Transform shootTransform = shootWithFirstCannon ? firstShootPosition : secondShootPosition;
            GameObject finalShot;

            if (SongController.songType == SongType.ROCK)
            {
                finalShot = bigShoot;
                shootTransform = thirdShootPosition;
            }
            else
            {
                finalShot = shoot;
            }

            GameObject shootClone = Instantiate(finalShot, shootTransform.position, shootTransform.rotation);

            Rigidbody shootRigidBody = shootClone.GetComponent<Rigidbody>();
            shootClone.AddComponent<ShootScript>();

            shootRigidBody.velocity = rigidbody.velocity;
            Vector3 shootForce = rigidbody.transform.forward * shootVelocity;
            shootRigidBody.AddForce(shootForce);
            shootWithFirstCannon = !shootWithFirstCannon;
        }
    }
    
    public void receiveDamage(Vector3 colissionPoint, float damage)
    {
        RickSpeakingScript.playAudioWhenShooted();
        updateLife(-damage);
        explosion.transform.position = colissionPoint;
        explosionParticlesSystem.Play();
    }

    public void updateLife(float delta)
    {
        if (!(shipLife >= 100 && delta > 0))
        {
            shipLife += delta;
            lifeBarController.updateLife(shipLife);
        
            if (shipLife <= 0)
                gameOver();
        }
    }

    public void gameOver()
    {
        RickSpeakingScript.playAudioWhenDied();
        capsuleCollider.height = 0f;
        capsuleCollider.center = new Vector3(0, 4, 0);
        GameOver.gameIsOver = true;
        SongController.shutDownSong();
        StartCoroutine(gameOverAnimation());
    }
    
    IEnumerator gameOverAnimation()
    {
        yield return new WaitForSeconds(1f);
        CarExplosion.explode();
        yield return new WaitForSeconds(2f);
        gameOverScreen.SetActive(true);
    }

    public void changeSong(SongType songType)
    {
        pitchForce = DEFAULT_PITCH_FORCE;
        forwardForce = DEFAULT_FORWARD_FORCE;
        sideForce = DEFAULT_SIDE_FORCE;
        shootVelocity = DEFAULT_SHOOT_VELOCITY;
        maxVelocity = DEFAULT_MAX_VELOCITY;
        forceFieldRenderer.enabled = false;

        GameObject[] turbines = GameObject.FindGameObjectsWithTag("TurbineRay");
        switch (songType)
        {
            case SongType.ROCK:
                shootVelocity = DEFAULT_SHOOT_VELOCITY * 2f;
                maxVelocity = DEFAULT_MAX_VELOCITY / 1.5f;
                sideForce = DEFAULT_SIDE_FORCE / 2f;
                forwardForce = DEFAULT_FORWARD_FORCE / 2f;
                foreach (GameObject turbine in turbines)
                {
                    turbine.GetComponent<MeshRenderer>().material = redMaterial;
                }

                leftTrailRenderer.material = redMaterial;
                rightTrailRenderer.material = redMaterial;

                break;
            case SongType.ELETRONIC:
                pitchForce = DEFAULT_PITCH_FORCE * 2f;
                forwardForce = DEFAULT_FORWARD_FORCE * 2f;
                sideForce = DEFAULT_SIDE_FORCE * 2f;
                shootVelocity = DEFAULT_SHOOT_VELOCITY * 2f;
                maxVelocity = DEFAULT_MAX_VELOCITY * 2f;

                foreach (GameObject turbine in turbines)
                {
                    turbine.GetComponent<MeshRenderer>().material = yellowMaterial;
                }

                leftTrailRenderer.material = yellowMaterial;
                rightTrailRenderer.material = yellowMaterial;
                break;
            case SongType.CLASSIC:
                foreach (GameObject turbine in turbines)
                {
                    turbine.GetComponent<MeshRenderer>().material = whiteMaterial;
                }

                forceFieldRenderer.enabled = true;
                healthParticlesSystem.Play();
                leftTrailRenderer.material = whiteMaterial;
                rightTrailRenderer.material = whiteMaterial;
                break;
            default:
                foreach (GameObject turbine in turbines)
                {
                    turbine.GetComponent<MeshRenderer>().material = blueMaterial;
                }

                leftTrailRenderer.material = blueMaterial;
                rightTrailRenderer.material = blueMaterial;
                break;
        }
    }
    
    public void onPitch(InputAction.CallbackContext context)
    {
        pitch = context.ReadValue<float>();
    }

    public void onForward(InputAction.CallbackContext context)
    {
        forward1D = context.ReadValue<float>();
    }

    public void onRightAndLeft(InputAction.CallbackContext context)
    {
        horizontal1D = context.ReadValue<float>();
    }
}