using UnityEngine;

namespace A11YTK
{

    [CreateAssetMenu(fileName = "SubtitleOptionsReference", menuName = "A11YTK/SubtitleOptionsReference")]
    public class SubtitleOptionsReference : ScriptableObject
    {

        public Subtitle.Position defaultPosition = Subtitle.Position.BOTTOM;

        public float fontSize = 1;

        public Color fontForegroundColor = Color.white;

        public Color fontBackgroundColor = Color.black;

        public bool showBackgroundColor = false;

    }

}