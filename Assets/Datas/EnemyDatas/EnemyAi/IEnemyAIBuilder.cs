using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AI;
using Character;

namespace AI{
	public interface IEnemyAIBuilder {
		//AIを取得します
		IEnemyAI build(IBattleable bal,ActiveSkillSet activeSkills,ReactionSkillSet passiveSkills);
		//AIのIDを取得します
		int getId();
	}
}
