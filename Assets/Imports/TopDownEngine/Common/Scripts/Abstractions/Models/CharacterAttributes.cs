using MoreMountains.TopDownEngine;
using System;

[Serializable]
public class CharacterAttributes
{ 
    public CharacterBaseClass CharacterBaseType { get; set; }
    public CharacterClass CharacterClass { get; set; }
    public CharacterRarity CharacterRarity { get; set; }
    public float Life { get; set; }
    public float Armor { get; set; }
    public float BlkChance { get; set; }
    public float BlkEfficiency { get; set; }
    public float MovementSpeed { get; set; }
    public float CDReduction { get; set; }
    public float Damage { get; set; }
    public float AttackSpeed { get; set; }
    public float CritChance { get; set; }
}
