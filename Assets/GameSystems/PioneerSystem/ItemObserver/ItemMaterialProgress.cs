public class ItemMaterialProgress{

    /// <summary>
    /// アイテムのレベルを示します
    /// </summary>
    private int level;
    public int Level{
        get{ return level; }

        set{
            if (value < 0)
                throw new System.ArgumentException("level won't be negative");
            this.level = value;
        }
    }

    /// <summary>
    /// アイテムの品質の成長値を示します
    /// </summary>
    private float quality;
    public float Quality{
        get { return quality; }

        set{
			if (value < 0)
				throw new System.ArgumentException("quality won't be negative");
            this.quality = value;
        }
    }

    /// <summary>
    /// アイテムの価格の成長値を示します
    /// </summary>
    private int itemValue;
    public int ItemValue{
        get { return itemValue; }

        set{
			if (value < 0)
				throw new System.ArgumentException("itemvalue won't be negative");
            this.itemValue = value;
        }
    }
}
