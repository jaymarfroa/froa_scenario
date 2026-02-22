using System;

namespace Froa_ScenarioD
{
    // --- BASE CLASS ---
    public abstract class WaterFilter
    {
        private string _filterId;
        private int _usageCount;

        // Encapsulation: Public properties with logic in accessors
        public string FilterID
        {
            get => _filterId;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Filter ID cannot be empty.");
                _filterId = value;
            }
        }

        public int UsageCount
        {
            get => _usageCount;
            set
            {
                if (value < 0) throw new ArgumentException("Usage count cannot be negative.");
                _usageCount = value;
            }
        }

        // Inheritance: Base constructor
        public WaterFilter(string id, int initialUsage)
        {
            FilterID = id;
            UsageCount = initialUsage;
        }

        // Polymorphism: Abstract method to be overridden
        public abstract void ProcessWater();

        // Polymorphism: Virtual method for efficiency calculation
        public virtual double CalculateEfficiency()
        {
            // Logic: Handle DivideByZero manually or let the Exception be caught in Main
            if (UsageCount == 0)
            {
                throw new DivideByZeroException("Cannot calculate efficiency: No water has passed through this filter yet.");
            }
            return 95.5; // Example efficiency constant
        }
    }

    // --- SUB-CLASS: CARBON ---
    public class CarbonFilter : WaterFilter
    {
        private int _physicalDebrisLevel;

        public int PhysicalDebrisLevel
        {
            get => _physicalDebrisLevel;
            set => _physicalDebrisLevel = value;
        }

        // Inheritance: Using : base() syntax
        public CarbonFilter(string id, int usage) : base(id, usage) { }

        public override void ProcessWater()
        {
            Console.WriteLine($"[Carbon Filter {FilterID}] is trapping physical debris. Usage increased.");
            UsageCount++;
        }
    }

    // --- SUB-CLASS: CHEMICAL ---
    public class ChemicalFilter : WaterFilter
    {
        private int _chemicalLevel;

        public int ChemicalLevel
        {
            get => _chemicalLevel;
            set => _chemicalLevel = value;
        }

        public ChemicalFilter(string id, int usage) : base(id, usage) { }

        public override void ProcessWater()
        {
            Console.WriteLine($"[Chemical Filter {FilterID}] is neutralizing chlorine levels. Usage increased.");
            UsageCount++;
        }
    }

    // --- MAIN EXECUTION ---
    class Program
    {
        static void Main(string[] args)
        {
            // Robustness: Try-Catch-Finally block
            try
            {
                Console.WriteLine("--- Clean-Water Plant Operations Starting ---\n");

                // Scenario: A new chemical filter with 0 usage
                ChemicalFilter chemFilter = new ChemicalFilter("CHEM-101", 0);

                // This call will trigger the ProcessWater logic
                chemFilter.ProcessWater();

                // Intentional reset to 0 to demonstrate the DivideByZeroException requirement
                chemFilter.UsageCount = 0;
                Console.WriteLine($"Calculating efficiency for {chemFilter.FilterID}...");

                double efficiency = chemFilter.CalculateEfficiency();
                Console.WriteLine($"Efficiency: {efficiency}%");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GENERAL ERROR: {ex.Message}");
            }
            finally
            {
                // Robustness: Final block execution
                Console.WriteLine("\nSession Ended. System Shutdown.");
            }
        }
    }
}
