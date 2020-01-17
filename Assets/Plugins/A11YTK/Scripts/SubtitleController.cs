using UnityEngine;

namespace A11YTK
{

    [RequireComponent(typeof(SubtitleRenderer))]
    public class SubtitleController : MonoBehaviour
    {

        private const float DEFAULT_VOLUME_SCALE = 1f;

        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        [TextArea(1, 10)]
        private string _subtitleText;

        [SerializeField]
        private float _durationPerLine = 1;

        [SerializeField]
        private Subtitle.Position _position = Subtitle.Position.AUTO;

        [SerializeField]
        private Subtitle.Type _type;

        [SerializeField]
        private SubtitleOptionsReference _subtitleOptions;

        private SubtitleRenderer _subtitleRenderer;

        private void Awake()
        {

            _subtitleRenderer = gameObject.GetComponent<SubtitleRenderer>();

        }

        public void PlayOneShot(AudioClip clip, float volumeScale = DEFAULT_VOLUME_SCALE)
        {

            _audioSource.PlayOneShot(clip, volumeScale);

        }

        public void PlayOneShot()
        {

            _audioSource.PlayOneShot(_audioSource.clip, DEFAULT_VOLUME_SCALE);

        }

    }

}