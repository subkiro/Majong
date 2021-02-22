using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [Header("VFX sounds")]
    [SerializeField] private List<AudioClip> vfx;

    [Header("Music sounds")]
    [SerializeField] private AudioClip[] music;


    public static SoundManager instance;
    private AudioSource source;
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
        {
            instance = this;
            
            DontDestroyOnLoad(this.gameObject);
        }

    }



    public void PlayVFX(string name) {
        source = GetComponent<AudioSource>();
        AudioClip clip = GetVFX(name);
        if (clip != null)
        {
            source.PlayOneShot(clip);
        }
        else {
            Debug.Log("SoundManager - No Vfx with name: " + name + " has found");
        }
           

    }

    private AudioClip GetVFX(string name) {
        for (int i = 0; i < vfx.Count; i++)
        {
            if (name == vfx[i].name) {
                return vfx[i];

            }
        }
        return null;
    }



}
