using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ElfWizard.Manager
{
    public class AudioManager : BaseManagerNonMono
    {
        public AudioManager(GameFacade facade) : base(facade)
        {
        }
        private AudioSource bgAudioSource;
        private AudioSource normalAudioSource;
        public override void OnInit()
        {
            GameObject audioSourceGO = new GameObject("AudioSource(GameObject)");
            bgAudioSource = audioSourceGO.AddComponent<AudioSource>();
            normalAudioSource = audioSourceGO.AddComponent<AudioSource>();

            //PlaySound(bgAudioSource, LoadSound(BGM), 0.5f, true);
        }
        public void PlayBgSound(string soundname)
        {
            PlaySound(bgAudioSource, LoadSound(soundname), 0.5f, true);
        }
        public void PlayNormalSound(string soundname)
        {
            PlaySound(normalAudioSource, LoadSound(soundname), 1);
        }
        private void PlaySound(AudioSource audioSource, AudioClip clip, float volume, bool loop = false)
        {
            audioSource.volume = volume;
            audioSource.clip = clip;
            audioSource.loop = loop;
            audioSource.Play();
        }
        private AudioClip LoadSound(string soundName)
        {
            return ResourceManager.Load<AudioClip>(soundName);

        }
    }
}