using System;

public class ActiveSkillProgress{
    int effect;
    public int Effect{
        get { return effect; }
        set{
            if (value < 0)
                throw new ArgumentException("effect won't be negative");
            this.effect = value;
        }
    }

    float delay;
    public float Delay{
        get { return this.delay; }
        set{
            if (value < 0.5f)
                value = 0.5f;
            this.delay = value;
        }
    }

    int cost;
    public int Cost{
        get { return this.cost; }
        set{
            if (value < 1)
                value = 1;
            this.cost = 1;
        }
    }
}
