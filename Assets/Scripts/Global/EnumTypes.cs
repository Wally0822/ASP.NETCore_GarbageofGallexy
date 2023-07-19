using System;

public static partial class EnumTypes
{
    public enum LanguageType // 테이블
    {
        Kor = 0,
        Eng = 1
    }
    public enum LayoutType
    {
        First,
        Middle,
        Global,  // popup
    }

    public enum InGameParamType
    {
        Player,
        MAX
    }

    public enum PlayerStateType
    {
        Death,
        LevelUp,
        MAX //마지막 Enum => Const느낌
    }
    public enum PlayerSkiilsType
    {
        DoubleShot,
        TripleShot,
        MultiShot,
        MAX
    }

    public enum EffectSoundType
    {
        None,
        Attack,
        AttackSkill,
        Defence,
        DefenceSkill,
        Hit,
        Die,
        Button,
    }

    public enum StageBGMType
    {
        Title,
        Lobby,
        Stage1,
        Stage2,
        Stage3,
        Stage4,
        Stage5,
        Stage6,
    }

    public enum ScenesType
    { 
        SceneTitle,
        SceneLobby,
        SceneInGame
    }

}
public static partial class EnumTypes
{
    public static int GetEnumNumber<TEnum>(TEnum tenum)
    {
        Type enumType = typeof(TEnum);
        int idx = -1;
        foreach (var type in Enum.GetValues(enumType))
        {
            idx++;
            if (type.ToString() == tenum.ToString()) break;
        }
        return idx;
    }
}