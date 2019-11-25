namespace MilitaryElite.Contracts.Privates.SpecialisedSoldiers
{
    using MilitaryElite.Models.Privates.SpecialisedSoldiers;
    using System.Collections.Generic;

    public interface IEngineer
    {
        IReadOnlyCollection<Repair> Repairs { get; }
    }
}
