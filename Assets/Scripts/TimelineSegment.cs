using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

[RequireComponent(typeof(SegmentUI))]
public class TimelineSegment : DraggableSegment
{
    private bool[] _solution;
    private AudioSource _instrument;
    public UnityEvent<TimelineSegment> onSegmentEnd;

    protected override void Start()
    {
        _solution = notes;
        notes = new bool[_solution.Length];
        base.Start();
    }

    public void SetInstrument(AudioSource audioSource)
    {
        _instrument = audioSource;
    }

    public IEnumerator PlaySeg(double interval, double startTime)
    {
        double playTime = startTime;
        for (int i = 0; i < notes.Length; i++)
        {
            yield return new WaitUntil(() => AudioSettings.dspTime >= playTime);

            if(notes[i])
                _instrument.PlayScheduled(startTime + (i * interval));
            
            playTime += interval;
        }
        
        onSegmentEnd.Invoke(this);
    }

    public void SetSegment(bool[] beats)
    {
        notes = beats;
        SegmentUI.UpdateTiles(notes);
    }

    public int GetBeatCount()
    {
        return notes.Length;
    }

    public bool IsCorrect()
    {
        return notes.SequenceEqual(_solution);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        SetSegment(new bool[_solution.Length]);
    }
}