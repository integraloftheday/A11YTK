using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Video;
using UnityEngine.Playables;

namespace A11YTK
{

    [AddComponentMenu("A11YTK/Subtitle Timeline Player Controller")]
    public class SubtitleTimelineController : SubtitleSourceController
    {

#pragma warning disable CS0649
        [FormerlySerializedAs("_videoSource")]
        [SerializeField]
        private PlayableDirector _playableDirector;
        [Tooltip("The max time, in seconds, for which subtitles should play. This is due to the fact that Playable Directors have no length property")]
        public int maxTime;
#pragma warning restore CS0649

        protected override double _elapsedTime => _playableDirector.time;

        protected override bool _isPlaying =>
            _playableDirector && _playableDirector.state == PlayState.Playing && _playableDirector.time < maxTime;

#if UNITY_EDITOR
        protected override void OnValidate()
        {

            base.OnValidate();

            if (_playableDirector == null)
            {

                _playableDirector = gameObject.GetComponent<PlayableDirector>();

            }

        }
#endif

    }

}
