using System;

namespace Marreco.Expenses.DomainModel
{
    public sealed class Category : ValueObject<Category>
    {
        public readonly string Name;
        public readonly string MasterCategory; 
        public Category(string name, string masterCategory)
        {
            Name = name; masterCategory = MasterCategory;
        }

        public Category WithName(string name) => new Category(name, this.MasterCategory);
    }

}

