using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    protected int maxHp;
    public int curHp;
    protected Animator anim;

    protected string bossNickname;
    protected string bossName;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        curHp = maxHp;
        GameManager.instance.bossNickname.text = bossNickname;
        GameManager.instance.bossName.text = bossName;
        GameManager.instance.bossHp.text = curHp + " / " + maxHp;
    }

    public virtual void Hurt()
    {
        curHp--;
        anim.SetTrigger("Hurt");
        GameManager.instance.bossHp.text = curHp + " / " + maxHp;
        if (curHp <= 0) { Die(); }
    }

    protected void Die()
    {
        anim.SetBool("Die", true);
        GameManager.instance.bossSmoke.SetActive(true);
        GameManager.instance.GameOver();
    }
}
