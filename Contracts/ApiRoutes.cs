namespace Contracts
{
    public static class ApiRoutes
    {
        public const string Base1 = "api/V1";

        public static class Polls
        {
            public static readonly string CreateNewPoll = $"/{Base1}/Poll";
            public static readonly string UpdatePoll = $"/{Base1}/Poll/" + "{Id}";
            public static readonly string DeletePoll = $"/{Base1}/Poll/" + "{Id}";
            public static readonly string GetSinglePoll = $"/{Base1}/Poll/" + "{Id}";
            public static readonly string GetAllPolls = $"/{Base1}/Poll";
        }

        public static class Categories
        {
            // polls categories operations
            public static readonly string CreateACategoryForPoll = $"/{Base1}/Poll/" + "{PollId}/Category";
            public static readonly string GetAllCategoriesOfPoll = $"/{Base1}/Poll/"+ "{PollId}/Category";

            public static readonly string GetSingleCategoryOfPoll = $"/{Base1}/Category/" + "{Id}";
            public static readonly string RemoveACategoryOfPoll = $"/{Base1}/Category/" + "{Id}";

            // polls category nominees operations
            public static readonly string AddNomineeToCategory = $"/{Base1}/Category/" + "{CategoryId}/Nominee/{NomineeId}";
            public static readonly string RemoveNomineeFromCategory = $"/{Base1}/Category/" + "{CategoryId}/Nominee/{NomineeId}";
        }

        public static class Nominees 
        {
            public static readonly string AddNewNominee = $"/{Base1}/Nominee";
            public static readonly string DeleteNominee = $"/{Base1}/Nominee/" + "{Id}";
            public static readonly string UpdateNominee = $"/{Base1}/Nominee/" + "{Id}";
            public static readonly string GetAllNominees = $"/{Base1}/Nominee";
            public static readonly string GetASingleNominee = $"/{Base1}/Nominee/" + "{Id}";
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
            public const string PerformMasterRegistration = Base1 + "/Identity/Master/Register";
        }
    }
}
