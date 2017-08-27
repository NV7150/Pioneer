using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Skill;
using Item;

using ActiveSkillType = Skill.ActiveSkillParameters.ActiveSkillType;

public class SkillBookDataManager {
    private static readonly SkillBookDataManager INSTANCE = new SkillBookDataManager();
    private SkillBookDataManager(){}

    public static SkillBookDataManager getInstance(){
        return INSTANCE;
    }

    List<SkillBookBuilder> dataTable = new List<SkillBookBuilder>();

    public void setData(ISkill skill){
        dataTable.Add(new SkillBookBuilder(skill));
    }

    public SkillBook getActiveSkillBookFromTypeAndId(int id,ActiveSkillType skillType){
        foreach(var builder in dataTable){
            if (!builder.IsReactionSkill) {
                if (builder.SkillType == skillType && builder.SkillId == id){
                    return builder.build();
                }
           }    
        }
        throw new System.ArgumentException("actionSkill you want couldn't be found");
    }

    public SkillBook getReactionSkillBookFromId(int id){
        foreach(var builder in dataTable){
            if (builder.IsReactionSkill && builder.SkillId == id)
                return builder.build();
        }

        throw new System.ArgumentException("reactionSKill you want couldn't be found");
    }

    public List<SkillBook> getSkillBooksFromLevel(int level){
        List<SkillBook> skillBooks = new List<SkillBook>();
        foreach(var builder in dataTable){
            if(builder.Level <= level && (builder.Level - level) <= 3)
                skillBooks.Add(builder.build());
        }
        return skillBooks;
    } 
}
