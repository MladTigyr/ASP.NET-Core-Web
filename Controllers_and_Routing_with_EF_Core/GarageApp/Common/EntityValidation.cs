namespace GarageApp.Common
{
    public class EntityValidation
    {
        //Car
        public const int MinCarMakeLength = 2;
        public const int MaxCarMakeLength = 50;
        public const int MinCarModelLength = 2;
        public const int MaxCarModelLength = 40;

        //Garage
        public const int MinGarageNameLength = 3;
        public const int MaxGarageNameLength = 100;
        public const int MinGarageLocationLength = 5;
        public const int MaxGarageLocationLength = 120;
    }
}
