using Insurance_Repository;

public class ProgramUI {

    RepoConsoleApp _insurance = new RepoConsoleApp();

    public void Run() {
        Seed();
        Claims();
    }

    private void Claims() {
        bool keepRunning = true;

        while (keepRunning) {
            Console.Clear();
            
            System.Console.WriteLine("Please select from the following options:\n"
            + "1. Add a new claim to queue.\n"
            + "2. View next claim in queue.\n"
            + "3. View ALL claims.\n"
            + "4. Process and remove the next claim.\n"
            + "5. Exit.");

            string? input = Console.ReadLine();
            
            switch (input)
            {
                case "1":
                    CreateNewClaim();
                    break;
                case "2":
                    ViewNextClaim();
                    break;
                case "3":
                    ViewAllClaims();
                    break;
                case "4":
                    ProcessNextClaim();
                    break;
                case "5":
                System.Console.WriteLine("Thank you for using Komodo Insurance Services. Have a good day!");
                    keepRunning = false;
                    break;
                default:
                System.Console.WriteLine("I'm sorry, we could not recognize your response. Please try again.");
                    break;
            }

            System.Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

    }

    private void CreateNewClaim()
    {
        Console.Clear();

        ClaimQueue newClaim = new ClaimQueue();

        newClaim.ClaimID = _insurance.GetAllClaims().Count + 1;

        System.Console.WriteLine("Please enter a claim type for your new claim. Choose between Car, Home, or Theft:\n"
        + "1. Car\n"
        + "2. Home\n"
        + "3. Theft\n");
        string? typeString = Console.ReadLine();
        int typeInt = int.Parse(typeString);
        newClaim.ClaimType = (ClaimType)typeInt;
        
        System.Console.WriteLine("Please enter a description for your new claim:");
        newClaim.Description = Console.ReadLine();
        
        System.Console.WriteLine("Please enter the claim amount for your new claim:");
        string? claimString = Console.ReadLine();
        newClaim.ClaimAmount = double.Parse(claimString);

        System.Console.WriteLine("Please enter the date of incident for your new claim:");
        string? incidentString = Console.ReadLine();
        newClaim.DateOfIncident = DateTime.Parse(incidentString);

        System.Console.WriteLine("Please enter the date of claim for your new claim:");
        string? claimDate = Console.ReadLine();
        newClaim.DateOfClaim = DateTime.Parse(claimDate);

        System.Console.WriteLine("Please enter yes or no if the incident date is within 30 days of the claim date:\n"
        + "1. Yes\n"
        + "2. No\n");
        string? validString = Console.ReadLine();
        int validInt = int.Parse(validString);
        newClaim.ValidStatus = (ValidStatus)validInt;

        bool claimAdded = _insurance.CreateNewClaim(newClaim);

        if (claimAdded) {
            Console.Clear();
            System.Console.WriteLine("Claim added successfully!");
        } else {
            Console.Clear();
            System.Console.WriteLine("Claim was NOT added. Please try again!");
        }
    }

    private void ViewNextClaim() {
        Console.Clear();

        Queue<ClaimQueue> claimsQueue = _insurance.GetAllClaims();

        if (claimsQueue.Count > 0)
        {
            ClaimQueue viewNext = _insurance.NextClaim();
            DisplayClaim(viewNext);
        } else {
            System.Console.WriteLine("There are no claims available.");
        }
    }

    private void ViewAllClaims() {
        Console.Clear();

        foreach (ClaimQueue claim in _insurance.GetAllClaims())
        {   
            DisplayClaim(claim);
        }
    }

    private void ProcessNextClaim() {
        Console.Clear();

        if (_insurance.GetAllClaims().Count > 0)
            {
                Console.Clear();
                
                ClaimQueue claim = _insurance.NextClaim();

                System.Console.WriteLine($"Claim ID: {claim.ClaimID} | {claim.ClaimType}\n"
                + "-----------------\n"
                + $"Claim Amount: {claim.ClaimAmount}\n"
                + $"Date Of Incident: {claim.DateOfIncident}\n"
                + $"Date Of Claim: {claim.DateOfClaim}\n"
                + $"Is claim within 30 days of incident? {claim.ValidStatus}\n"
                + $"Description of incident: {claim.Description}\n");
                System.Console.WriteLine();
                
                System.Console.WriteLine("Would you like to process this claim?\n"
                + "1. Yes\n"
                + "2. No\n");
                string? processAnswer = Console.ReadLine();

                switch (processAnswer)
                {
                    case "1":
                        Console.Clear();

                        ClaimQueue processClaim = _insurance.ProcessNext();

                    System.Console.WriteLine("Claim successfully processed!");
                            break;
                        case "2":
                        System.Console.WriteLine("Back to Main Menu");
                            break;
                        default:
                        Console.Clear();
                        System.Console.WriteLine("Could not process claim. Please try again!");
                            break;
                        }
            } else {
                System.Console.WriteLine("There are no claims to process.");
            }
    }

    private void DisplayClaim(ClaimQueue claim) {
        
        System.Console.WriteLine();
        System.Console.WriteLine($"Claim ID: {claim.ClaimID} | {claim.ClaimType}\n"
        + "-----------------\n"
        + $"Claim Amount: {claim.ClaimAmount}\n"
        + $"Date Of Incident: {claim.DateOfIncident}\n"
        + $"Date Of Claim: {claim.DateOfClaim}\n"
        + $"Is claim within 30 days of incident? {claim.ValidStatus}\n"
        + $"Description of incident: {claim.Description}\n");
        System.Console.WriteLine();
    }

    private void Seed() {
        ClaimQueue carClaim = new ClaimQueue(_insurance.GetAllClaims().Count + 1, ClaimType.Car, "Client's car was rear-ended on McDonald Street while stopped at a red light.", new DateTime(2022, 07, 15), new DateTime(2022, 07, 20), 300.76, ValidStatus.Yes);
        _insurance.CreateNewClaim(carClaim);

        ClaimQueue homeClaim = new ClaimQueue(_insurance.GetAllClaims().Count + 1, ClaimType.Home, "Fire burned the client's house down while homeowner was cooking dinner. Owner was in the hospital for 3 months. Home is completely destroyed.", new DateTime(2022, 04, 28), new DateTime(2022, 06, 30), 500000.00, ValidStatus.No);
        _insurance.CreateNewClaim(homeClaim);

        ClaimQueue theftClaim = new ClaimQueue(_insurance.GetAllClaims().Count + 1, ClaimType.Theft, "Clients house was broken into while family was on vacation. Incident was recorded on security cameras", new DateTime(2022, 03, 20), new DateTime(2022, 04, 19), 3000.53, ValidStatus.Yes);
        _insurance.CreateNewClaim(theftClaim);
    }
}