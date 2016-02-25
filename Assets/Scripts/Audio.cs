using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Audio : MonoBehaviour
{

    private static Dictionary<string, AudioSource> AudioSources;

	// Use this for initialization
	void Start () 
	{
		Initialise ();
    }

	void OnDestroy()
	{
		AudioSources.Clear ();
		AudioSources = null;
	}

	void Update()
	{
		Initialise ();
	}

	void Initialise()
	{
		if (AudioSources == null)
		{
			AudioSources = new Dictionary<string, AudioSource>();
			
			AudioSource[] sources = gameObject.GetComponentsInChildren<AudioSource>();
			for (int i = 0; i < sources.Length; i++)
			{
				AudioSources[sources[i].gameObject.name] = sources[i];
				DontDestroyOnLoad(sources[i]);
			}
		}	
	}

	public static AudioSource GetPullObjectOutSound()
	{
		return AudioSources ["SlideOut"];
	}

	public static AudioSource GetStartUp()
	{
		return AudioSources["StartUp"];
	}

	public static AudioSource GetShutdown()
	{
		return AudioSources ["Shutdown"];
	}

	public static AudioSource GetLowBattery()
	{
		return AudioSources ["LowBattery"];
	}
}

//    public static void StartGameMusic()
//    {
//        AudioSources["Background"].Play();
//    }
//}
