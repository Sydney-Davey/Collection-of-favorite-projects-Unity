using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    [Header("Targets")]
    public List<Transform> specificTargets;

    private AudioSource source;
    private SphereCollider sphere;

    private Coroutine fadeRoutine;

    private void Start()
    {
        // geting the Audio and collider
        source = GetComponent<AudioSource>();
        sphere = GetComponent<SphereCollider>();

        source.volume = 0f;
        fadeRoutine = StartCoroutine(Fade(true, source, 2f, 1f)); // fadeing sound in on called
    }

    // if the specific targets enter the collider fade background music out
    private void OnTriggerEnter(Collider other)
    {
        Transform root = other.transform.root;

        if (specificTargets.Contains(root))
        {
            Debug.Log("Detected target: " + root.name);

            if (fadeRoutine != null) StopCoroutine(fadeRoutine);
            fadeRoutine = StartCoroutine(Fade(true, source, 2f, 0f));
        }
    }

    // if the specific targets leave the collider fade background music in
    private void OnTriggerExit(Collider other)
    {
        Transform root = other.transform.root;

        if (specificTargets.Contains(root))
        {
            Debug.Log("Target left: " + root.name);

            if (fadeRoutine != null) StopCoroutine(fadeRoutine);
            fadeRoutine = StartCoroutine(Fade(false, source, 2f, 1f));
        }
    }

    // fadeing the sounds 
    private IEnumerator Fade(bool fadeIn, AudioSource source, float duration, float targetVolume)
    {
        float time = 0f;
        float startVolume = source.volume;

        while (time < duration)
        {
            time += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
            yield return null;
        }
    }
}

