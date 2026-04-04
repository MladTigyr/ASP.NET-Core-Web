namespace EventManager.Common
{
    public static class EntityValidation
    {
        public static class Event
        {
            public const int TitleMinLength = 5;
            public const int TitleMaxLength = 100;

            public const int DescriptionMinLength = 20;
            public const int DescriptionMaxLength = 1000;

            public const int ParticipantsMin = 1;
            public const int ParticipantsMax = 500;
        }

        public static class Category
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 50;
        }

        public static class Registration
        {
            public const int ParticipantNameMinLength = 2;
            public const int ParticipantNameMaxLength = 60;
        }
    }
}
