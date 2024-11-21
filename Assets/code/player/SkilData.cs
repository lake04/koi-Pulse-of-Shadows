using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Skil",menuName ="Scriprtble Object/SkilData")]
public class SkilData : ScriptableObject
{
    public enum SkilType {hp_up, damage_up, healing};

    [Header("# Main Info")]
    public SkilType skilType;
    public int skilId;
    public string skilName;
    public string skilDesc;
    public Sprite skilIcon;

    [Header("# Level Data")]
    public float baseDamge;
    public int   baseCount;
    public float[] damages;
    public int[]   counts;

}