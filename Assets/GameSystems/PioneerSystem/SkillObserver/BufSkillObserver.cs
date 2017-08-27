using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Skill;
using MasterData;

public class BufSkillObserver : SkillObserver {

    private BufSkill observeSkill;

    private readonly int MAG = 30;
    protected override int Mag => MAG;

    private int effect;
    protected override int Effect => effect;

    protected override bool IsAttackSkill => false;

    private int cost;
    protected override int Cost => cost;

    private float delay;
    protected override float Delay => delay;

    protected override string FileAddress => "BufProgress";

    protected override ActiveSkillProgress progress => BufSkillMasterManager.getInstance().getProgressFromId(OBSERVE_SKILL_ID);

	public BufSkillObserver(int id) : base(id) {}

	protected override void setParameter() {
        observeSkill = BufSkillMasterManager.getInstance().getBufSkillFromId(OBSERVE_SKILL_ID);
		this.effect = observeSkill.getRawBonus();
		this.cost = observeSkill.getRawCost();
		this.delay = observeSkill.getRawDelay();
    }
}
