using UnityEngine;
using System.Collections;

public delegate void MoveDelegate();
public class GUI_PlayerMoveController : MonoBehaviour {



    public static MoveDelegate moveStart;
    public static MoveDelegate moveEnd;
    public static GUI_PlayerMoveController instance;

    [SerializeField]
    private GUI_JoytackController guiJoystackController;
    private Transform selfTransform;
    [SerializeField]
    private bool turnBase = false;
    private float angle;
    [SerializeField]
    private float moveSpeed;

    private Animation playerAnimation;
    void Awake()
    {
        playerAnimation = GetComponent<Animation>();
        playerAnimation["wait"].blendMode = AnimationBlendMode.Blend;
        instance = this;
        selfTransform = this.transform;

        moveStart = OnMoveStart;
        moveEnd = OnMoveEnd;
    }

    void Update()
    {
        if (turnBase)
        {
            //位置的移动
            Vector3 move = guiJoystackController.movePosNorm * Time.deltaTime * moveSpeed;
            selfTransform.localPosition += move;
            //从JoytackController移动方向 算出自身的角度
            angle = Mathf.Atan2(guiJoystackController.movePosNorm.x,
                guiJoystackController.movePosNorm.z) * Mathf.Rad2Deg;
            selfTransform.localRotation = Quaternion.Euler(Vector3.up * angle);

        }
    }


    private void OnMoveEnd()
    {
        turnBase = false;
        playerAnimation.Play("wait");

    }

    private void OnMoveStart()
    {
        turnBase = true;
        playerAnimation.Play("run");
     
    }
}
