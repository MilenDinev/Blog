namespace Blog.Data.Models.Constants
{
    internal static class AttributesParams
    {
        internal const int TitleMinLength = 5;
        internal const int TitleMaxLength = 100;

        internal const int DescriptionMinLength = 20;
        internal const int DescriptionMaxLength = 200;

        internal const int ContentMinLength = 200;

        internal const int ProviderMinLength = 3;
        internal const int ProviderMaxLength = 50;

        internal const int StrategyMinLength = 3;
        internal const int StrategyMaxLength = 20;

        internal const int TagValueMinLength = 3;
        internal const int TagValueMaxLength = 10;

        internal const int RelatedToolNameMinLength = 3;
        internal const int RelatedToolNameMaxLength = 30;
    }
}
