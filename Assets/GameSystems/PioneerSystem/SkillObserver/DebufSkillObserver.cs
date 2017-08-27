using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Skill;
using MasterData;
using System;

public class DebufSkillObserver : SkillObserver {
    private DebufSkill skill;

    readonly int MAG = 30;
    protected override int Mag => MAG;

    int effect;
    protected override int Effect => effect;

    protected override bool IsAttackSkill => false;

    int cost;
    protected override int Cost => cost;

    float delay;
    protected override float Delay => delay;

    protected override string FileAddress => "DebufSkillProgress";

    protected override ActiveSkillProgress progress => DebufSkillMasterManager.getInstance().getProgressFromId(OBSERVE_SKILL_ID);

	public DebufSkillObserver(int id) : base(id) {}

	protected override void setParameter() {
        skill = DebufSkillMasterManager.getInstance().getDebufSkillFromId(OBSERVE_SKILL_ID);
		this.effect = skill.getRawBonus();
		this.cost = skill.getRawCost();
		this.delay = skill.getRawDelay();
    }
}
