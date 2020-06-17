using UnityEngine;

namespace Controllers
{
    public class SoundController : MonoBehaviour
    {
        public enum SoundName
        {
            SoundExample
        }
        
        public static SoundController Instance { get; private set; }

        private AudioSource _soundPlayer;
        [SerializeField] private AudioClip soundExample;

        private void Awake()
        {
            Instance = this;
            _soundPlayer = this.GetComponent<AudioSource>();
            if (soundExample == null)
                soundExample = Resources.Load<AudioClip>("Sounds/SoundExample_SFX");
        }

        public void PlaySound(SoundName soundName)
        {
            switch (soundName)
            {
                case SoundName.SoundExample:
                    _soundPlayer.PlayOneShot(soundExample);
                    break;
                default:
                    Debug.LogWarning("THERE'S NO SOUND LIKE THIS");
                    break;
            }
        }
    }
}
