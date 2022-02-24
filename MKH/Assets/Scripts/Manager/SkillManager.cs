using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public enum Skill
    {
        DoubleJump, TimeSlow, FaceBreak, Count
    }

    [System.Serializable]
    public struct SkillInfo
    {
        [SerializeField]
        public Skill skill;
        [SerializeField]
        public bool unlock;
        [SerializeField]
        public bool active;
    }

    [NamedArrayElement("skill")]
    public SkillInfo[] skillInfo = new SkillInfo[(int)Skill.Count];

    public SkillManager()
    {
        for(int i=0; i<(int)Skill.Count; ++i)
        {
            skillInfo[i].skill = (Skill)i;
            skillInfo[i].active = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isSkillUnLocked(in Skill skill)
    {
        return skillInfo[(int)skill].unlock;
    }

    public bool IsActived(in Skill skill)
    {
        return skillInfo[(int)skill].active;
    }
    public void Activate(in Skill skill)
    {
        skillInfo[(int)skill].active = true;
    }
    public void DeActivate(in Skill skill)
    {
        skillInfo[(int)skill].active = false;
    }

}
