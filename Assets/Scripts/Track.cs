using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class Track : MonoBehaviour
{
    private AudioSource _instrument;
    private TimelineSegment[] _segments;
    private readonly HashSet<TimelineSegment> _playingSegments = new HashSet<TimelineSegment>();
    public UnityEvent<Track> onTrackEnd;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _instrument = GetComponent<AudioSource>();
        
        _segments = GetComponentsInChildren<TimelineSegment>();
        foreach (TimelineSegment segment in _segments)
        {
            segment.SetInstrument(_instrument);
            segment.onSegmentEnd.AddListener(OnSegmentEnd);
        }
    }
    
    public void PlayTrack(double interval, double startTime)
    {
        double segTime = startTime;

        foreach (TimelineSegment seg in _segments)
        {
            StartCoroutine(seg.PlaySeg(interval, segTime));
            _playingSegments.Add(seg);
            segTime += interval * seg.GetBeatCount();
        }
    }

    public void StopTrack()
    {
        StopAllCoroutines();
        _playingSegments.Clear();
    }

    private void OnSegmentEnd(TimelineSegment segment)
    {
        _playingSegments.Remove(segment);
        if(_playingSegments.Count > 0) return;

        onTrackEnd.Invoke(this);
    }

    public bool IsCorrect()
    {
        return _segments.All(timelineSegment => timelineSegment.IsCorrect());
    }
    
}
