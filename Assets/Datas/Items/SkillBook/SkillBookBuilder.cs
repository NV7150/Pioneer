using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Skill;
using Item;

using ActiveSKillType = Skill.ActiveSkillParameters.ActiveSkillType;

public class SkillBookBuilder{
    private int skillId;
    public int SkillId {
        get { return skillId; }
    }

    private ActiveSKillType skillType;
    public ActiveSKillType SkillType {
        get { return skillType; }
    }

    private string name;
    public string Name {
        get { return name; }
    }

    private string description;
    public string Description {
        get { return description; }
    }

    private string flavorText;
    public string FlavorText {
        get { return flavorText; }
    }

    private int itemValue;
    public int ItemValue {
        get { return itemValue; }
    }

    private int mass;
    public int Mass {
        get { return mass; }
    }

    private bool isReactionSkill;
    public bool IsReactionSkill {
        get { return isReactionSkill; }
    }

    private int level;

    public int Level {
        get {return level;}
    }

    public SkillBookBuilder(ISkill skill){
        skillId = skill.getId();
        name = skill.getName() + "のスキル書";
        description = name + "の知識が書かれた魔法の書";
        flavorText = skill.getFlavorText();

        itemValue = skill.getLevel() * 30;
        mass = skill.getLevel() / 5 + 1;
        level = skill.getLevel();

        if(skill is ReactionSkill){
            isReactionSkill = true;
        }else{
            var activeSkill = (IActiveSkill)skill;
            skillType = activeSkill.getActiveSkillType();
            isReactionSkill = false;
        }
    }

    public SkillBook build(){
        return new SkillBook(this);
    }
}
