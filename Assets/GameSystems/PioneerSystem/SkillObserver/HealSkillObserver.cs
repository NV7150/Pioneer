using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Skill;
using MasterData;

public class HealSkillObserver : SkillObserver {

    private HealSkill observeSkill;

    private int MAG = 50;
    protected override int Mag => MAG;

    private int heal;
    protected override int Effect => heal;

    private int cost;
    protected override int Cost => cost;

    private float delay;
    protected override float Delay => delay;

    protected override string FileAddress => "HealSkillProgress";

    protected override ActiveSkillProgress progress => HealSkillMasterManager.getInstance().getHealSkillProgressFromId(OBSERVE_SKILL_ID);

    protected override bool IsAttackSkill => false;

    public HealSkillObserver(int id) : base(id) {}

	protected override void setParameter() {
        observeSkill = HealSkillMasterManager.getInstance().getHealSkillFromId(OBSERVE_SKILL_ID);
		this.heal = observeSkill.getRawHeal();
		this.cost = observeSkill.getRawCost();
		this.delay = observeSkill.getRawDelay();
    }
}
