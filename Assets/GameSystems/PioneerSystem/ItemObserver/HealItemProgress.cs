public class HealItemProgress{
    /// <summary> 
    /// アイテムのレベルを表します 
    /// </summary>
    private int level;

    public int Level{
        set{
            if (value < 0)
                throw new System.ArgumentException("level won't be negative");
            this.level = value;
        }

        get { return level; }
    }

    /// <summary>
    /// アイテムの回復値の成長度を示します
    /// </summary>
    private int heal;

    public int Heal{
        get { return heal; }

		set {
            UnityEngine.Debug.Log("progress " + value);
			if (value < 0)
				throw new System.ArgumentException("heal won't be negative");
            this.heal = value;
		}
    }

    /// <summary>
    /// アイテムの価格の成長度を示します
    /// </summary>
    private int itemValue;

    public int ItemValue{
        get { return itemValue; }

		set {
			if (value < 0)
				throw new System.ArgumentException("itemvalue won't be negative");
            this.itemValue = value;
		}
	}
}
