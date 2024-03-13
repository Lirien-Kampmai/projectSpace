using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class LevelEpisodeSelection : MonoBehaviour
    {
        [SerializeField] private Episode episode;
        [SerializeField] private Text    episodeNickname;
        [SerializeField] private Image   previewImage;

        private void Start()
        {
            if (episodeNickname != null)
                episodeNickname.text = episode.EpisodeName;
            if (previewImage != null)
                previewImage.sprite = episode.PreviewImage;
        }

        public void OnStartEpisodeButton() { LevelSequenceController.Instance.StartEpisode(episode); }
    }
}