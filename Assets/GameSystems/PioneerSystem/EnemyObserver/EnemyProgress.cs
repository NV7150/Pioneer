using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BattleAbility = Parameter.CharacterParameters.BattleAbility;
using AttackAttribute = Skill.ActiveSkillParameters.AttackSkillAttribute;

public class EnemyProgress{
    /// <summary>
    /// 能力値の成長値
    /// </summary>
    private Dictionary<BattleAbility, int> abilities;
    public Dictionary<BattleAbility, int> Abilities{
        get{ return new Dictionary<BattleAbility, int>(abilities); }
        set{ 
            if (abilities == null) 
                abilities = value; 
        }
    }

    /// <summary>
    /// 各属性への耐性(%形式)
    /// </summary>
    private Dictionary<AttackAttribute, float> attributeResistances;
    public Dictionary<AttackAttribute, float> AttributeResistances{
        get { return new Dictionary<AttackAttribute, float>(attributeResistances); }
        set { 
            if (attributeResistances == null) 
                attributeResistances = value; 
        }
    }

    /// <summary>
    /// レベル
    /// </summary>
    private int level;
    public int Level{
        get { return level; }
        set{
            if (value < 0)
                throw new System.ArgumentException("level won't be negative");
            this.level = value;
        }
    }

    /// <summary>
    /// 武器レベルの成長値
    /// </summary>
    private int weponLevel;
    public int WeponLevel{
        get { return weponLevel; }
        set{
            if (value < 0)
                throw new System.ArgumentException("weponLevel won't be negative");
            this.weponLevel = value;
        }
    }
}
