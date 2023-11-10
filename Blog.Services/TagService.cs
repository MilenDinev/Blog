namespace Blog.Services
{
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using Data;
    using Data.Entities;
    using Data.Models.ViewModels.Tag;
    using Data.Models.RequestModels.Tag;
    using Interfaces;
    using Repository;

    public class TagService : Repository<Tag>, ITagService
    {
        private readonly IMapper _mapper;

        public TagService(
            IMapper mapper,
            ApplicationDbContext  _dbContext
            )
            : base( _dbContext)
        {
            _mapper = mapper;
        }

        public async Task CreateAsync(TagCreateModel tagModel, string userId)
        {
            await ValidateCreateInputAsync(tagModel.Value);

            var tag = _mapper.Map<Tag>(tagModel);

            await CreateEntityAsync(tag, userId);
        }

        public async Task<ICollection<TagViewModel>> GetTagViewModelBundleAsync()
        {
            var tagViewModelBundle = await  _dbContext.Tags
                .AsNoTracking()
                .Where(x => !x.Deleted)
                .Select(x => new TagViewModel
                {
                    Id = x.Id,
                    Value = x.Value,
                })
                .OrderBy(x => x.Value)
                .ToListAsync();

            return tagViewModelBundle;
        }
    }
}
