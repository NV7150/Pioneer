using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AI;
using Character;

namespace AI{
	public interface IEnemyAIBuilder {
		IEnemyAI build(IBattleable bal,ActiveSkillSet activeSkills,PassiveSkillSet passiveSkills);
		int getId();
	}
}
