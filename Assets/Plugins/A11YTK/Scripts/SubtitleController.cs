using System.Collections.Generic;
using UnityEngine;

namespace A11YTK
{

    public abstract class SubtitleController : MonoBehaviour
    {

#pragma warning disable CS0649
        [SerializeField]
        [TextArea(1, 10)]
        protected string _subtitleText = "1\n0:0:1,0 --> 0:0:2,0\nHello, world.\n";

        [SerializeField]
        protected TextAsset _subtitleTextAsset;

        [SerializeField]
        protected Subtitle.Position _position = Subtitle.Position.AUTO;

        [SerializeField]
        protected Subtitle.Type _type;

        [SerializeField]
        protected SubtitleOptionsReference _subtitleOptions;
#pragma warning restore CS0649

        public Subtitle.Position position =>
            _position.Equals(Subtitle.Position.AUTO) ? _subtitleOptions.defaultPosition : _position;

        public SubtitleOptionsReference subtitleOptions => _subtitleOptions;

        protected SubtitleRenderer _subtitleRenderer;

        protected List<SRT.Subtitle> _subtitles;

        protected abstract double _elapsedTime { get; }

        protected abstract bool _isPlaying { get; }

        protected SRT.Subtitle? _currentSubtitle;

        protected void Awake()
        {

            _subtitleRenderer = gameObject.GetComponent<SubtitleRenderer>();

            if (_subtitleTextAsset != null)
            {

                _subtitles = SRT.ParseSubtitlesFromString(_subtitleTextAsset.text);

            }
            else
            {

                _subtitles = SRT.ParseSubtitlesFromString(_subtitleText);

            }

            if (subtitleOptions == null)
            {

                Debug.LogWarning("Subtitle options asset is missing!");

            }

        }

        protected void FixedUpdate()
        {

            if (subtitleOptions.enabled && _isPlaying)
            {

                Tick();

            }

        }

        protected void Tick()
        {

            if (_currentSubtitle.HasValue &&
                _subtitleRenderer.isVisible &&
                (_elapsedTime < _currentSubtitle.Value.startTime ||
                 _elapsedTime > _currentSubtitle.Value.endTime))
            {

                _subtitleRenderer.Hide();

                _currentSubtitle = null;

            }

            if (!_currentSubtitle.HasValue)
            {

                for (var i = 0; i < _subtitles.Count; i += 1)
                {

                    if (_elapsedTime < _subtitles[i].endTime)
                    {

                        _currentSubtitle = _subtitles[i];

                        break;

                    }

                }

            }

            if (_currentSubtitle.HasValue &&
                !_subtitleRenderer.isVisible &&
                _elapsedTime >= _currentSubtitle.Value.startTime &&
                _elapsedTime <= _currentSubtitle.Value.endTime)
            {

                _subtitleRenderer.Show();

                _subtitleRenderer.SetText(_currentSubtitle.Value.text);

            }

        }

    }

}
