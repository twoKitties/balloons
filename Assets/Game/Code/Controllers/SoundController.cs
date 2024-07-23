using UnityEngine;

namespace Game.Code.Controllers
{
    public class SoundController
    {
        private readonly AudioClip _clip;
        private readonly AudioSource _source;
        
        public SoundController(AudioSource source, AudioClip clip)
        {
            _source = source;
            _clip = clip;
        }

        public void Play()
        {
            _source.PlayOneShot(_clip);
        }
    }
}