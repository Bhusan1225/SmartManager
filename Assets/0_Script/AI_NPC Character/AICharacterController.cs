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

    AICharacterModel aiModel;
    AICharacterView  aiCharacterView;

    NPCDepenpencies npcDepenpencies;


    public AICharacterController(GameObject _NPCCharacterModel, List<Transform> _waypoints, float _moveSpeed, float _rotationSpeed, NPCDepenpencies _npcDepenpencies)
    {
        aiModel = new AICharacterModel(_NPCCharacterModel, _waypoints, _moveSpeed, _rotationSpeed);
        StartMoving();
        this.npcDepenpencies = _npcDepenpencies;
    }



    void StartMoving()
    {
        aiModel.waypointIndex = 0;
        aiModel.isMoving = true;
        
    }

    
    public void Update()
    {
        AIMovement();
        PickupProduct();
        PayCash();
    }

    void AIMovement()
    {
        if (!aiModel.isMoving)
        {
            return;
        }

        if (aiModel.waypointIndex < aiModel.waypoints.Count)
        {
            aiModel.NPCCharacterModel.transform.position = Vector3.MoveTowards(aiModel.NPCCharacterModel.transform.position, aiModel.waypoints[aiModel.waypointIndex].position, Time.deltaTime * aiModel.moveSpeed); // first movement to first point

            //roation
            var direction = aiModel.NPCCharacterModel.transform.position - aiModel.waypoints[aiModel.waypointIndex].position;
            var targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            aiModel.NPCCharacterModel.transform.rotation = Quaternion.Lerp(aiModel.NPCCharacterModel.transform.rotation, targetRotation, Time.deltaTime * aiModel.rotationSpeed); // smooth rotation towards the waypoint


            // seting the distance
            var distance = Vector3.Distance(aiModel.NPCCharacterModel.transform.position, aiModel.waypoints[aiModel.waypointIndex].position);

            if (distance <= 0.05f)
            {
                aiModel.waypointIndex++;


                if (aiModel.isLoop && aiModel.waypointIndex >= aiModel.waypoints.Count)
                {

                    aiModel.waypointIndex = 0; // loop back to the first waypoint

                }
            }
        }
        
    }

    async void PickupProduct()
    {
        if( ShopRackInRange())
        {
            Debug.Log("Pickup product from the shop rack.");
            aiModel.moveSpeed = 0f; // Stop moving while buying
            aiModel.isMoving = false;

            // Wait for 5 seconds asynchronously
            await Task.Delay(5000);
            AddedToCart();

        }
    }

    private void AddedToCart()
    {
        Debug.Log("Finished shopping. Resuming movement.");
        aiModel.isMoving = true;
        aiModel.moveSpeed = 1f; // Resume moving after buying
    }

    bool ShopRackInRange()
    {
        float buyingRadious = 0.3f;
        Collider[] hitColliders = Physics.OverlapSphere(aiModel.NPCCharacterModel.transform.position, buyingRadious);
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
        if (CashCounterInRange() && ManagerInRange())
        {
            Debug.Log("Pay the required amount");
            aiModel.moveSpeed = 0f; // Stop moving while buying
            
            // Implement payment logic here
             npcDepenpencies.GetEmployeeView.GetEmployeeController.TransferPayment(10);
            // Wait for 2 seconds asynchronously
            await Task.Delay(2000);
            PurchesDone();

        }
        else if (CashCounterInRange()) 
        {
            Debug.Log("Wait for the Manager for the payment");
            aiModel.moveSpeed = 0f;
        }
        else
        {
            aiModel.moveSpeed = 1f;
        }
    }

    private void PurchesDone()
    {
        aiModel.moveSpeed = 1f; // Resume moving after purchesing the product
    }

    bool CashCounterInRange()
    {
        float purchesRadious = 0.3f;
        Collider[] hitColliders = Physics.OverlapSphere(aiModel.NPCCharacterModel.transform.position, purchesRadious);
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

    bool ManagerInRange()
    {
        float purchesRadious = 1.1f;
        Collider[] hitColliders = Physics.OverlapSphere(aiModel.NPCCharacterModel.transform.position, purchesRadious);
        {
            foreach (Collider collider in hitColliders)
            {
                if (collider.gameObject.layer == 9)
                {
                    Debug.Log("Manager is in range: " + collider.name);
                    return true; // Shop Rack found in range
                }


            }
        }

        //Debug.Log("No Shop Rack in range.");
        return false; // None were found
    }



}
