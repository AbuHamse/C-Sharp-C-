using System;

class Car
{
    // Public fields
    public string Brand;
    public string Model;
    public string Color;
    public int Year;

    // Constructor
    public Car(string brand, string model, string color, int year)
    {
        this.Brand = brand;
        this.Model = model;
        this.Color = color;
        this.Year = year;
    }

    // Method to display car details
    public void DisplayDetails()
    {
        Console.WriteLine($"Car Details: Brand: {Brand}, Model: {Model}, Color: {Color}, Year: {Year}");
    }

    // Method to simulate driving the car
    public void Drive()
    {
        Console.WriteLine($"The {Color} {Brand} {Model} ({Year}) is now driving!");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create an instance of the Car class
        Car myCar = new Car("Toyota", "Corolla", "Blue", 2023);

        // Display car details
        myCar.DisplayDetails();

        // Simulate driving the car
        myCar.Drive();
    }
}
