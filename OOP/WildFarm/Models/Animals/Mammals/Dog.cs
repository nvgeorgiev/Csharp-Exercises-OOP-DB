namespace WildFarm.Models.Animals.Mammals
{
    using System.Collections.Generic;
    using WildFarm.Models.Foods;

    public class Dog : Mammal
    {
        private const double GainValue = 0.4;

        public Dog(string name, double weight, string livingRegion)
            : base(name, weight, livingRegion)
        {
        }

        public override void Eat(Food food)
        {
            this.BaseEat(food, new List<string>() { nameof(Meat) }, GainValue);
        }

        public override string ProduceSound()
        {
            return "Woof!";
        }
    }
}
