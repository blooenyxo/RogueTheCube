using System.Collections.Generic;
using UnityEngine;

public enum TypeOfNPC { QuestGiver, Shop }

public class NPC_Controller : MonoBehaviour
{
    public TypeOfNPC typeOfNPC;
    public List<Item> items = new List<Item>();
    public int[] stacks;

}
