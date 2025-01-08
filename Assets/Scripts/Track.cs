using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Track : MonoBehaviour
{
    private AudioSource _instrument;
    private TimelineSegment[] _segments;
    private List<Coroutine> _playingSegments = new List<Coroutine>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _instrument = GetComponent<AudioSource>();
        
        _segments = GetComponentsInChildren<TimelineSegment>();
        foreach (TimelineSegment segment in _segments)
            segment.SetInstrument(_instrument);
    }
    
    public void PlayTrack(double interval, double startTime)
    {
        double segTime = startTime;

        foreach (TimelineSegment seg in _segments)
        {
            seg.SetInstrument(_instrument);
            StartCoroutine(seg.PlaySeg(interval, segTime));
            segTime += interval * seg.GetBeatCount();
        }
    }

    public void StopTrack()
    {
        StopAllCoroutines();
    }
}
