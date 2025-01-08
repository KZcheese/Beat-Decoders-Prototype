using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Timeline : MonoBehaviour
{
    private Track[] _tracks;
    private readonly HashSet<Track> _playingTracks = new HashSet<Track>();

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
        foreach (Track track in _tracks)
            track.onTrackEnd.AddListener(OnTrackEnd);
        
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
            {
                track.PlayTrack(interval, startTime);
                _playingTracks.Add(track);
            }
        }
        else
        {
            playButton.sprite = _playSprite;
            
            foreach (Track track in _tracks)
                track.StopTrack();
            _playingTracks.Clear();
        }
    }

    private void OnTrackEnd(Track track)
    {
        _playingTracks.Remove(track);
        if(_playingTracks.Count > 0) return;

        PlayTimeLine();
        Debug.Log(_tracks.All(timelineSegment => timelineSegment.IsCorrect()));
    }
}
