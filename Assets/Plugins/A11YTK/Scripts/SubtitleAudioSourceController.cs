#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
#endif
using UnityEngine;

namespace A11YTK
{

    [AddComponentMenu("A11YTK/Subtitle Audio Source Controller")]
    [RequireComponent(typeof(SubtitleRenderer))]
    public class SubtitleAudioSourceController : SubtitleController
    {

#pragma warning disable CS0649
        [SerializeField]
        private AudioSource _audioSource;
#pragma warning restore CS0649

        protected override double _elapsedTime => _audioSource.time;

        protected override bool _isPlaying =>
            _audioSource && _audioSource.isPlaying && _audioSource.time < _audioSource.clip.length;

#if UNITY_EDITOR
        protected void OnValidate()
        {

            if (_subtitleOptions == null)
            {

                _subtitleOptions =
                    AssetDatabase.LoadAssetAtPath<SubtitleOptionsReference>(AssetDatabase.GUIDToAssetPath(AssetDatabase
                        .FindAssets("t:SubtitleOptionsReference", null)
                        .FirstOrDefault()));

            }

            if (_audioSource == null)
            {

                _audioSource = gameObject.GetComponent<AudioSource>();

            }

        }
#endif

    }

}