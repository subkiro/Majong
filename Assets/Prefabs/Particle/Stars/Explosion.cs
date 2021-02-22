
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    private void Start()
    {
        Destroy(this.gameObject, this.GetComponent<ParticleSystem>().main.duration);
    }
    public float GetDuration()
    {
       return this.GetComponent<ParticleSystem>().main.duration;
    }


    
}
