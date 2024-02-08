using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewInvManager : MonoBehaviour
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

    int newEquipped;

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
        if(Input.GetKeyDown(KeyCode.F) && isTaserUnlocked)
        {
            SetTarget(1);
        }
        else if(Input.GetKeyDown(KeyCode.G) && isBatonUnlocked)
        {
            SetTarget(2);
        }
        else if(Input.GetKeyDown(KeyCode.H))
        {
            SetTarget(0);
        }
    }

    void SetTarget(int equipping)
    {
        newEquipped = equipping;

        activeImg.transform.parent.GetComponent<Animator>().Play("Out");
        inactiveImg.transform.parent.GetComponent<Animator>().Play("Out");
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
}
