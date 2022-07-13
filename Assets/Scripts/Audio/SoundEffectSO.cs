using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu]
public class SoundEffectSO : ScriptableObject
{
    public AudioClip clip;
    public Vector2 volumeRange = new Vector2(0.5f, 0.5f);
    public Vector2 pitchRange = new Vector2(1, 1);

    public AudioSource Play(AudioSource audioSourceParam = null, float delay = 0)
    {
        if (clip == null)
        {
            Debug.Log("Missing sound clips");
            return null;
        }

        var source = audioSourceParam;
        if (source == null)
        {
            var obj = new GameObject("Sound", typeof(AudioSource));
            source = obj.GetComponent<AudioSource>();
        }

        source.clip = clip;
        source.volume = Random.Range(volumeRange.x, volumeRange.y);
        source.pitch = Random.Range(pitchRange.x, pitchRange.y);

        source.PlayDelayed(delay);

        Destroy(source.gameObject, source.clip.length / source.pitch);

        return source;
    }
}
