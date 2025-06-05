using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AICharacterModel 
{

    public GameObject NPCCharacterModel;
    public List<Transform> waypoints = new List<Transform>();
    
    public bool isMoving;
    public int waypointIndex;
    public float moveSpeed = 1f;
    public float rotationSpeed = 5f;
    public bool isLoop = true;

    public AICharacterModel(GameObject _NPCCharacterModel, List<Transform> _waypoints, float _moveSpeed, float _rotationSpeed)
    {
        this.NPCCharacterModel = _NPCCharacterModel;
        this.waypoints = _waypoints;
        this.moveSpeed = _moveSpeed;
        this.rotationSpeed = _rotationSpeed;
        
        
    }


}
