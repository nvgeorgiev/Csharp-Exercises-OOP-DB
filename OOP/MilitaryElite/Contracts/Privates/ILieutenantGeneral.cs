namespace MilitaryElite.Contracts.Privates
{
    using MilitaryElite.Models;
    using System.Collections.Generic;

    public interface ILieutenantGeneral
    {
        IReadOnlyCollection<Private> Privates { get; }
    }
}
