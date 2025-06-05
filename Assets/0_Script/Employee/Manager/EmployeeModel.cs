using UnityEngine;

public class EmployeeModel
{
    // Private fields
    private GameObject employeeCharacterModel;
    private float speed = 5f;
    private int bankBalance;
    private int targetBankBalance;
    private bool isManagerInCashCounter;
    private bool isPaymentTransferred;
    private GameLevel currentLevel;
    private EmployeeType employeeType;

   
    public GameObject EmployeeCharacterModel
    {
        get => employeeCharacterModel;
        set => employeeCharacterModel = value;
    }

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    public int BankBalance
    {
        get => bankBalance;
        set => bankBalance = value;
    }

    public int TargetBankBalance
    {
        get => targetBankBalance;
        set => targetBankBalance = value;
    }

    public bool IsManagerInCashCounter
    {
        get => isManagerInCashCounter;
        set => isManagerInCashCounter = value;
    }

    public bool IsPaymentTransferred
    {
        get => isPaymentTransferred;
        set => isPaymentTransferred = value;
    }

    public GameLevel CurrentLevel
    {
        get => currentLevel;
        set => currentLevel = value;
    }

    public EmployeeType EmployeeType
    {
        get => employeeType;
        set => employeeType = value;
    }

    
    public EmployeeModel(GameObject _model, float _speed, GameLevel _currentLevel, EmployeeType _employeeType)
    {
        this.employeeCharacterModel = _model;
        this.speed = _speed;
        this.currentLevel = _currentLevel;
        this.employeeType = _employeeType;
    }
}

