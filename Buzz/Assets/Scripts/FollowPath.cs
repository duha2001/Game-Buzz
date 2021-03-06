using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour {
    // sao cái thanh taskbar của bạn, mình k ấn được vậy?

    public enum FollowType
    {
        MoveTowards,
        Lerp
    }

    public FollowType Type = FollowType.MoveTowards;
    public PathDefinition Path;
    public float Speed = 1;
    public float MaxDistanceToGoal = .1f;

    private IEnumerator<Transform> _currentPoint;

    public void Start()
    {
        if(Path == null)
        {
            Debug.LogError("Path con not be null", gameObject);
            return;
        }

        _currentPoint = Path.GetPathsEnumerator();
        _currentPoint.MoveNext();

        if(_currentPoint.Current == null)
        {
            return;
        }

        transform.position = _currentPoint.Current.position;
    }

    public void Update()
    {
        if(_currentPoint == null || _currentPoint.Current ==  null)
        {
            return;
        }
        if(Type == FollowType.MoveTowards)
        {
            transform.position = Vector3.MoveTowards(transform.position, _currentPoint.Current.position, Time.deltaTime * Speed); //di chuyen theo duong theo kieu MoveTowards
        }
        else if(Type == FollowType.Lerp)
        {
            transform.position = Vector3.Lerp(transform.position, _currentPoint.Current.position, Time.deltaTime * Speed); //di chuyen theo duong theo kieu Lerp
        }

        var distanceSquared = (transform.position - _currentPoint.Current.position).sqrMagnitude;
        if(distanceSquared < MaxDistanceToGoal * MaxDistanceToGoal)
        {
            _currentPoint.MoveNext(); //di chuyen ve
        }
    }

}
