using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu]
public class AudioMixerVolumeController : ScriptableObject
{
    [SerializeField]
    private AudioMixer audioMixer;

    private bool muted;

    public void OnEnable()
    {
        muted = false;
    }

    public void ToggleMute()
    {
        muted = !muted;

        if (muted)
        {
            audioMixer.SetFloat("volume", -80f);
        }

        else
        {
            audioMixer.SetFloat("volume", 0f);
        }
    }

    public bool IsMuted()
    {
        return muted;
    }
}