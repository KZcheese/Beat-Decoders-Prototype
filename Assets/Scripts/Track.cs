using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Track : MonoBehaviour
{
    private AudioSource _instrument;
    private Segment[] _segments;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _instrument = GetComponent<AudioSource>();
        
        _segments = GetComponentsInChildren<Segment>();
        foreach (Segment segment in _segments)
            segment.SetInstrument(_instrument);
    }
    
    public void PlayTrack(int bpm, double startTime)
    {
        double spb = 1.0 / (bpm * 60);
        double currTime = startTime;
        
        foreach (Segment seg in _segments)
        {
            seg.SetInstrument(_instrument);
            seg.PlaySeg(bpm, currTime);
            currTime += spb * seg.GetBeatCount();
        }
    }
}
