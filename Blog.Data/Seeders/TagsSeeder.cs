namespace Blog.Data.Seeders
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.EntityFrameworkCore;

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
                    Value = "aIDetection",
                    NormalizedTag = "aIDetection".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "aggregators",
                    NormalizedTag = "aggregators".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "avatar",
                    NormalizedTag = "avatar".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "chat",
                    NormalizedTag = "chat".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "copywriting",
                    NormalizedTag = "copywriting".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "finance",
                    NormalizedTag = "finance".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "forFun",
                    NormalizedTag = "forFun".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "gaming",
                    NormalizedTag = "gaming".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "generativeArt",
                    NormalizedTag = "generativeArt".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "generativeArt",
                    NormalizedTag = "generativeArt".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "generativeCode",
                    NormalizedTag = "generativeCode".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "generativeVideo",
                    NormalizedTag = "generativeVideo".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "imageImprovement",
                    NormalizedTag = "imageImprovement".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "imageScanning",
                    NormalizedTag = "imageScanning".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "inspiration",
                    NormalizedTag = "inspiration".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "marketing",
                    NormalizedTag = "marketing".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "motionCapture",
                    NormalizedTag = "motionCapture".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },           
                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "music",
                    NormalizedTag = "music".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "podcasting",
                    NormalizedTag = "podcasting".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "productivity",
                    NormalizedTag = "productivity".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "promptGuides",
                    NormalizedTag = "promptGuides".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "research",
                    NormalizedTag = "research".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "selfImprovement",
                    NormalizedTag = "selfImprovement".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "socialMedia",
                    NormalizedTag = "socialMedia".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "speechToText",
                    NormalizedTag = "speechToText".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "textToSpeech",
                    NormalizedTag = "textToSpeech".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "textToVideo",
                    NormalizedTag = "textToVideo".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "translation",
                    NormalizedTag = "translation".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "videoEditing",
                    NormalizedTag = "videoEditing".ToUpper(),
                    CreatorId = creator.Id,
                    LastModifierId = creator.Id,
                    CreationDate = DateTime.UtcNow,
                    LastModifiedOn = DateTime.UtcNow
                },

                new Tag()
                {
                    Id = Guid.NewGuid().ToString(),
                    Value = "voiceModulation",
                    NormalizedTag = "voiceModulation".ToUpper(),
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