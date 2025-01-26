using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMove : MonoBehaviour
{
    [SerializeField] private float distanceRemove;
    private GroundManager manager;
    
    private void Start()
    {
        manager = FindObjectOfType<GroundManager>();
    }
    private void Update()
    {
        if(transform.position.z <= distanceRemove)
        {
            manager.Remove(gameObject);
        }
        transform.position += new Vector3(0, 0, -manager.Speed * Time.deltaTime);
    }
}
