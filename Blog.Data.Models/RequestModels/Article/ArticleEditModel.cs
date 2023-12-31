﻿namespace Blog.Data.Models.RequestModels.Article
{
    using System.ComponentModel.DataAnnotations;
    using Common.Constants;

    public class ArticleEditModel
    {
        [Required]
        public string? Id { get; set; }

        [StringLength(AttributesParams.ArticleTitleMaxLength,
            ErrorMessage = ValidationMessages.MinMaxLength,
            MinimumLength = AttributesParams.ArticleTitleMinLength)]
        public string? Title { get; set; }

        [Url(ErrorMessage = ValidationMessages.URL)]
        public string? Url { get; set; }

        [Url(ErrorMessage = ValidationMessages.URL)]
        public string? ImageUrl { get; set; }

        [StringLength(AttributesParams.ProviderMaxLength,
            ErrorMessage = ValidationMessages.MinMaxLength,
            MinimumLength = AttributesParams.ProviderMinLength)]
        public string? ProviderName { get; set; }
    }
}
