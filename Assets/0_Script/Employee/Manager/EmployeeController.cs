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


    //public bool AttendCashCounter()
    //{
    //    float checkingRange = 0.4f;
    //    Collider[] hitColliders = Physics.OverlapSphere(employeeModel.employeeCharacterModel.transform.position, checkingRange);

    //    foreach(Collider collider in hitColliders )
    //    {
    //        if (collider.gameObject.layer == 8)
    //        {
    //            Debug.Log("Emplove is present in CashCounter.");

    //            return true; // Cash counter found in range

    //        }

    //    }
    //    return false;
    //}





}
