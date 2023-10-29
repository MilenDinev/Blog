namespace Blog.Services
{
    using AutoMapper;
    using Data;
    using Data.Entities;
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
            ApplicationDbContext dbContext
            )
            : base(dbContext)
        {
            _mapper = mapper;
        }

        public async Task CreateAsync(TagCreateModel tagModel, string userId)
        {
            await ValidateCreateInputAsync(tagModel);

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

        private async Task ValidateCreateInputAsync(TagCreateModel tagModel)
        {
            var isAnyTag = await AnyByStringAsync(tagModel.Value);
            if (isAnyTag)
                throw new ResourceAlreadyExistsException(string.Format(
                    ErrorMessages.EntityAlreadyExists,
                    typeof(Tag).Name, tagModel.Value));
        }
    }
}
