using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
using System.Threading.Tasks;

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
        PickupProduct();
        PayCash();
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

    async void PickupProduct()
    {
        if( ShopRackInRange())
        {
            Debug.Log("Pickup product from the shop rack.");
            aiCharacter.moveSpeed = 0f; // Stop moving while buying
            // Implement pickup logic here


            // Wait for 2 seconds asynchronously
            await Task.Delay(2000);
            AddedToCart();

        }
    }

    private void AddedToCart()
    {
        Debug.Log("Finished shopping. Resuming movement.");
        aiCharacter.moveSpeed = 1f; // Resume moving after buying
    }

    bool ShopRackInRange()
    {
        float buyingRadious = 0.3f;
        Collider[] hitColliders = Physics.OverlapSphere(aiCharacter.NPCCharacterModel.transform.position, buyingRadious);
        {
            foreach (Collider collider in hitColliders)
            {
                if(collider.gameObject.layer == 7)
                {
                    Debug.Log("Shop Rack in range: " + collider.name);
                    return true; // Shop Rack found in range
                }
                
                
            }
        }

        //Debug.Log("No Shop Rack in range.");
        return false; // None were found
    }


    async void PayCash()
    {
        if (CashCounterInRange())
        {
            Debug.Log("Pay the required amount");
            aiCharacter.moveSpeed = 0f; // Stop moving while buying
            // Implement payment logic here


            // Wait for 2 seconds asynchronously
            await Task.Delay(2000);
            PurchesDone();

        }
    }

    private void PurchesDone()
    {
        aiCharacter.moveSpeed = 1f; // Resume moving after purchesing the product
    }

    bool CashCounterInRange()
    {
        float purchesRadious = 0.3f;
        Collider[] hitColliders = Physics.OverlapSphere(aiCharacter.NPCCharacterModel.transform.position, purchesRadious);
        {
            foreach (Collider collider in hitColliders)
            {
                if (collider.gameObject.layer == 8)
                {
                    Debug.Log("Cash Counter is in range: " + collider.name);
                    return true; // Shop Rack found in range
                }


            }
        }

        //Debug.Log("No Shop Rack in range.");
        return false; // None were found
    }



}
