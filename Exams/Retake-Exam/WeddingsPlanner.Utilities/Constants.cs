namespace WeddingsPlanner.Utilities
{
    public static class Constants
    {
        public const string RootDirectory = "../../..";
        public const string InvalidDataErrorMsg = "Error. Invalid data provided";

        public const string AgenciesImportLocation = RootDirectory + "/datasets/agencies.json";
        public const string PeopleImportLocation = RootDirectory + "/datasets/people.json";
        public const string WeddingsImportLocation = RootDirectory + "/datasets/weddings.json";
        public const string VenuesImportLocation = RootDirectory + "/datasets/venues.xml";
        public const string PresentsImportLocation = RootDirectory + "/datasets/presents.xml";

        public const string OrderedAgenciesExportLocation = RootDirectory + "/results/ordered-agencies.json";
        public const string GuestsExportLocation = RootDirectory + "/results/guests.json";
        public const string SofiaVenuesExportLocation = RootDirectory + "/results/sofia-venues.xml";
        public const string AgenciesByTownExportLocation = RootDirectory + "/results/agencies-by-town.xml";
    }
}
