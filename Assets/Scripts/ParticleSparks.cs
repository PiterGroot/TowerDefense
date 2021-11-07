using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSparks : MonoBehaviour
{
    [SerializeField] private ParticleSystem Sparks;
    public void PlaySpark() {
        try{
            Sparks.Play();
        }
        catch{
            
        }
    }
}
