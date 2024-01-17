using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvManager : MonoBehaviour
{
    //Grab unlock vars
    public static bool isTaserUnlocked;
    public static bool isBatonUnlocked;

    public static int equippedNum;

    [SerializeField] Sprite activeTaser, inactiveTaser, activeBaton, inactiveBaton;
    SavePointScriptable[] savedVars;
    int saveIndex;
    [Space]
    [SerializeField] Image activeImg;
    [SerializeField] Image inactiveImg;
    [Header("Move Vals")]
    [SerializeField] float imgMoveSpeed;
    [SerializeField] float imgMoveTime;
    [SerializeField] float imgMoveDist;

    int newEquipped;

    bool isMovingImgAway, isMovingImgBack;

    Vector3 activeImgTargetPos, inactiveImgTargetPos;

    void Awake()
    {
        //Ensure nothing equipped on start
        equippedNum = 0;

        //Grab saved index
        saveIndex = PlayerPrefs.GetInt("SaveIndex", 0);

        //Grab saved inventory stats
        Object[] allSavedVars = Resources.LoadAll("SaveData");
        savedVars = new SavePointScriptable[allSavedVars.Length];
        allSavedVars.CopyTo(savedVars, 0);

        //Assign values
        isTaserUnlocked = savedVars[saveIndex].isTaserUnlocked;
        isBatonUnlocked = savedVars[saveIndex].isBatonUnlocked;
    }

    void Update()
    {
        if(!isMovingImgAway && !isMovingImgBack)
        {
            if(Input.GetKeyDown(KeyCode.F) && isTaserUnlocked)
            {
                SetTargetPos(1);
            }
            else if(Input.GetKeyDown(KeyCode.G) && isBatonUnlocked)
            {
                SetTargetPos(2);
            }
            else if(Input.GetKeyDown(KeyCode.H))
            {
                SetTargetPos(0);
            }
        }

        MoveImages();
    }

    void SwitchIcons(int newEquipped)
    {
        if(newEquipped == 0)
        {
            //Reduce opacity of active image
            activeImg.color = new Color32(255, 255, 255, 15);
            activeImg.sprite = null;

            //Set inactive image
            if(equippedNum == 1)
            {
                inactiveImg.sprite = inactiveTaser;
            }
            else if(equippedNum == 2)
            {
                inactiveImg.sprite = inactiveBaton;
            }
        }
        else
        {
            //Ensure full opacity
            activeImg.color = new Color(255, 255, 255, 235);

            //Set primary and secondary icons
            if(newEquipped == 1)
            {
                activeImg.sprite = activeTaser;
                inactiveImg.sprite = inactiveBaton;
            }
            else
            {
                activeImg.sprite = activeBaton;
                inactiveImg.sprite = inactiveTaser;
            }
        }

        //Update global var
        equippedNum = newEquipped;
    }

    void MoveImages()
    {
        if(isMovingImgAway || isMovingImgBack)
        {
            var step =  imgMoveSpeed * Time.deltaTime;

            activeImg.transform.position = Vector3.MoveTowards(activeImg.transform.position, activeImgTargetPos, step);
            inactiveImg.transform.position = Vector3.MoveTowards(inactiveImg.transform.position, inactiveImgTargetPos, step);

            if(Vector2.Distance(activeImg.transform.position, activeImgTargetPos) < .5f)
            {
                if(isMovingImgAway)
                {
                    isMovingImgAway = false;
                    isMovingImgBack = true;
                    SwitchIcons(newEquipped);
                    SetTargetPos(newEquipped);
                }
                else
                {
                    isMovingImgBack = false;
                }
            }
        }
    }

    void SetTargetPos(int equipping)
    {
        newEquipped = equipping;

        if(isMovingImgAway)
        {
            //Set target pos
            activeImgTargetPos = new Vector3(activeImg.transform.position.x - imgMoveDist, activeImg.transform.position.y, activeImg.transform.position.z);
            inactiveImgTargetPos = new Vector3(inactiveImg.transform.position.x - imgMoveDist, inactiveImg.transform.position.y, inactiveImg.transform.position.z);
        }
        else if(isMovingImgBack)
        {
            //Set target pos
            activeImgTargetPos = new Vector3(activeImg.transform.position.x + imgMoveDist, activeImg.transform.position.y, activeImg.transform.position.z);
            inactiveImgTargetPos = new Vector3(inactiveImg.transform.position.x + imgMoveDist, inactiveImg.transform.position.y, inactiveImg.transform.position.z);
        }
        else
        {
            isMovingImgAway = true;
            SetTargetPos(newEquipped);
        }
    }
}
