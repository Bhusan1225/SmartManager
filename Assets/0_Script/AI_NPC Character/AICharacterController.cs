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

    AICharacterModel AiCharacterModel;
    AICharacterView  AiCharacterView;

    NPCDepenpencies npcDepenpencies;


    //public AICharacterController(GameObject _NPCCharacterModel, List<Transform> _waypoints, float _moveSpeed, float _rotationSpeed, NPCDepenpencies _npcDepenpencies)
    //{
    //    AiCharacterModel = new AICharacterModel(_NPCCharacterModel, _waypoints, _moveSpeed, _rotationSpeed);
    //    StartMoving();
    //    this.npcDepenpencies = _npcDepenpencies;
    //}


    public AICharacterController(AICharacterView _view, AICharacterModel _model, NPCDepenpencies _npcDepenpencies)
    {
        this.AiCharacterView = _view;
        this.AiCharacterModel = _model;
       
        StartMoving();
        this.npcDepenpencies = _npcDepenpencies;
    }


    void StartMoving()
    {
        AiCharacterModel.waypointIndex = 0;
        AiCharacterModel.isMoving = true;
        
    }

    
    public void Update()
    {
        AIMovement();
        PickupProduct();
        PayCash();
    }

    void AIMovement()
    {
        if (!AiCharacterModel.isMoving)
        {
            return;
        }

        if (AiCharacterModel.waypointIndex < AiCharacterModel.waypoints.Count)
        {
            AiCharacterModel.NPCCharacterModel.transform.position = Vector3.MoveTowards(AiCharacterModel.NPCCharacterModel.transform.position, AiCharacterModel.waypoints[AiCharacterModel.waypointIndex].position, Time.deltaTime * AiCharacterModel.moveSpeed); // first movement to first point

            //roation
            var direction = AiCharacterModel.NPCCharacterModel.transform.position - AiCharacterModel.waypoints[AiCharacterModel.waypointIndex].position;
            var targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            AiCharacterModel.NPCCharacterModel.transform.rotation = Quaternion.Lerp(AiCharacterModel.NPCCharacterModel.transform.rotation, targetRotation, Time.deltaTime * AiCharacterModel.rotationSpeed); // smooth rotation towards the waypoint


            // seting the distance
            var distance = Vector3.Distance(AiCharacterModel.NPCCharacterModel.transform.position, AiCharacterModel.waypoints[AiCharacterModel.waypointIndex].position);

            if (distance <= 0.05f)
            {
                AiCharacterModel.waypointIndex++;


                if (AiCharacterModel.isLoop && AiCharacterModel.waypointIndex >= AiCharacterModel.waypoints.Count)
                {

                    AiCharacterModel.waypointIndex = 0; // loop back to the first waypoint

                }
            }
        }
        
    }

    async void PickupProduct()
    {
        if( ShopRackInRange())
        {
            Debug.Log("Pickup product from the shop rack.");
            AiCharacterModel.moveSpeed = 0f; // Stop moving while buying
            AiCharacterModel.isMoving = false;

            // Wait for 5 seconds asynchronously
            await Task.Delay(5000);
            AddedToCart();

        }
    }

    private void AddedToCart()
    {
        Debug.Log("Finished shopping. Resuming movement.");
        AiCharacterModel.isMoving = true;
        AiCharacterModel.moveSpeed = 1f; // Resume moving after buying
    }

    bool ShopRackInRange()
    {
        float buyingRadious = 0.3f;
        Collider[] hitColliders = Physics.OverlapSphere(AiCharacterModel.NPCCharacterModel.transform.position, buyingRadious);
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
            AiCharacterModel.moveSpeed = 0f; // Stop moving while buying
            
            // Implement payment logic here
             npcDepenpencies.GetEmployeeView.GetEmployeeController.TransferPayment(10);
            // Wait for 2 seconds asynchronously
            await Task.Delay(2000);
            PurchesDone();

        }
        else if (CashCounterInRange()) 
        {
            Debug.Log("Wait for the Manager for the payment");
            AiCharacterModel.moveSpeed = 0f;
        }
        else
        {
            AiCharacterModel.moveSpeed = 1f;
        }
    }

    private void PurchesDone()
    {
        AiCharacterModel.moveSpeed = 1f; // Resume moving after purchesing the product
    }

    bool CashCounterInRange()
    {
        float purchesRadious = 0.3f;
        Collider[] hitColliders = Physics.OverlapSphere(AiCharacterModel.NPCCharacterModel.transform.position, purchesRadious);
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
        Collider[] hitColliders = Physics.OverlapSphere(AiCharacterModel.NPCCharacterModel.transform.position, purchesRadious);
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
