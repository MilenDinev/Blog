namespace Blog.Services
{
    using Microsoft.EntityFrameworkCore;
    using AutoMapper;
    using Data;
    using Data.Entities;
    using Data.Models.ViewModels.Tag;
    using Data.Models.RequestModels.Tag;
    using Constants;
    using Interfaces;
    using Repository;
    using Handlers.Exceptions;

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
            var isAnyTag = await AnyByStringAsync(tagModel.Value);
            if (isAnyTag)
                throw new ResourceAlreadyExistsException(string.Format(
                    ErrorMessages.EntityAlreadyExists,
                    typeof(Tag).Name, tagModel.Value));

            var tag = _mapper.Map<Tag>(tagModel);

            await CreateEntityAsync(tag, userId);
        }

        public async Task EditAsync(TagEditModel tagModel, string tagId, string modifierId)
        {
            var tag = await GetByIdAsync(tagId);

            tag.Value = tagModel.Value ?? tag.Value;

            await SaveModificationAsync(tag, modifierId);
        }

        public async Task DeleteAsync(string tagId, string modifierId)
        {
            var tag = await GetByIdAsync(tagId);

            await DeleteEntityAsync(tag, modifierId);
        }

        public async Task<Tag> GetTagByIdAsync(string tagId)
        {
            var tag = await GetByIdAsync(tagId);

            return tag;
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
