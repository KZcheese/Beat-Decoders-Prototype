using UnityEngine;
using UnityEngine.UI;

public class Timeline : MonoBehaviour
{
    private Track[] _tracks;
    public float startDelay = 0.1f;
    public int bpm = 80;
    public Image playButton;
    private Sprite _playSprite;
    public Sprite stopSprite;
    
    private bool _isPlaying;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _tracks = GetComponentsInChildren<Track>();
        _playSprite = playButton.sprite;
    }

    public void PlayTimeLine()
    {
        _isPlaying = !_isPlaying;

        if(_isPlaying)
        {
            playButton.sprite = stopSprite;
            
            double startTime = AudioSettings.dspTime + startDelay;
            double interval = 1.0 / bpm * 60 / 4.0; // assumes 4/4

            foreach (Track track in _tracks)
                track.PlayTrack(interval, startTime);
        }
        else
        {
            playButton.sprite = _playSprite;
            
            foreach (Track track in _tracks)
                track.StopTrack();
        }

    }
}
