﻿namespace WildFarm.Models.Animals.Birds
{
    using System.Collections.Generic;
    using WildFarm.Models.Foods;

    public class Hen : Bird
    {
        private const double GainValue = 0.35;

        public Hen(string name, double weight, double wingSize)
            : base(name, weight, wingSize)
        {
        }

        public override void Eat(Food food)
        {
            this.BaseEat(food, new List<string>() { nameof(Vegetable), nameof(Meat), nameof(Seeds),
                nameof(Fruit) }, GainValue);
        }

        public override string ProduceSound()
        {
            return "Cluck";
        }
    }
}
