using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyMana : MonoBehaviour
{
    public static EnemyMana ins;

    //private Mana mana;
    public const int MANA_MAX = 100;
    private float manaAmount;
    public float manaRegenAmount = 200;
    // public Image powerBar;
    public TextMeshProUGUI manaPower;
    public float manaCurrent;
    public bool isFullManaBar;
    private bool isUpdateManaBar;
    public bool canAddMana = false;
    private void Awake()
    {
        ins = this;

        //mana = new Mana();
        //powerBar.fillAmount = 0.2f;
        manaCurrent = 0;
        manaPower.text = manaCurrent.ToString();
    }
    private void Start()
    {
        UpdataManabar();
    }
    private void Update()
    {
        //mana.Update();
        //powerBar.fillAmount = mana.GetManaNormalized();
        manaPower.text = manaCurrent.ToString();
        CheckManaFull();


    }
    IEnumerator UpdataManaBar()
    {
        while (manaCurrent < 5)
        {
            canAddMana = true;

            // Debug.Log(manaCurrent);
            yield return new WaitForSeconds(2f);
            //mana.Update();
            //powerBar.fillAmount = mana.GetManaNormalized();
            manaCurrent++;
            UIManager.ins.UpdateManaPowerPlayer(manaCurrent / 5f);
            //manaPower.text = manaCurrent.ToString();

        }
    }
    private void CheckManaFull()
    {
        if (manaCurrent == 5)
        {
            isFullManaBar = true;
        }
        else
        {
            isFullManaBar = false;
        }
    }
    public void UpdataManabar()
    {
        StartCoroutine(UpdataManaBar());
    }
    public void AddManaBar()
    {
        manaAmount += manaRegenAmount * Time.deltaTime;
        //powerBar.fillAmount = manaAmount / MANA_MAX;
    }
}
