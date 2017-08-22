using System;

namespace BattleSystem {
    /// <summary>
    /// BattleTaskManager上のステート
    /// </summary>
	public enum BattleState {
		//コマンド待ち
		IDLE,
        //ディレイ中
        DELAY,
		//アクション
		ACTION
	}
}

