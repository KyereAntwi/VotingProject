namespace Contracts
{
    public static class ApiRoutes
    {
        public const string Base1 = "api/V1";

        public static class Polls
        {
            public const string CreateNewPoll = Base1 + "/Poll";
            public const string UpdatePoll = Base1 + "/Poll/{Id}";
            public const string DeletePoll = Base1 + "/Poll/{Id}";
            public const string GetSinglePoll = Base1 + "/Poll/{Id}";
            public const string GetAllPolls = Base1 + "/Poll";
        }

        public static class Categories
        {
            // polls categories operations
            public const string CreateACategoryForPoll = Base1 + "/Poll/{PollId}/Category";
            public const string GetAllCategoriesOfPoll = Base1 + "/Poll/{PollId}/Category";
            public const string GetAllCategoriesOfPollAvailable = Base1 + "/Poll/{PollId}/AvailableCategories/{Username}";

            public const string GetSingleCategoryOfPoll = Base1 + "/Category/{Id}";
            public const string RemoveACategoryOfPoll = Base1 + "/Category/{Id}";

            // polls category nominees operations
            public const string AddNomineeToCategory = Base1 + "/Category/{CategoryId}/Nominee/{NomineeId}";
            public const string RemoveNomineeFromCategory = Base1 + "/Category/{CategoryId}/Nominee/{NomineeId}";
        }

        public static class Nominees 
        {
            public const string AddNewNominee = Base1 + "/Nominee";
            public const string DeleteNominee = Base1 + "/Nominee/{Id}";
            public const string UpdateNominee = Base1 + "/Nominee/{Id}";
            public const string GetAllNominees = Base1 + "/Nominee";
            public const string GetASingleNominee = Base1 + "/Nominee/{Id}";
        }

        public static class VotersRegister 
        {
            public const string Vote = Base1 + "/Voter/Vote";
            public const string GenerateVoterCode = Base1 + "/Voter/Code";
        }

        public static class Auth 
        {
            public const string Login = Base1 + "/Identity/Login";
            public const string Register = Base1 + "/Identity/Register";
            public const string Refresh = Base1 + "/Identity/Refresh";
            public const string PerformMasterRegistration = Base1 + "/Identity/Master/Register";
            public const string DeleteUser = Base1 + "/Identity/DeleteUser/{Username}";
            public const string GetUser = Base1 + "/Identity/User/{Username}";
        }
    }
}
