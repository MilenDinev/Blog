namespace Blog.Services.Common.Constants
{

    public static class ErrorMessages
    {
        public const string EntityDoesNotExist = @"{0} does not exist!";
        public const string EntityAlreadyExists = @"{0} - '{1}' already exists!";
        public const string TagAlreadyAssigned = @"{0} - '{1}' is already assigned";
        public const string TagNotAssigned = @"{0} - '{1}' is not assigned!";
        public const string Unauthorized = @"User is not authorized";
    }
}
