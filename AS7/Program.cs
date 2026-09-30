using System;

// DeliveryAddress بيفضل struct (من Assignment 01)
public struct DeliveryAddress
{
    public string City { get; set; }
    public string Street { get; set; }

    public DeliveryAddress(string city, string street)
    {
        City = city;
        Street = street;
    }
}

// ================= Parent Class =================
public class Shipment
{
    // private fields
    private string _trackingCode = "";
    private string _description = "";
    private decimal _weight;
    private decimal _deliveryFee;

    // public properties + validation
    public string TrackingCode
    {
        get { return _trackingCode; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Tracking code cannot be empty.");
            _trackingCode = value;
        }
    }

    public string Description
    {
        get { return _description; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Description cannot be empty.");
            _description = value;
        }
    }

    public decimal Weight
    {
        get { return _weight; }
        set
        {
            if (value <= 0)
                throw new ArgumentException("Weight must be greater than 0.");
            _weight = value;
        }
    }

    public decimal DeliveryFee
    {
        get { return _deliveryFee; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Delivery fee cannot be negative.");
            _deliveryFee = value;
        }
    }

    public DeliveryAddress Destination { get; set; }

    // Constructor Overloading
    public Shipment(string trackingCode, string description, decimal weight, decimal deliveryFee)
        : this(trackingCode, description, weight, deliveryFee, new DeliveryAddress())
    {
    }

    public Shipment(string trackingCode, string description, decimal weight,
                    decimal deliveryFee, DeliveryAddress destination)
    {
        TrackingCode = trackingCode;
        Description = description;
        Weight = weight;
        DeliveryFee = deliveryFee;
        Destination = destination;
    }

    // virtual: الـ children يقدروا يعملوا override
    public virtual decimal EstimatedCost
    {
        get { return DeliveryFee + (Weight * 5); }
    }

    public void UpdateDeliveryFee(decimal newFee)
    {
        DeliveryFee = newFee;
    }

    public virtual void PrintShipment()
    {
        Console.WriteLine("Shipment");
        Console.WriteLine();
        Console.WriteLine($"Tracking Code  : {TrackingCode}");
        Console.WriteLine($"Description    : {Description}");
        Console.WriteLine($"Weight         : {Weight} KG");
        Console.WriteLine($"Delivery Fee   : {DeliveryFee} EGP");
        Console.WriteLine($"Estimated Cost : {EstimatedCost} EGP");
    }
}

// ================= StandardShipment =================
public class StandardShipment : Shipment
{
    // constructor chaining بـ base(...)
    public StandardShipment(string trackingCode, string description, decimal weight,
                            decimal deliveryFee, DeliveryAddress destination)
        : base(trackingCode, description, weight, deliveryFee, destination)
    {
    }

    public override void PrintShipment()
    {
        Console.WriteLine("Standard Shipment");
        Console.WriteLine();
        Console.WriteLine($"Tracking Code  : {TrackingCode}");
        Console.WriteLine($"Description    : {Description}");
        Console.WriteLine($"Weight         : {Weight} KG");
        Console.WriteLine($"Delivery Fee   : {DeliveryFee} EGP");
        Console.WriteLine($"Estimated Cost : {EstimatedCost} EGP");
    }
}

// ================= ExpressShipment =================
public class ExpressShipment : Shipment
{
    private decimal _extraFee;

    public decimal ExtraFee
    {
        get { return _extraFee; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Extra fee must be greater than or equal to 0.");
            _extraFee = value;
        }
    }

    public ExpressShipment(string trackingCode, string description, decimal weight,
                           decimal deliveryFee, DeliveryAddress destination, decimal extraFee)
        : base(trackingCode, description, weight, deliveryFee, destination)
    {
        ExtraFee = extraFee;
    }

    // DeliveryFee + (Weight * 5) + ExtraFee
    public override decimal EstimatedCost
    {
        get { return base.EstimatedCost + ExtraFee; }
    }

    public override void PrintShipment()
    {
        Console.WriteLine("Express Shipment");
        Console.WriteLine();
        Console.WriteLine($"Tracking Code  : {TrackingCode}");
        Console.WriteLine($"Description    : {Description}");
        Console.WriteLine($"Weight         : {Weight} KG");
        Console.WriteLine($"Delivery Fee   : {DeliveryFee} EGP");
        Console.WriteLine($"Extra Fee      : {ExtraFee} EGP");
        Console.WriteLine($"Estimated Cost : {EstimatedCost} EGP");
    }
}

// ================= InternationalShipment =================
public class InternationalShipment : Shipment
{
    private string _destinationCountry = "";
    private decimal _customsFee;

    public string DestinationCountry
    {
        get { return _destinationCountry; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Destination country cannot be empty.");
            _destinationCountry = value;
        }
    }

    public decimal CustomsFee
    {
        get { return _customsFee; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Customs fee must be greater than or equal to 0.");
            _customsFee = value;
        }
    }

    public InternationalShipment(string trackingCode, string description, decimal weight,
                                 decimal deliveryFee, DeliveryAddress destination,
                                 string destinationCountry, decimal customsFee)
        : base(trackingCode, description, weight, deliveryFee, destination)
    {
        DestinationCountry = destinationCountry;
        CustomsFee = customsFee;
    }

    // DeliveryFee + (Weight * 5) + CustomsFee
    public override decimal EstimatedCost
    {
        get { return base.EstimatedCost + CustomsFee; }
    }

    public override void PrintShipment()
    {
        Console.WriteLine("International Shipment");
        Console.WriteLine();
        Console.WriteLine($"Tracking Code       : {TrackingCode}");
        Console.WriteLine($"Description         : {Description}");
        Console.WriteLine($"Weight              : {Weight} KG");
        Console.WriteLine($"Delivery Fee        : {DeliveryFee} EGP");
        Console.WriteLine($"Destination Country : {DestinationCountry}");
        Console.WriteLine($"Customs Fee         : {CustomsFee} EGP");
        Console.WriteLine($"Estimated Cost      : {EstimatedCost} EGP");
    }
}