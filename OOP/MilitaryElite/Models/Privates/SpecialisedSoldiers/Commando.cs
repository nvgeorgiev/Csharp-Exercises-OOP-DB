namespace MilitaryElite.Models.Privates.SpecialisedSoldiers
{
    using MilitaryElite.Contracts.Privates.SpecialisedSoldiers;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Commando : SpecialisedSoldier, ICommando
    {
        public Commando(string id, string firstName, string lastName, decimal salary, string corps, List<Mission> missions)
            : base(id, firstName, lastName, salary, corps)
        {
            this.Missions = missions;
        }

        public IReadOnlyCollection<Mission> Missions { get; }

        public void CompleteMission(string codeName)
        {
            Mission mission = Missions.FirstOrDefault(x => x.CodeName == codeName);

            if (mission != null)
            {
                mission.State = "Finished";
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(base.ToString());
            stringBuilder.AppendLine($"Missions:");

            foreach (var mission in this.Missions)
            {
                stringBuilder.AppendLine("  " + mission.ToString());
            }

            return stringBuilder.ToString().TrimEnd();
        }
    }
}
