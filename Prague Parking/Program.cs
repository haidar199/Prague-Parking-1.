//1. We need to create the parking.
//1.a We need 100 parking spaces.

using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices.Marshalling;
using System.Threading.Channels;

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
            SearchVehicle(parking);
            break;

        case"3":
            RemoveVehicle(parking);
            break;
        
        case "4":
            MoveVehicle(parking);
            break;

        case "0":
            return;

        default:
            Console.WriteLine("Invalid choice.");
            break;
    }
}
// Display the menu.
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
// Park vehicle and two MC can be parked in one space.
static void ParkVehicle(string[] parking)
{
    string registration = ReadRegistration();
    string vehicleType = ReadVehicleType();

    int index = FindEmptySpace(parking, vehicleType);

    if (index != -1)
    {
        string vehicle = vehicleType + "#" + registration;

        if (parking[index]=="")
        {
            parking[index] = vehicle;
        }
        else
        {
            parking[index] += "|" + vehicle;
        }

        Console.WriteLine($"Vehicle parked in space {index +1}.");
    }
    else
    {
        Console.WriteLine("No empty spaces avialable.");
    }


}

// Get registration number
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

// Check vehicle type Car or Mc 


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

//Find first available space for a vehicle. 
static int FindEmptySpace(string[] parking, string vehicleType)
{
        for (int i = 0; i < parking.Length; i++)
        {
            if (parking[i] == "") 
            {
                return i;
            }
            if (vehicleType == "MC" && parking[i].StartsWith("MC#") && !parking[i].Contains('|'))
            {
                return i;
            }
        }

        return -1;
}
//Shows where the vehicle is parked
static void SearchVehicle(string[] parking)
{
    string registration = ReadRegistration();
    int index = FindVehicle(parking, registration);

    if (index !=-1)
    {
        Console.WriteLine($"Vehicle found in space {index +1}.");
    }
    else
    {
        Console.WriteLine("Vehicle not found.");
    }
}

// Returns the index of where the vehicle is.
static int FindVehicle(string[] parking, string registration)
{
    for (int i = 0;i < parking.Length;i++)
    {
        string[] vehicles = parking[i].Split('|');

        foreach (string vehicle in vehicles)
        {
            if (vehicle.EndsWith("#" + registration))
            {
                return i;
            }
        }
    }
    return -1;
}

// Remove a vehicle from the parking garage.

static void RemoveVehicle(string[] parking)
{
    string registration = ReadRegistration() ;
    int index = FindVehicle(parking, registration);

    if (index == -1)
    {
        Console.WriteLine("Vehicle not found.");
        return;
    }

    RemoveFromSpace(parking, index ,registration);

    Console.WriteLine($"Vehicle removed from space {index +1}.");
}
// Remove one MC while keeping the other one.
static void RemoveFromSpace(string[] parking, int index ,string registration)
{
    string[] vehicle = parking[index].Split("|");

    if (vehicle.Length == 1)
    {
        parking[index] = "";
    }
    else if (vehicle[0].EndsWith("#" + registration))
    {
        parking[index] = vehicle[1];
    }
    else
    {
        parking[index]=vehicle[0];
    }
}
// Reads a parking space number and returns its index.
static int ReadParkingSpace()
{
    while (true)
    {
        Console.WriteLine("Enter new parking space (1-100): ");
        string input= Console.ReadLine() ?? "";

        if (int.TryParse(input, out int space) && space >= 1 && space <=100)
        {
            return space - 1;
        }

        Console.WriteLine("Enter a number between 1 and 100.");
    }
}
// returns complete vehicle´s information
static string GetVehicle(string parkingSpace, string registration)
{
    string[] vehicles = parkingSpace.Split("|");

    foreach (string vehicle in vehicles)
    {
        if (vehicle.EndsWith("#" + registration))
        {
            return vehicle;
        }
    }
    return "";
}
// checks whether a vehicle can be parked in a selected space
static bool CanParkInSpace(string parkingSpace, string vehicle)
{
    if (parkingSpace == "")
    {
        return true;
    }
    if (vehicle.StartsWith("MC#") && parkingSpace.StartsWith("MC#") && !parkingSpace.Contains('|'))
    {
        return true;
    }
    return false;
}
// move a vehicle to a selected space.
static void MoveVehicle(string[] parking)
{
    string registration = ReadRegistration();
    int oldIndex = FindVehicle(parking, registration);

    if (oldIndex == -1)
    {
        Console.WriteLine("Vehicle not found.");
        return;
    }

    int newIndex = ReadParkingSpace();

    if (oldIndex == newIndex)
    {
        Console.WriteLine("Vehicle is already in that space.");
        return;
    }

    string vehicle = GetVehicle(parking[oldIndex], registration);
    if (!CanParkInSpace(parking[newIndex], vehicle))
    {
        Console.WriteLine("The selected space is not available.");
        return;
    }


    RemoveFromSpace(parking, oldIndex, registration);//remove vehicle from old space before adding it to a new one,

    if (parking[newIndex]=="")
    {
        parking[newIndex] = vehicle;
    }
    else
    {
        parking[newIndex] += "|" + vehicle;
    }

    Console.WriteLine($"Vehicle moved from space {oldIndex +1} to space {newIndex +1}.");
}