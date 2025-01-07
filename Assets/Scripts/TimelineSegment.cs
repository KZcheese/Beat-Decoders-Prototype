using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SegmentUI))]
public class TimelineSegment : DraggableSegment
{
    private List<bool> _solution;
    private AudioSource _instrument;

    protected override void Start()
    {
        _solution = notes;
        notes = new List<bool>(_solution.Count);
        base.Start();
    }

    public void SetInstrument(AudioSource audioSource)
    {
        _instrument = audioSource;
        notes = new List<bool>(_solution.Count);
    }

    public void PlaySeg(int bpm, double startTime)
    {
        for (int i = 0; i < notes.Count; i++)
            _instrument.PlayScheduled(startTime + (i * bpm));
    }

    public void SetSegment(List<bool> beats)
    {
        notes = beats;
        SegmentUI.UpdateTiles(notes);
    }

    public int GetBeatCount()
    {
        return notes.Count;
    }

    public override void Drop()
    {
        base.Drop();
        SetSegment(new List<bool>(_solution.Count));
    }
}