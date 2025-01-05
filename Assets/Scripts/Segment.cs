using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Segment : MonoBehaviour
{
    private List<bool> _notes;
    public List<bool> sol;
    private AudioSource _instrument;
    private Image _image;
    private Sprite _emptySegment;
    
    private HashSet<Segment> _overlappingSegments;

    private void Start()
    {
        _image = GetComponent<Image>();
        _emptySegment = _image.sprite;
    }

    public void SetInstrument(AudioSource audioSource)
    {
        _instrument = audioSource;
        _notes = new List<bool>(sol.Count);
    }
    
    public void PlaySeg(int bpm, double startTime)
    {
        for (int i = 0; i < _notes.Count; i++)
        {
            _instrument.PlayScheduled(startTime + (i * bpm));
        }
    }

    public void SetBeats(Sprite image, List<bool> beats)
    {
        _image.sprite = image;
        _notes = beats;
    }

    public int GetBeatCount()
    {
        return _notes.Count;
    }

    private Segment ClosestSegment()
    {   
        Segment closestSegment = null;
        float closestDistance = Mathf.Infinity;
        
        foreach (Segment segment in _overlappingSegments)
        {
            float distance = Vector2.Distance(segment.transform.position, transform.position);
            
            if(closestDistance < distance) continue;
            closestSegment = segment;
            closestDistance = distance;
        }
        return closestSegment;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Segment otherSegment = other.GetComponent<Segment>();
        if (otherSegment) _overlappingSegments.Add(otherSegment);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Segment otherSegment = other.GetComponent<Segment>();
        if (otherSegment) _overlappingSegments.Remove(otherSegment);
    }
}
