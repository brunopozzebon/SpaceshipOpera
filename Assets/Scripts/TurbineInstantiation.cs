using UnityEngine;

public class TurbineInstantiation : MonoBehaviour
{
    public GameObject rayModel;
    public GameObject[] rays = new GameObject[SongController.SPECTRUM_SIZE];
    public float turbineHeighScalar = 10;
    public float radius = 0.40f;
    void Start()
    {
        float angleIncrement = (360f / SongController.SPECTRUM_SIZE)*2;
        for (int i = 0; i<SongController.SPECTRUM_SIZE/2; i++)
        {
            GameObject newRay = Instantiate(rayModel);
            newRay.tag = "TurbineRay";
            float angle = angleIncrement * i;
           Vector3 p = new Vector3( radius * Mathf.Cos((Mathf.PI / 180) * angle),
               radius* Mathf.Sin((Mathf.PI / 180) * angle),0);
           
           newRay.transform.parent = transform;
           newRay.transform.position =  p + transform.position;
           rays[i] = newRay;
        }
    }

    void Update()
    {
        for (int i = 0; i < SongController.SPECTRUM_SIZE/2; i++)
        {
            float finalHeightScale = SongController.spectrum[i] * turbineHeighScalar;
            rays[i].transform.localScale = new Vector3(0.03f,  finalHeightScale,0.03f);
        }
        
    }
}
