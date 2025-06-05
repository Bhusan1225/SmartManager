using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;



public class EmployeeController 
{
  
    EmployeeModel employeeModel;
    EmployeeView employeeView;

    //[Header("Employee View")]
    Rigidbody rigidbody;
    // FixedJoystick joystick;
    //TextMeshProUGUI bankBalanceText;
    //[SerializeField] TextMeshProUGUI targetBankBalanceText;
    //[SerializeField] GameLevel CurrentLevel = GameLevel.None;
    //[SerializeField] EmployeeType employeeType = EmployeeType.None;


    // Start is called before the first frame update
    public EmployeeController(GameObject _employeeModel, float _speed )
    {
        this.employeeModel = new EmployeeModel(_employeeModel, _speed);
        this.rigidbody = employeeModel.employeeCharacterModel.GetComponent<Rigidbody>();
       
        this.employeeModel.bankBalance = 0;
        this.employeeModel.targetBankBalance = SetTargetBankBalance();
        startingStats();
    }

    public void startingStats()
    {
        if (employeeView.BankBalanceText != null)
        {
            employeeView.BankBalanceText.text = "Bank Balance: " + employeeModel.bankBalance.ToString();
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
                return employeeModel.targetBankBalance = 500;
                
            case GameLevel.Level2:
                return employeeModel.targetBankBalance = 1000;
               
            case GameLevel.Level3:
                return employeeModel.targetBankBalance = 1500;
               
            default:
                return employeeModel.targetBankBalance = 0;
                
        }
    }

    public int GetBankBalance()=> employeeModel.bankBalance;

    public void TransferPayment(int amount)
    {
        if(!employeeModel.isPaymentTransferred)
        {
            employeeModel.bankBalance += amount;
            if (employeeView.BankBalanceText != null)
            {
                employeeView.BankBalanceText.text = "Bank Balance: " + employeeModel.bankBalance.ToString();
            }
            employeeModel.isPaymentTransferred = true; 
        }
       
    }
    public void Update()
    {
        Levelup();
    }
    public void FixedUpdate()
    {
        Movement();
    }


    void Movement()
    {
        
        rigidbody.velocity = new Vector3(employeeView.Joystick.Horizontal * employeeModel.speed, rigidbody.velocity.y, employeeView.Joystick.Vertical * employeeModel.speed);

        if(employeeView.Joystick.Horizontal != 0 || employeeView.Joystick.Vertical != 0)
        {
            employeeModel.employeeCharacterModel.transform.rotation = Quaternion.LookRotation(rigidbody.velocity);
        }   

    }

    public void Levelup()
    {
        if (employeeModel.bankBalance == employeeModel.targetBankBalance)
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
