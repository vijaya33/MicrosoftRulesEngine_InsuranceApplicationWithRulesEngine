namespace InsuranceApplicationWithRulesEngine.Models
{
    public class Customer
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string PlanType { get; set; } // e.g., "Basic", "Premium"
        public bool IsFamilyPlan { get; set; }
        public int FamilyMembers { get; set; } // If family plan
     
    }
}
