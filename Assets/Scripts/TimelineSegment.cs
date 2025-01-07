using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SegmentUI))]
public class TimelineSegment : DraggableSegment
{
    private bool[] _solution;
    private AudioSource _instrument;

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

    public void PlaySeg(int bpm, double startTime)
    {
        for (int i = 0; i < notes.Length; i++)
            _instrument.PlayScheduled(startTime + (i * bpm));
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

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        SetSegment(new bool[_solution.Length]);
    }
}