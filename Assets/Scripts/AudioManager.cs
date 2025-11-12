using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Range(0f, 0.5f)] public float pitchVariation = 0.1f;
    public void PlaySound(GameObject soundPrefab)
    {
        GameObject soundObj = Instantiate(soundPrefab);
        AudioSource source = soundObj.GetComponent<AudioSource>();

        float randomPitch = 1f + Random.Range(-pitchVariation, pitchVariation);
        source.pitch = randomPitch;

        source.Play();

        Destroy(soundObj, source.clip.length / source.pitch);
    }
}
