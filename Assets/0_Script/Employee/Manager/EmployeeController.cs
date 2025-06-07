using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;



public class EmployeeController 
{
  
    EmployeeModel employeeModel;
    EmployeeView employeeView;

    Rigidbody rigidbody;
    


    public EmployeeController(EmployeeView view, EmployeeModel model)
    {
        this.rigidbody = model.EmployeeCharacterModel.GetComponent<Rigidbody>();
        this.employeeView = view;
        this.employeeModel = model;

        SetTargetBankBalance();
        startingStats();
    }

    public EmployeeModel GetEmployeeModel
    {
        get => employeeModel;
        set => employeeModel = value;
    }
    public void startingStats()
    {
        this.employeeModel.BankBalance = 0;
        this.employeeModel.TargetBankBalance = SetTargetBankBalance();
        if (employeeView.BankBalanceText != null)
        {
            employeeView.BankBalanceText.text = "Bank Balance: " + employeeModel.BankBalance.ToString();
        }


        if (employeeView.TargetBankBalanceText != null)
        {

            employeeView.TargetBankBalanceText.text = "Target Bank Balance: " + SetTargetBankBalance().ToString();
        }

        // for Employee movement
        employeeModel.WaypointIndex = 0;
        employeeModel.IsMoving = false;
        employeeModel.IsLoop = true;
        employeeModel.IsReadyForNewLoop = true;
    }

    int SetTargetBankBalance()
    {
        switch (employeeView.CurrentLevel)
        {
            case GameLevel.Level1:
                return employeeModel.TargetBankBalance = 500;
                
            case GameLevel.Level2:
                return employeeModel.TargetBankBalance = 1000;
               
            case GameLevel.Level3:
                return employeeModel.TargetBankBalance = 1500;
               
            default:
                return employeeModel.TargetBankBalance = 0;
                
        }
    }

   

    public void TransferPayment(int amount)
    {
        if(!employeeModel.IsPaymentTransferred)
        {
            employeeModel.BankBalance += amount;
            if (employeeView.BankBalanceText != null)
            {
                employeeView.BankBalanceText.text = "Bank Balance: " + employeeModel.BankBalance.ToString();
            }
            employeeModel.IsPaymentTransferred = true; 
        }
        //employeeModel.IsPaymentTransferred = false;
    }
    public void update()
    {
        Levelup();
        EmployeeMovement();
        
    }
    public void FixedUpdate()
    {
        if(employeeModel.EmployeeType == EmployeeType.Manager)
        {
            Movement();
        }

    }


    void Movement()
    {

        rigidbody.velocity = new Vector3(employeeView.Joystick.Horizontal * employeeModel.Speed, rigidbody.velocity.y, employeeView.Joystick.Vertical * employeeModel.Speed);

        if(employeeView.Joystick.Horizontal != 0 || employeeView.Joystick.Vertical != 0)
        {
            employeeModel.EmployeeCharacterModel.transform.rotation = Quaternion.LookRotation(rigidbody.velocity);
        }   

    }
    void StartOneLoop()
    {
        employeeModel.IsMoving = true;
        employeeModel.WaypointIndex = 0;
        employeeModel.IsReadyForNewLoop = false; 
    }
    void EmployeeMovement()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (employeeModel.IsReadyForNewLoop)
            {
                StartOneLoop();
            }
        }

        if (!employeeModel.IsMoving)
            {
                return;
            }

            if (employeeModel.WaypointIndex < employeeModel.Waypoints.Count)
            {
                employeeModel.EmployeeCharacterModel.transform.position = Vector3.MoveTowards(employeeModel.EmployeeCharacterModel.transform.position, employeeModel.Waypoints[employeeModel.WaypointIndex].position, Time.deltaTime * employeeModel.MoveSpeed); // first movement to first point

                //roation
                var direction = employeeModel.EmployeeCharacterModel.transform.position - employeeModel.Waypoints[employeeModel.WaypointIndex].position;
                var targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                employeeModel.EmployeeCharacterModel.transform.rotation = Quaternion.Lerp(employeeModel.EmployeeCharacterModel.transform.rotation, targetRotation, Time.deltaTime * employeeModel.RotationSpeed); // smooth rotation towards the waypoint


                // seting the distance
                var distance = Vector3.Distance(employeeModel.EmployeeCharacterModel.transform.position, employeeModel.Waypoints[employeeModel.WaypointIndex].position);

                if (distance <= 0.05f)
                {
                    employeeModel.WaypointIndex++;


                    if (employeeModel.IsLoop && employeeModel.WaypointIndex >= employeeModel.Waypoints.Count)
                    {
                       employeeModel.IsMoving = false;
                       employeeModel.WaypointIndex = 0; // loop back to the first waypoint
                       employeeModel.IsReadyForNewLoop = true;
                    }
                }
            }
        

    }
    

    public void Levelup()
    {
        if (employeeModel.BankBalance == employeeModel.TargetBankBalance)
        {
            //load next level or scene
        }
    }
    /// <summary>
    /// /////////////////////////////////////////////
    /// </summary>

    
    public void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.layer == 6)
        {
            Rack rack = collision.gameObject.GetComponent<Rack>();
            if (rack != null && employeeModel.Cart.Count != 2)
            {
                employeeModel.NearbyRack = rack;
                rack.RemoveProduct(employeeModel.Product);
                Debug.Log("Product removed from rack: ");
                AddProductToCart(employeeModel.Product);
                Debug.Log("Product added to cart ");
            }
            rack = null;
        }

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            bool isCartEmpty = employeeModel.Cart.Count == 0;
            Rack rack = collision.gameObject.GetComponent<Rack>();
            if (rack != null && employeeModel.Cart.Count > 0 && !isCartEmpty)
            {
                Debug.Log("Employee is colliding with shop rack");
                employeeModel.NearbyRack = rack;
                RemoveProductToCart(employeeModel.Product);
                rack.AddProduct(employeeModel.Product);
                //
                //Debug.Log("Product added to cart ");
            }
            //rack = null;
        }
    }


    public void AddProductToCart(ProductSO targetProduct)
    {

            if (targetProduct == employeeView.Product && employeeModel.Cart.Count != 2)
            {
                employeeModel.Cart.Add(targetProduct);
                employeeModel.ProductCount++;
                employeeView.ProductCountText.text = employeeModel.ProductCount.ToString();
            }
            else if (employeeModel.Cart.Count == 2)
            {
                Debug.Log("Cart is full");
            }
        
    }

    public void RemoveProductToCart(ProductSO targetProduct)
    {

        if (targetProduct == employeeView.Product && employeeModel.Cart.Count > 0)
        {
            employeeModel.Cart.Remove(targetProduct);
            employeeModel.ProductCount--;
            employeeView.ProductCountText.text = employeeModel.ProductCount.ToString();
        }
        else if (employeeModel.Cart.Count == 0)
        {
            Debug.Log("Cart is empty");
        }

    }




}
