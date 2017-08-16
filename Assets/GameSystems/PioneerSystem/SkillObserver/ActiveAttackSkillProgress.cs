using System;

public class ActiveAttackSkillProgress : ActiveSkillProgress{
    int hit;
    public int Hit{
        get { return this.hit; }
        set {
            if (value < 0)
                throw new ArgumentException("hit won't be negative");
            this.hit = value;
        }
    }
}
