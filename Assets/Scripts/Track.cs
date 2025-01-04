using UnityEngine;

public class Track : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip instrument;
    private TrackSegment[] _segments;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _segments = GetComponentsInChildren<TrackSegment>();
        audioSource.clip = instrument;
        foreach (TrackSegment segment in _segments)
            segment.SetInstrument(audioSource);
        
    }
    
    public void PlayTrack(int bpm, double startTime)
    {
        double spb = 1.0 / (bpm * 60);
        double currTime = startTime;
        
        foreach (TrackSegment seg in _segments)
        {
            seg.SetInstrument(audioSource);
            seg.PlaySeg(bpm, currTime);
            currTime += spb * seg.GetBeatCount();
        }
    }
}
