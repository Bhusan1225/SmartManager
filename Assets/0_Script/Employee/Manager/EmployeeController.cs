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
       
    }
    public void update()
    {
        Levelup();
        
    }
    public void FixedUpdate()
    {
        Movement();
    }


    void Movement()
    {

        rigidbody.velocity = new Vector3(employeeView.Joystick.Horizontal * employeeModel.Speed, rigidbody.velocity.y, employeeView.Joystick.Vertical * employeeModel.Speed);

        if(employeeView.Joystick.Horizontal != 0 || employeeView.Joystick.Vertical != 0)
        {
            employeeModel.EmployeeCharacterModel.transform.rotation = Quaternion.LookRotation(rigidbody.velocity);
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
