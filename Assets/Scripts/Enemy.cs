using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform Target;
    private int wayPointIndex = 0;

    public int enemyIndex;

    public Transform[] wayPoints;

    public Transform player;

    public float range;
    public float speed;

    public bool boss;

    void Start()
    {
        Target = wayPoints[wayPointIndex];
    }

    void Update()
    {
        Vector3 dir = Target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(Target.position,transform.position) <= 0.4f)
        {
            getNextWayPoint();
        }
        if (Vector3.Distance(player.position, transform.position) < range)
        {
            Target = player;
        }
        if (Vector3.Distance(player.position, transform.position) > range)
        {
            Target = wayPoints[wayPointIndex];
        }
    }

    private void getNextWayPoint()
    {
        if (wayPointIndex >= wayPoints.Length - 1)
            wayPointIndex = 0;
        else
            wayPointIndex++;
        Target = wayPoints[wayPointIndex];
    }
}
