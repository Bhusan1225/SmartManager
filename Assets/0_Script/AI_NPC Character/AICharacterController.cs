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
        AiCharacterModel.WaypointIndex = 0;
        AiCharacterModel.IsMoving = true;
        
    }

    
    public void Update()
    {
        AIMovement();
        PickupProduct();
        PayCash();
    }

    void AIMovement()
    {
        if (!AiCharacterModel.IsMoving)
        {
            return;
        }

        if (AiCharacterModel.WaypointIndex < AiCharacterModel.Waypoints.Count)
        {
            AiCharacterModel.NPCCharacterModel.transform.position = Vector3.MoveTowards(AiCharacterModel.NPCCharacterModel.transform.position, AiCharacterModel.Waypoints[AiCharacterModel.WaypointIndex].position, Time.deltaTime * AiCharacterModel.MoveSpeed); // first movement to first point

            //roation
            var direction = AiCharacterModel.NPCCharacterModel.transform.position - AiCharacterModel.Waypoints[AiCharacterModel.WaypointIndex].position;
            var targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            AiCharacterModel.NPCCharacterModel.transform.rotation = Quaternion.Lerp(AiCharacterModel.NPCCharacterModel.transform.rotation, targetRotation, Time.deltaTime * AiCharacterModel.RotationSpeed); // smooth rotation towards the waypoint


            // seting the distance
            var distance = Vector3.Distance(AiCharacterModel.NPCCharacterModel.transform.position, AiCharacterModel.Waypoints[AiCharacterModel.WaypointIndex].position);

            if (distance <= 0.05f)
            {
                AiCharacterModel.WaypointIndex++;


                if (AiCharacterModel.IsLoop && AiCharacterModel.WaypointIndex >= AiCharacterModel.Waypoints.Count)
                {

                    AiCharacterModel.WaypointIndex = 0; // loop back to the first waypoint

                }
            }
        }
        
    }

    async void PickupProduct()
    {
        if( ShopRackInRange())
        {
            Debug.Log("Pickup product from the shop rack.");
            AiCharacterModel.MoveSpeed = 0f; // Stop moving while buying
            AiCharacterModel.IsMoving = false;

            // Wait for 5 seconds asynchronously
            await Task.Delay(1000);
            AddedToCart();

        }
    }

    private void AddedToCart()
    {
        Debug.Log("Finished shopping. Resuming movement.");
        AiCharacterModel.IsMoving = true;
        AiCharacterModel.MoveSpeed = 1f; // Resume moving after buying
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
            AiCharacterModel.MoveSpeed = 0f; // Stop moving while buying
            
            // Implement payment logic here
             npcDepenpencies.GetEmployeeView.GetEmployeeController.TransferPayment(30);
            // Wait for 2 seconds asynchronously
            await Task.Delay(2000);
            PurchesDone();

        }
        else if (CashCounterInRange()) 
        {
            Debug.Log("Wait for the Manager for the payment");
            AiCharacterModel.MoveSpeed = 0f;
        }
        else
        {
            AiCharacterModel.MoveSpeed = 1f;
        }
    }

    private void PurchesDone()
    {
        AiCharacterModel.MoveSpeed = 1f; // Resume moving after purchesing the product
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


    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 7) // Assuming layer 7 is for the shop rack
        {
            Debug.Log("AI Character entered the shop rack area.");
            Rack rack = collision.gameObject.GetComponent<Rack>();
            if (rack != null && AiCharacterModel.Cart.Count != 1)
            {
                AiCharacterModel.NearbyRack = rack;
                rack.RemoveProduct(AiCharacterModel.Product);
                Debug.Log("Product removed from rack: " + AiCharacterModel.Product.name);
                AddProductToCart(AiCharacterModel.Product);
                Debug.Log("Product added to cart: " + AiCharacterModel.Product.name);
            }   
        }
        
    }

     public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            RemoveProductToCart(AiCharacterModel.Cart[0]);
        }
    }

    public void AddProductToCart(ProductSO targetProduct)
    {

        if (targetProduct == AiCharacterView.Product && AiCharacterModel.Cart.Count != 2)
        {
            AiCharacterModel.Cart.Add(targetProduct);
            AiCharacterModel.ProductCount++;
            AiCharacterView.ProductCountText.text = AiCharacterModel.ProductCount.ToString();
            AiCharacterView.ProductImage.enabled = true;
        }
        else if (AiCharacterModel.Cart.Count == 2)
        {
            Debug.Log("Cart is full");
        }

    }
    public void RemoveProductToCart(ProductSO targetProduct)
    {

        if (targetProduct == AiCharacterView.Product && AiCharacterModel.Cart.Count > 0)
        {
            AiCharacterModel.Cart.Remove(targetProduct);
            AiCharacterModel.ProductCount--;
            AiCharacterView.ProductCountText.text = AiCharacterModel.ProductCount.ToString();
            AiCharacterView.ProductImage.enabled = false;
        }
        else if (AiCharacterModel.Cart.Count == 0)
        {
            Debug.Log("Cart is empty");
        }

    }


}
