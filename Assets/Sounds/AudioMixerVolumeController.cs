using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu]
public class AudioMixerVolumeController : ScriptableObject
{
    [SerializeField]
    private AudioMixer audioMixer;

    private bool muted;

    private float volume;

    public void ToggleMute()
    {
        if (muted)
        {
            audioMixer.SetFloat("volume", -80f);
        }

        else
        {
            audioMixer.SetFloat("volume", 0f);
        }

        muted = !muted;
    }
}