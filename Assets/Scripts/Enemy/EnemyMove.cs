using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

    public float speed;

    Vector3 goalPos;

	// Use this for initialization
	void Start () {
        // goalPos = GameObject.FindGameObjectWithTag("Goal").transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        // Move();
	}

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, goalPos, speed);
    }
}
