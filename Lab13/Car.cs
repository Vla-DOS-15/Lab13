namespace Lab13
{
    // Full description of processor class:
    public class Car
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public uint YearOfCreation { get; set; }
        public int Price { get; set; }
        public bool KeyWorkingOrder { get; set; }

        public Car() { }

        public Car(string _make, string _model, string color, uint yearOfCreation, int price, bool keyWorkingOrder)
        {
            Make = _make;
            Model = _model;
            Color = color;
            YearOfCreation = yearOfCreation;
            Price = price;
            KeyWorkingOrder = keyWorkingOrder;
        }

    }
}
