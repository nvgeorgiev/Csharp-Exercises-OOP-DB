namespace MilitaryElite.Models.Privates
{
    using MilitaryElite.Contracts.Privates;
    using System.Collections.Generic;
    using System.Text;

    public class LieutenantGeneral : Private, ILieutenantGeneral
    {
        public LieutenantGeneral(string id, string firstName, string lastName, decimal salary, HashSet<Private> privates) 
            : base(id, firstName, lastName, salary)
        {
            this.Privates = privates;
        }

        public IReadOnlyCollection<Private> Privates { get; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(base.ToString());
            stringBuilder.AppendLine("Privates:");

            foreach (var @private in this.Privates)
            {
                stringBuilder.AppendLine("  " + @private.ToString());
            }

            return stringBuilder.ToString().TrimEnd();
        }
    }
}
