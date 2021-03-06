using System.Collections;
using System.Collections.Generic;
using ElfWizard.Manager;
using Framework;
using UnityEngine;

namespace ElfWizard
{
    public interface IAudioSystem : ISystem
    {
        public void PlayBgSound(string soundname);
        public void PlayNormalSound(string soundname);
    }
    public class AudioManager : BaseManagerSystem, IAudioSystem
    {
        private AudioSource bgAudioSource;
        private AudioSource normalAudioSource;
        protected override void OnInit()
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
            return ResourceManager.LoadObsolete<AudioClip>(soundName);

        }

        public override void OnDestroy()
        {
            
        }
    }
}