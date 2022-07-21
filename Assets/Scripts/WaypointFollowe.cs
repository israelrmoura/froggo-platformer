using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollowe : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWpIndex = 0;

    [SerializeField] private float speed = 2f;

    private Vector2 currentWpPosition = new Vector2();

    private void Update() {
        currentWpPosition = waypoints[currentWpIndex].transform.position;

        if (Vector2.Distance(currentWpPosition, transform.position) < .1f)
        {
            currentWpIndex++;
            if (currentWpIndex >= waypoints.Length){
                currentWpIndex = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, currentWpPosition, 
        Time.deltaTime * speed);
    }
}
