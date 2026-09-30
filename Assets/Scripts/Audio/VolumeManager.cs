using UnityEngine;

/// <summary>Which volume slider a sound belongs to.</summary>
public enum AudioChannel
{
    Music,
    Sfx
}

/// <summary>
/// Stores music and sound-effect volume, remembers them between sessions and
/// tells every registered source when they change.
///
/// Nothing needs to be placed in a scene: the first component that asks for
/// <see cref="Instance"/> creates it.
/// </summary>
public class VolumeManager : MonoBehaviour
{
    private const string MusicKey = "volume.music";
    private const string SfxKey = "volume.sfx";

    /// <summary>Raised whenever a volume changes, so sources can re-apply it.</summary>
    public static event System.Action Changed;

    private static VolumeManager instance;

    public static VolumeManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<VolumeManager>();

                if (instance == null)
                    instance = new GameObject("VolumeManager").AddComponent<VolumeManager>();
            }

            return instance;
        }
    }

    private float music = 1f;
    private float sfx = 1f;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        music = PlayerPrefs.GetFloat(MusicKey, 1f);
        sfx = PlayerPrefs.GetFloat(SfxKey, 1f);
    }

    private void OnDestroy()
    {
        if (instance == this) instance = null;
    }

    public float Get(AudioChannel channel)
    {
        return channel == AudioChannel.Music ? music : sfx;
    }

    public void Set(AudioChannel channel, float value)
    {
        value = Mathf.Clamp01(value);

        if (channel == AudioChannel.Music)
        {
            music = value;
            PlayerPrefs.SetFloat(MusicKey, value);
        }
        else
        {
            sfx = value;
            PlayerPrefs.SetFloat(SfxKey, value);
        }

        PlayerPrefs.Save();
        Changed?.Invoke();
    }
}
