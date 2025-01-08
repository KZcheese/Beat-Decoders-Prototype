using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SegmentUI))]
public class DraggableSegment : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    protected bool[] Notes;
    private readonly HashSet<TimelineSegment> _overlappingSegments = new HashSet<TimelineSegment>();
    protected SegmentUI SegmentUI;
    private Vector2 _startLocation;


    protected virtual void Start()
    {
        SegmentUI = GetComponent<SegmentUI>();
        Notes = SegmentUI.GetTileStatus();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startLocation = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos = transform.position;
        Vector2 delta = eventData.delta;
        transform.position = new Vector3(pos.x + delta.x, pos.y + delta.y, pos.z);

    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        transform.position = _startLocation;

        if(_overlappingSegments.Count < 1) return;
        TimelineSegment closestSeg = null;
        float currentDistance = float.MaxValue;

        Vector2 pos = AttemptGetCenter(gameObject);
        foreach (TimelineSegment segment in _overlappingSegments)
        {
            Vector2 otherPos = AttemptGetCenter(segment.gameObject);
            
            float newSegDist = Vector2.Distance(pos, otherPos);
            if(currentDistance < newSegDist) continue;

            closestSeg = segment;
            currentDistance = newSegDist;
        }

        if(closestSeg) closestSeg.SetSegment(Notes);
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

    /// <summary>
    /// Attempts to get the collider center of an object, returns its transform position if unavailable.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    private static Vector2 AttemptGetCenter(GameObject obj)
    {
        Collider2D collider = obj.GetComponent<Collider2D>();
        Vector2 pos = obj.transform.position;
        if(collider) pos = collider.bounds.center;
        return pos;
    }
}