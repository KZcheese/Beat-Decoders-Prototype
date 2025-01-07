using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SegmentUI))]
public abstract class DraggableSegment : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public List<bool> notes;
    private readonly HashSet<TimelineSegment> _overlappingSegments = new HashSet<TimelineSegment>();
    protected SegmentUI SegmentUI;

    protected virtual void Start()
    {
        SegmentUI = GetComponent<SegmentUI>();
        SegmentUI.UpdateTiles(notes);
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos = transform.position;
        Vector2 delta = eventData.delta;
        transform.position = new Vector3(pos.x + delta.x, pos.y + delta.y, pos.z);
        
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        if(_overlappingSegments.Count < 1) return;
        TimelineSegment closestSeg = null;
        float currentDistance = float.MaxValue;

        foreach (TimelineSegment segment in _overlappingSegments)
        {
            float newSegDist = Vector2.Distance(transform.position, segment.transform.position);
            if(currentDistance < newSegDist) continue;

            closestSeg = segment;
            currentDistance = newSegDist;
        }

        if(closestSeg) closestSeg.SetSegment(notes);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TimelineSegment timelineSegment = other.gameObject.GetComponent<TimelineSegment>();
        if(timelineSegment) _overlappingSegments.Add(timelineSegment);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        TimelineSegment timelineSegment = other.gameObject.GetComponent<TimelineSegment>();
        if(timelineSegment) _overlappingSegments.Remove(timelineSegment);
    }


}