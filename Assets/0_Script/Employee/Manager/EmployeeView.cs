using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class EmployeeView : MonoBehaviour
{

    [Header("Employee View")]
    [SerializeField] GameObject employeeCharacterModel;
    [SerializeField] int speed;
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] FixedJoystick joystick;
    [SerializeField] TextMeshProUGUI bankBalanceText;
    [SerializeField] TextMeshProUGUI targetBankBalanceText;
    [SerializeField] GameLevel currentLevel = GameLevel.None;
    [SerializeField] EmployeeType employeeType = EmployeeType.None;

    private EmployeeController employeeController;

    // Start is called before the first frame update
    void Start()
    {
       employeeController = new EmployeeController(employeeCharacterModel, speed);
    }

    // Update is called once per frame
    void Update()
    {
        employeeController.Update();
    }

    private void FixedUpdate()
    {
        employeeController.FixedUpdate();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(employeeCharacterModel.transform.position, 0.4f);
    }

    public Rigidbody Rigidbody => rigidbody;

    public FixedJoystick Joystick => joystick;

    public TextMeshProUGUI BankBalanceText => bankBalanceText;

    public TextMeshProUGUI TargetBankBalanceText => targetBankBalanceText;

    public GameLevel CurrentLevel => currentLevel;

    public EmployeeType EmployeeType => employeeType;
}
