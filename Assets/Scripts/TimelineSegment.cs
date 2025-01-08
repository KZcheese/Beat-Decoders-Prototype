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
        base.Start();
        _solution = Notes;
        Notes = new bool[_solution.Length];
        SegmentUI.UpdateTiles(Notes);
    }

    public void SetInstrument(AudioSource audioSource)
    {
        _instrument = audioSource;
    }

    public IEnumerator PlaySeg(double interval, double startTime)
    {
        double playTime = startTime;
        for (int i = 0; i < Notes.Length; i++)
        {
            yield return new WaitUntil(() => AudioSettings.dspTime >= playTime);

            if(Notes[i])
                _instrument.PlayScheduled(startTime + (i * interval));
            
            playTime += interval;
        }
        
        onSegmentEnd.Invoke(this);
    }

    public void SetSegment(bool[] beats)
    {
        Notes = beats;
        SegmentUI.UpdateTiles(Notes);
    }

    public int GetBeatCount()
    {
        return Notes.Length;
    }

    public bool IsCorrect()
    {
        return Notes.SequenceEqual(_solution);
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        SetSegment(new bool[_solution.Length]);
    }
}