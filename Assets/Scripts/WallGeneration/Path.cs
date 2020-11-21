﻿using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Path : MonoBehaviour
{
    [SerializeField, HideInInspector]
    private List<Vector2> points;

    [SerializeField, HideInInspector]
    private bool isClosed;

    [SerializeField, HideInInspector]
    private bool autoSetControlPoints;

    public Path(Vector2 centre)
    {
        points = new List<Vector2>
        {
            centre + Vector2.left,
            centre + (Vector2.left + Vector2.up) * 0.5f,
            centre + (Vector2.right + Vector2.down) * 0.5f,
            centre + Vector2.right
        };
    }

    public void GenerateRandomPath()
    {
        float xBound = 10;
        float yBound = 20;

        autoSetControlPoints = true;

        int pointsToAdd = Random.Range(6, 8);

        for (int i = 1; i <= pointsToAdd; i++)
        {
            if (i == pointsToAdd)
            {
                float y = yBound / 2;
                float x = 0;
                Vector2 newPoint = new Vector2(x, y);

                AddSegment(newPoint);
            }
            else
            {
                float y = Random.Range(points[points.Count - 1].y, (yBound / (float)pointsToAdd) * ((float)i) * 0.5f);
                float x = Random.Range(-xBound, xBound) * (y - points[points.Count - 1].y) * 0.05f;
                Vector2 newPoint = new Vector2(x, y);

                AddSegment(newPoint);
            }
        }
    }

    public Vector2 this[int index]
    {
        get
        {
            return points[index];
        }
    }

    public bool IsClosed
    {
        get
        {
            return isClosed;
        }
        set
        {
            if (isClosed != value)
            {
                isClosed = value;

                if (isClosed)
                {
                    points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
                    points.Add(points[0] * 2 - points[1]);

                    if (autoSetControlPoints)
                    {
                        AutoSetAnchorControlPoints(0);
                        AutoSetAnchorControlPoints(points.Count - 3);
                    }
                }
                else
                {
                    points.RemoveRange(points.Count - 2, 2);

                    if (autoSetControlPoints)
                    {
                        AutoSetStartAndEndControls();
                    }
                }
            }
        }
    }

    public bool AutoSetControlPoints
    {
        get
        {
            return autoSetControlPoints;
        }
        set
        {
            if (autoSetControlPoints != value)
            {
                autoSetControlPoints = value;
                if (autoSetControlPoints)
                {
                    AutoSetAllControlPoints();
                }
            }
        }
    }

    public int NumPoints 
    {   
        get 
        { 
            return points.Count; 
        } 
    }

    public int numSegments
    {
        get
        {
            return points.Count / 3;
        }
    }

    public void AddSegment(Vector2 anchorPos)
    {
        points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
        points.Add((points[points.Count - 1] + anchorPos) * 0.5f);
        points.Add(anchorPos);

        if (autoSetControlPoints)
        {
            AutoSetAllAffectedControlPoints(points.Count - 1);
        }
    }

    public void SplitSegment(Vector2 anchorPos, int segmentIndex)
    {
        points.InsertRange(segmentIndex * 3 + 2, new Vector2[] { Vector2.zero, anchorPos, Vector2.zero });
        if (autoSetControlPoints)
        {
            AutoSetAllAffectedControlPoints(segmentIndex * 3 + 3);
        }
        else
        {
            AutoSetAnchorControlPoints(segmentIndex * 3 + 3);
        }
    }

    public void DeleteSegment(int anchorIndex)
    {
        if (numSegments > 2 || (!isClosed && numSegments > 1 ))
        {
            if (anchorIndex == 0)
            {
                if (isClosed)
                {
                    points[points.Count - 1] = points[2];
                }

                points.RemoveRange(0, 3);
            }
            else if (anchorIndex == points.Count - 1 && !isClosed)
            {
                points.RemoveRange(anchorIndex - 2, 3);
            }
            else
            {
                points.RemoveRange(anchorIndex - 1, 3);
            }
        }
    }

    public Vector2[] GetPointsInSegment(int index)
    {
        return new Vector2[] { points[index * 3], points[index * 3 + 1], points[index * 3 + 2], points[LoopIndex(index * 3 + 3)] };
    }

    public void MovePoint(int index, Vector2 pos)
    {
        Vector2 deltaMove = pos - points[index];

        if (index % 3 == 0 || !autoSetControlPoints)
        {
            points[index] = pos;

            if (autoSetControlPoints)
            {
                AutoSetAllAffectedControlPoints(index);
            }
            else
            {
                if (index % 3 == 0)
                {
                    if (index + 1 < points.Count || isClosed)
                    {
                        points[LoopIndex(index + 1)] += deltaMove;
                    }

                    if (index - 1 >= 0 || isClosed)
                    {
                        points[LoopIndex(index - 1)] += deltaMove;
                    }
                }
                else
                {
                    bool nextPointIsAnchor = (index + 1) % 3 == 0;
                    int correspondingControlIndex = (nextPointIsAnchor) ? index + 2 : index - 2;
                    int anchorIndex = (nextPointIsAnchor) ? index + 1 : index - 1;

                    if (correspondingControlIndex >= 0 && correspondingControlIndex < points.Count || isClosed)
                    {
                        float distance = (points[LoopIndex(anchorIndex)] - points[LoopIndex(correspondingControlIndex)]).magnitude;
                        Vector2 direction = (points[LoopIndex(anchorIndex)] - pos).normalized;

                        points[LoopIndex(correspondingControlIndex)] = points[LoopIndex(anchorIndex)] + direction * distance;
                    }

                }
            }
        }

       
    }

    public Vector2[] CalculateEvenlySpacedPoints(float spacing, float resolution = 1)
    {
        List<Vector2> evenlySpacedPoints = new List<Vector2>();
        evenlySpacedPoints.Add(points[0]);

        Vector2 previousPoint = points[0];
        float distSinceLastEvenPoint = 0;

        for (int segmentIndex = 0; segmentIndex < numSegments; segmentIndex++)
        {
            Vector2[] p = GetPointsInSegment(segmentIndex);

            float controlNetLength = Vector2.Distance(p[0], p[1]) + Vector2.Distance(p[1], p[2]) + Vector2.Distance(p[2], p[3]);
            float estimatedCurveLength = Vector2.Distance(p[0], p[3]) + (controlNetLength / 2f);
            int divisions = Mathf.CeilToInt(estimatedCurveLength * resolution * 10);

            float t = 0;

            while (t <= 1)
            {
                t += 1f/divisions;

                Vector2 pointOnCurve = Bezier.EvaluateCubic(p[0], p[1], p[2], p[3], t);
                distSinceLastEvenPoint += Vector2.Distance(previousPoint, pointOnCurve);

                while (distSinceLastEvenPoint >= spacing)
                {
                    float overShotDistance = distSinceLastEvenPoint - spacing;
                    Vector2 newEvenlySpacedPoint = pointOnCurve + (previousPoint - pointOnCurve).normalized * overShotDistance;
                    evenlySpacedPoints.Add(newEvenlySpacedPoint);
                    distSinceLastEvenPoint = overShotDistance;

                    previousPoint = newEvenlySpacedPoint;
                }

                previousPoint = pointOnCurve;
            }
        }

        return evenlySpacedPoints.ToArray();
    }

    void AutoSetAllAffectedControlPoints(int updatedAnchorIndex)
    {
        for (int i = updatedAnchorIndex - 3; i <= updatedAnchorIndex + 3 ; i += 3)
        {
            if (i >= 0 && i < points.Count || isClosed)
            {
                AutoSetAnchorControlPoints(LoopIndex(i));
            }
        }

        AutoSetStartAndEndControls();
    }

    void AutoSetAllControlPoints()
    {
        for (int i = 0; i < points.Count; i += 3)
        {
            AutoSetAnchorControlPoints(i);
        }

        AutoSetStartAndEndControls();
    }

    void AutoSetAnchorControlPoints(int anchorIndex)
    {
        Vector2 anchorPos = points[anchorIndex];
        Vector2 dir = Vector2.zero;
        float[] neighbourDistances = new float[2];

        if (anchorIndex - 3 >= 0 || isClosed)
        {
            Vector2 offset = points[LoopIndex(anchorIndex - 3)] - anchorPos;
            dir += offset.normalized;
            neighbourDistances[0] = offset.magnitude;
        }

        if (anchorIndex + 3 >= 0 || isClosed)
        {
            Vector2 offset = points[LoopIndex(anchorIndex + 3)] - anchorPos;
            dir -= offset.normalized;
            neighbourDistances[1] = -offset.magnitude;
        }

        dir.Normalize();

        for (int i = 0; i < 2; i++)
        {
            int controlIndex = anchorIndex + i * 2 - 1;
            if (controlIndex >= 0 && controlIndex < points.Count || isClosed)
            {
                points[LoopIndex(controlIndex)] = anchorPos + dir * neighbourDistances[i] * 0.5f;
            }
        }
    }

    void AutoSetStartAndEndControls()
    {
        if (!isClosed)
        {
            points[1] = (points[0] + points[2]) * 0.5f;
            points[points.Count - 2] = (points[points.Count - 1] + points[points.Count - 3]) * 0.5f;
        }
    }

    int LoopIndex(int index)
    {
        return (index + points.Count) % points.Count;
    }
}
