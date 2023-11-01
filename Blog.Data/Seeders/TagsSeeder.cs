namespace Blog.Data.Seeders
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;

    internal class TagsSeeder
    {
        internal static async Task SeedAsync(ApplicationDbContext dbContext)
        {
            var creator = await dbContext.Users.SingleOrDefaultAsync();
            var tags = new List<Tag>()
            {
                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "AI Detection",
                    NormalizedTag = "AIDetection".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "Aggregators",
                    NormalizedTag = "Aggregators".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "Avatar",
                    NormalizedTag = "Avatar".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "Chat",
                    NormalizedTag = "Chat".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "Copywriting",
                    NormalizedTag = "Copywriting".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "Finance",
                    NormalizedTag = "Finance".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "For Fun",
                    NormalizedTag = "ForFun".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "Gaming",
                    NormalizedTag = "Gaming".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "Generative Art",
                    NormalizedTag = "GenerativeArt".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "Generative Code",
                    NormalizedTag = "GenerativeCode".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "Generative Video",
                    NormalizedTag = "GenerativeVideo".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "Image Improvement",
                    NormalizedTag = "ImageImprovement".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "Image Scanning",
                    NormalizedTag = "ImageScanning".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "Inspiration",
                    NormalizedTag = "Inspiration".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "Marketing",
                    NormalizedTag = "Marketing".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "Motion Capture",
                    NormalizedTag = "MotionCapture".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },           
                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "Music",
                    NormalizedTag = "Music".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "Podcasting",
                    NormalizedTag = "Podcasting".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "Productivity",
                    NormalizedTag = "Productivity".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "Prompt Guides",
                    NormalizedTag = "PromptGuides".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "Research",
                    NormalizedTag = "Research".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "Self Improvement",
                    NormalizedTag = "SelfImprovement".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "Social Media",
                    NormalizedTag = "SocialMedia".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "Speech-To-Text",
                    NormalizedTag = "SpeechToText".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "Text-To-Speech",
                    NormalizedTag = "TextToSpeech".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "Text-To-Video",
                    NormalizedTag = "TextToVideo".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "Translation",
                    NormalizedTag = "Translation".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "Video Editing",
                    NormalizedTag = "VideoEditing".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "Voice Modulation",
                    NormalizedTag = "VoiceModulation".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },      
            };

            await dbContext.Tags.AddRangeAsync(tags);
            await dbContext.SaveChangesAsync();
        }
    }
}