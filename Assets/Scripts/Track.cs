using UnityEngine;

public class Track : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip instrument;
    private Segment[] _segments;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _segments = GetComponentsInChildren<Segment>();
        audioSource.clip = instrument;
        foreach (Segment segment in _segments)
            segment.SetInstrument(audioSource);
        
    }
    
    public void PlayTrack(int bpm, double startTime)
    {
        double spb = 1.0 / (bpm * 60);
        double currTime = startTime;
        
        foreach (Segment seg in _segments)
        {
            seg.SetInstrument(audioSource);
            seg.PlaySeg(bpm, currTime);
            currTime += spb * seg.GetBeatCount();
        }
    }
}
