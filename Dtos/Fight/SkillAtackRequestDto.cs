namespace dotnet_rpg.Dtos.Fight
{
    public class SkillAtackRequestDto
    {
        public int AttackerId { get; set; }
        public int OpponentId { get; set; }
        public int SkillId { get; set; }
    }
}