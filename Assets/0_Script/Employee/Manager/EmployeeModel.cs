using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeModel 
{
    public GameObject employeeCharacterModel;
    public float speed = 5f;
    public int bankBalance;
    public int targetBankBalance;
    public bool isManagerinCashCounter;
    public bool isPaymentTransferred;


    public EmployeeModel(GameObject _employeeCharacterModel, float _speed)
    {
        employeeCharacterModel = _employeeCharacterModel;
        speed = _speed;
        
        isManagerinCashCounter = false;
        isPaymentTransferred = false;
    }
}
