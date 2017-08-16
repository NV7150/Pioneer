using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Skill;

using Random = UnityEngine.Random;

public abstract class SkillObserver : IObserver {
    protected readonly int OBSERVE_SKILL_ID;

	protected int useFrequency = 0;

    protected abstract int Mag{
        get;
    }

    protected abstract int Effect{
        get;
    }

    protected virtual int Hit{
        get { return 0; }
    }

    protected abstract bool IsAttackSkill{
        get;
    }

    protected abstract int Cost{
        get;
    }

    protected abstract float Delay{
        get;
    }

    protected abstract string FileAddress{
        get;
    }

    protected abstract ActiveSkillProgress progress{
        get;
    }

	public SkillObserver(int id) {
		this.OBSERVE_SKILL_ID = id;
		PioneerManager.getInstance().setObserver(this);
	}

    public void report(){
        progress.Effect += getValueProgress(Effect);
        progress.Cost -= getValueProgress(Cost);
        progress.Delay -= getSpeedProgress(Delay);

        if (IsAttackSkill) {
            var attackProgress = (ActiveAttackSkillProgress)progress;
			attackProgress.Hit += Hit;
            ObserverHelper.saveToFile<ActiveAttackSkillProgress>(attackProgress, FileAddress, OBSERVE_SKILL_ID);
        } else{
            ObserverHelper.saveToFile<ActiveSkillProgress>(progress, FileAddress,OBSERVE_SKILL_ID);
        }

    }

    public void reset(){
        useFrequency = 0;
    }

    protected int getValueProgress(int baseValue){
        baseValue = (baseValue > 0) ? baseValue : 1;
        float probality = (useFrequency + 1) / (baseValue / 15 + 1) * Mag;
        float probalityRand = Random.Range(0, 100);
        if(probality >= probalityRand){
			float randAbs = useFrequency / 3;
            float valueRand = Random.Range(-randAbs, randAbs);
			int growthPercentage = (valueRand + useFrequency >= 0) ? (int)valueRand + useFrequency : 0;
            int valueProgress = (int)((float)baseValue * (float)growthPercentage / 100);
			return valueProgress;
        }
        return 0;
    }

    protected float getSpeedProgress(float baseSpeed) {
        float probality = (useFrequency + 1) / (50 / baseSpeed) * Mag / 2;
		float probalityRand = Random.Range(0, 100);
		if (probality >= probalityRand) {
			float randAbs = useFrequency / 3;
			float valueRand = Random.Range(-randAbs, randAbs);
			int growthPercentage = (valueRand + useFrequency >= 0) ? (int)valueRand + useFrequency : 0;
            float valueProgress = (baseSpeed * (float)growthPercentage / 1000);
            valueProgress = (valueProgress <= 0.2f) ? valueProgress : 0.2f;
                
			return -valueProgress;
		}
		return 0;
    }

    public void used(){
        useFrequency++;
    }

    protected abstract void setParameter();
}
