using System.Collections.Generic;
using UnityEngine;

public class Controller_Buffs : MonoBehaviour
{
    public List<Buff> buffs = new List<Buff>();

    public Buff[] buff;
    private float[] buffDuration;

    private Stats stats;
    private float[] nextTime;

    private GameObject visualEffect;
    private bool effectActive = false;

    private void Start()
    {
        stats = GetComponent<Stats>();

        buff = new Buff[10];
        buffDuration = new float[10];
        nextTime = new float[10];
        for (int i = 0; i < nextTime.Length; i++)
        {
            nextTime[i] = 0f;
        }
    }

    public void AddBuff(Buff buffToAdd)
    {
        if (CheckIfBuffExists(buffToAdd) == false)
            AddNewBuff(buffToAdd);
    }

    public bool CheckIfBuffExists(Buff buffToAdd)
    {
        for (int i = 0; i < buff.Length; i++)
        {
            if (buff[i] != null)
            {
                if (buff[i].title == buffToAdd.title)
                {
                    buffDuration[i] = buffToAdd.duration;
                    return true;
                }
            }
        }
        return false;
    }

    public void AddNewBuff(Buff buffToAdd)
    {
        for (int i = 0; i < buff.Length; i++)
        {
            if (buff[i] == null)
            {
                //Debug.Log("apply buff");
                buffDuration[i] = buffToAdd.duration;
                buff[i] = buffToAdd;
                return;
            }
        }
    }

    public void Update()
    {
        for (int i = 0; i < buff.Length; i++)
        {
            if (buff[i] != null)
            {
                buffDuration[i] -= Time.deltaTime;
                if (buffDuration[i] > 0f)
                {
                    if (Time.time >= nextTime[i])
                    {
                        Execute(buff[i], i);
                        nextTime[i] = Time.time + buff[i].interval;
                    }

                    if (effectActive == false && buff[i].visualEffect != null)
                    {
                        visualEffect = Instantiate(buff[i].visualEffect, this.transform.position, this.transform.rotation);
                        visualEffect.transform.SetParent(this.transform);
                        effectActive = true;
                    }
                }
                else if (buffDuration[i] <= 0f)
                {
                    if (buff[i].movementImpairing)
                        stats.ResetMovespeed(buff[i].buffValue);

                    buff[i] = null;
                    nextTime[i] = 0f;
                    Destroy(visualEffect);
                    effectActive = false;
                }
            }
        }
    }

    public void Execute(Buff buffToExecute, float nextTimeArrayPosition)
    {
        if (buffToExecute.harmfull)
        {
            if (buffToExecute.damage)
                stats.TakeDamage(buffToExecute.buffValue);
            if (buffToExecute.movementImpairing)
                stats.SetMovespeed(buffToExecute.buffValue);
        }
        else if (buffToExecute.harmfull == false)
        {

        }
    }
}