using UnityEngine;
using System.Collections;

public class SoundEngine : MonoBehaviour {
	public static bool isSoundSFX = true;
    public static bool isSoundMusic = true;
    
    
    public AudioClip _soundclick = null;
    public AudioClip _soundMove = null;
    public AudioClip _soundWin = null;
    public AudioClip _soundpair = null;

    public AudioClip _soundBG1 = null;//dont úe




    //public AudioClip _soundShoot = null;
    
    public static SoundEngine instance;
    void Start()
    {
        /*
        if (instance != null)
        {
            Debug.Log("Destroy This");
            Destroy(this);
        }
        else
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
        //this.gameObject.
        SoundEngine.getInstance().PlayLoop(SoundEngine.getInstance()._soundBG1);
         * */
        instance = this;
    }
    public static SoundEngine getInstance()
    {
        if(instance == null)
        {
            instance = new SoundEngine();
        }
        return instance;
    }
    public void PlayOneShot(AudioClip e)
    {

        if (e == null)
            return;
       // if (!e..isPlaying)
            if (isSoundSFX)
            {
                GetComponent<AudioSource>().PlayOneShot(e);
            }
    }
    // Update is called once per frame
    public void PlayLoop(AudioClip e)
    {
        
        if (isSoundMusic)
        {
            if (GetComponent<AudioSource>() != null && e != null)
            {
                GetComponent<AudioSource>().clip = e;
                GetComponent<AudioSource>().loop = true;
                if (!GetComponent<AudioSource>().isPlaying)
                    GetComponent<AudioSource>().Play();
            }
        }
    }
    public void stopSound()
    {
        GetComponent<AudioSource>().Stop();
    }

	// Update is called once per frame
	void Update () {
	
	}

	
}
