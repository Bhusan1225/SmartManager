using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class AICharacterController 
{

    AICharacterModel aiCharacter;
    AICharacterView  aiCharacterView;

    public AICharacterController(GameObject _NPCCharacterModel, List<Transform> _waypoints, float _moveSpeed, float _rotationSpeed)
    {
       aiCharacter = new AICharacterModel(_NPCCharacterModel,_waypoints, _moveSpeed, _rotationSpeed);
       StartMoving();
    }



    void StartMoving()
    {
        aiCharacter.waypointIndex = 0;
        aiCharacter.isMoving = true;
        
    }

    
    public void Update()
    {
        AIMovement();
    }

    void AIMovement()
        {
        if (!aiCharacter.isMoving)
        {
            return;
        }

        if (aiCharacter.waypointIndex < aiCharacter.waypoints.Count)
        {
            aiCharacter.NPCCharacterModel.transform.position = Vector3.MoveTowards(aiCharacter.NPCCharacterModel.transform.position, aiCharacter.waypoints[aiCharacter.waypointIndex].position, Time.deltaTime * aiCharacter.moveSpeed); // first movement to first point

            //roation
            var direction = aiCharacter.NPCCharacterModel.transform.position - aiCharacter.waypoints[aiCharacter.waypointIndex].position;
            var targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            aiCharacter.NPCCharacterModel.transform.rotation = Quaternion.Lerp(aiCharacter.NPCCharacterModel.transform.rotation, targetRotation, Time.deltaTime * aiCharacter.rotationSpeed); // smooth rotation towards the waypoint


            // seting the distance
            var distance = Vector3.Distance(aiCharacter.NPCCharacterModel.transform.position, aiCharacter.waypoints[aiCharacter.waypointIndex].position);

            if (distance <= 0.05f)
            {
                aiCharacter.waypointIndex++;


                if (aiCharacter.isLoop && aiCharacter.waypointIndex >= aiCharacter.waypoints.Count)
                {

                    aiCharacter.waypointIndex = 0; // loop back to the first waypoint

                }
            }
        }
        
    }
}
