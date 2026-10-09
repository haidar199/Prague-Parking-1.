//1. We need to create the parking.
//1.a We need 100 parking spaces.

using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.Marshalling;

string[] parking = new string[100];
Array.Fill(parking, ""); //sets every space to empty string


//1.b Create menu

while (true)
{
    ShowMenu();
    string choice= Console.ReadLine() ?? "";

    switch(choice)
    {
        case "1":
            ParkVehicle(parking);
            break;

        case "2":
            Console.WriteLine("Search: coming next.");
            break;

        case"3":
            Console.WriteLine("Remove: coming next.");
            break;
        
        case "4":
            Console.WriteLine("Move: coming next.");
            break;

        case "0":
            return;

        default:
            Console.WriteLine("Invalid choice.");
            break;
    }
}

static void ShowMenu()
{
        Console.WriteLine("\n--- Prague Parking ---");
        Console.WriteLine("1. Park a vehicle");
        Console.WriteLine("2. Search for a vehicle");
        Console.WriteLine("3. Remove a vehicle");
        Console.WriteLine("4. Move a vehicl");
        Console.WriteLine("0. Exit");
        Console.WriteLine("Choose an option: ");

}

static void ParkVehicle(string[] parking)
{
    string registration = ReadRegistration();
    string vehicleType = ReadVehicleType();

    int index = FindEmptySpace(parking);

    if (index != -1)
    {
        parking[index] = string.Join("#", vehicleType, registration);
        Console.WriteLine($"Vehicle parked in space {index + 1}.");
    }
    else
    {
        Console.WriteLine("No empty spaces avialable.");
    }

    //2. Take input data and check if it is valid.
}
static string ReadRegistration()
{
    while (true)
    {
        Console.WriteLine("Enter plate number: ");
        string registration = (Console.ReadLine() ?? "").Trim().ToUpper();

        if (registration.Length > 0 && registration.Length <= 10)
        {
            return registration;
        }

        Console.WriteLine("Plate number must contain 1-10 charachters.");
    }
}

// Check vehicle type and find a parking space


static string ReadVehicleType()
{
    while (true)
    {
        Console.WriteLine("Enter vehicle type car or mc: ");
        string vehicleType = (Console.ReadLine() ?? "").Trim().ToUpper();

        if (vehicleType == "CAR" || vehicleType == "MC")
        {
            return vehicleType;
        }
    }
}

static int FindEmptySpace(string[] parking)
{
        for (int i = 0; i < parking.Length; i++)
        {
            if (parking[i] == "") 
            {
                return i;
            }
        }
        return -1;
}
