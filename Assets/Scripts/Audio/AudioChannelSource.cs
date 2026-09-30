using UnityEngine;

/// <summary>
/// Put this next to any AudioSource and pick the channel it belongs to.
/// The volume set in the inspector stays as the source's own balance; the
/// slider scales it.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class AudioChannelSource : MonoBehaviour
{
    [SerializeField] private AudioChannel channel = AudioChannel.Sfx;

    private AudioSource source;
    private float baseVolume = 1f;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        baseVolume = source.volume;   // the balance you set in the inspector
    }

    private void OnEnable()
    {
        VolumeManager.Changed += Apply;
        Apply();
    }

    private void OnDisable()
    {
        VolumeManager.Changed -= Apply;
    }

    private void Apply()
    {
        if (source != null) source.volume = baseVolume * VolumeManager.Instance.Get(channel);
    }
}
