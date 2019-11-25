namespace MilitaryElite.Contracts.Privates.SpecialisedSoldiers
{
    using MilitaryElite.Models.Privates.SpecialisedSoldiers;
    using System.Collections.Generic;

    public interface ICommando
    {
        IReadOnlyCollection<Mission> Missions { get; }

        void CompleteMission(string codeName);
    }
}
