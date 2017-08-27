using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Skill;
using MasterData;

public class AttackSkillObserver : SkillObserver {

    private AttackSkill observeSkill;

	private readonly int MAG = 50;
    protected override int Mag => MAG;

    private int attack;
    protected override int Effect => attack;

    private int cost;
    protected override int Cost => cost;

    private float delay;
    protected override float Delay => delay;

    protected override string FileAddress => "AttackSkillObserver";

    protected override ActiveSkillProgress progress => AttackSkillMasterManager.getInstance().getAttackSkillProgressFromId(OBSERVE_SKILL_ID);

	protected override bool IsAttackSkill => true;

    private int hit;
	protected override int Hit => hit;

    public AttackSkillObserver(int id) : base(id) {}

	protected override void setParameter() {
        observeSkill = AttackSkillMasterManager.getInstance().getAttackSkillFromId(OBSERVE_SKILL_ID);
        this.observeSkill = AttackSkillMasterManager.getInstance().getAttackSkillFromId(OBSERVE_SKILL_ID);
		this.attack = observeSkill.getRawAttack();
		this.hit = observeSkill.getRawHit();
		this.cost = observeSkill.getRawCost();
		this.delay = observeSkill.getRawDelay();
    }
}
