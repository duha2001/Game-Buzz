using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathDefinition : MonoBehaviour {

    public Transform[] Points; //tạo tập hợp điểm

    public IEnumerator<Transform> GetPathsEnumerator() //IEnumerable Là một mảng read-only, duyệt theo một chiều, từ đầu tới cuối mảng.
    {
        if (Points == null || Points.Length < 1)
        {
            yield break; // trả về một danh sách read-only (vi su dung IEnum nen phai dung yield)
        }
        if (Points == null || Points.Length < 1)
            {
                yield break;
            }

            var direction = 1;
            var index = 0;
            while(true)
            {
                yield return Points[index];

                if(index <= 0)
                {
                    direction = 1;
                }
                else if(index >= Points.Length - 1)
                {
                    direction = -1;
                }
                index = index + direction;
            }
    }

    public void OnDrawGizmos() //Hàm OnDrawGizmos cho phép hiển thị Physics Raycast trong thẻ Scene để giả lập cho chúng ta các tia ray hiển thị theo đúng hướng của nó
    {
        if(Points == null || Points.Length < 2)
        {
            return;
        }

        var points = Points.Where(t => t != null).ToList();
        if(points.Count < 2)
        {
            return;
        }

        for (var i = 1; i < points.Count; i++)
        {
            Gizmos.DrawLine(points[i - 1].position, points[i].position); //vẽ đường
        }
    }
}
