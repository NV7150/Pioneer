using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BattleAbility = Parameter.CharacterParameters.BattleAbility;
using AttackSkillAttribute = Skill.ActiveSkillParameters.AttackSkillAttribute;

public class EnemyProgress{
    public EnemyProgress(){
        var abilityKeys = System.Enum.GetValues(typeof(AttackSkillAttribute));
        foreach(BattleAbility key in abilityKeys){
            abilities.Add(key,0);
        }

        var attributeKeys = System.Enum.GetValues(typeof(AttackSkillAttribute));
        foreach(AttackSkillAttribute key in attributeKeys){
            attributeResistances.Add(key, 1.0f);
        }

    }


    /// <summary>
    /// 能力値の成長値
    /// </summary>
    private Dictionary<BattleAbility, int> abilities = new Dictionary<BattleAbility, int>();
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
    private Dictionary<AttackSkillAttribute, float> attributeResistances = new Dictionary<AttackSkillAttribute, float>();
    public Dictionary<AttackSkillAttribute, float> AttributeResistances{
        get { return new Dictionary<AttackSkillAttribute, float>(attributeResistances); }
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
