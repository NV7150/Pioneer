using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

using MasterData;
using Skill;

using ItemAttribute = Item.ItemParameters.ItemAttribute;
using ItemType = Item.ItemParameters.ItemType;
using SkillType = Skill.ActiveSkillParameters.ActiveSkillType;

namespace Item {
    public class SkillBook : IItem {
        private readonly int SKILL_ID;
        private readonly SkillType SKILL_TYPE;
        private readonly string NAME;
        private readonly string DESCRIPTION;
        private readonly string FLAVOR_TEXT;
        private readonly int ITEM_VALUE;
        private readonly int MASS;
        private readonly bool IS_REACTIONSKILL;
        private readonly int LEVEL;


        public SkillBook(SkillBookBuilder builder){
            SKILL_ID = builder.SkillId;
            SKILL_TYPE = builder.SkillType;
            NAME = builder.Name;
            DESCRIPTION = builder.Description;
            FLAVOR_TEXT = builder.FlavorText;
            ITEM_VALUE = builder.ItemValue;
            MASS = builder.Mass;
            IS_REACTIONSKILL = builder.IsReactionSkill;
            LEVEL = builder.Level;
        }

        public bool getCanStack() {
            return false;
        }

        public bool getCanStore() {
            return true;
        }

        public string getDescription() {
            return DESCRIPTION;
        }

        public string getFlavorText() {
            return FLAVOR_TEXT;
        }

        public int getId() {
            return SKILL_ID;
        }

        public ItemAttribute getItemAttribute() {
            return ItemAttribute.MAGIC;
        }

        public ItemType getItemType() {
            return ItemType.SKILL_BOOK;
        }

        public int getItemValue() {
            return ITEM_VALUE;
        }

        public int getMass() {
            return MASS;
        }

        public string getName() {
            return NAME;
        }

        public void use(IPlayable user) {
            if(IS_REACTIONSKILL){
                var reactionSkill = ReactionSkillMasterManager.getInstance().getReactionSkillFromId(SKILL_ID);
                user.addSkill(reactionSkill);
            }else{
                var activeSkill = ActiveSkillSupporter.getActiveSkill(SKILL_TYPE, SKILL_ID);
                user.addSkill(activeSkill);
            }
        }
    }
}
