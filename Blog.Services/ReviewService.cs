namespace Blog.Services
{
    using AutoMapper;
    using Data;
    using Data.Entities;
    using Data.Models.RequestModels.Review;
    using Base;
    using Constants;
    using Interfaces;
    using Handlers.Exceptions;

    public class ReviewService : ReviewBaseService,  IReviewService
    {
        private readonly IMapper mapper;    

        public ReviewService(
            IMapper mapper,
            ApplicationDbContext dbContext
            ) 
            : base(dbContext)
        {
            this.mapper = mapper;
        }

        public async Task CreateAsync(ReviewCreateModel reviewModel, string userId)
        {
            await this.ValidateCreateInputAsync(reviewModel);

            var review = mapper.Map<Review>(reviewModel);

            await CreateEntityAsync(review, userId);
        }

        public async Task EditAsync(ReviewEditModel reviewModel, string reviewId, string modifierId)
        {
            var review = await this.GetByIdAsync(reviewId);

            review.Title = reviewModel.Title ?? review.Title;
            review.Description = reviewModel.Description ?? review.Description;
            review.Content = reviewModel.Content ?? review.Content;
            review.ImageUrl = reviewModel.ImageUrl ?? review.ImageUrl;
            review.VideoUrl = reviewModel.VideoUrl ?? review.VideoUrl;
            review.ExternalArticleUrl = reviewModel.ExternalArticleUrl ?? review.ExternalArticleUrl;
            review.TopPick = reviewModel.TopPick ?? review.TopPick;
            review.SpecialOffer = reviewModel.TopPick ?? review.SpecialOffer;
                      
            await SaveModificationAsync(review, modifierId);
        }

        public async Task DeleteAsync(string reviewId, string modifierId)
        {
            var review = await this.GetByIdAsync(reviewId);

            await DeleteEntityAsync(review, modifierId);
        }

        public async Task Vote(bool type, string reviewId, string userId)
        {
            var review = await this.GetByIdAsync(reviewId);
            var isTheUserAlreadyVoted = review.Votes.Any(x => x.UserId == userId && !x.Type == type);
            
            if (isTheUserAlreadyVoted)
            {
                var oldVote = review.Votes.FirstOrDefault(x => x.UserId == userId);
                oldVote.Deleted = true;

                var currentVote = review.Votes.FirstOrDefault(x => x.UserId == userId && x.Type == type);

                if (currentVote != null)
                {
                    currentVote.Deleted = false;
                    await SaveModificationAsync(review, userId);
                    return;
                }
            }

            var vote = new Vote
            {
                Id = Guid.NewGuid().ToString(),
                Type = type,
                ReviewId = review.Id,
                UserId = userId
            };

            await this.dbContext.Votes.AddAsync(vote);
            await SaveModificationAsync(review, userId);
        }

        private async Task ValidateCreateInputAsync(ReviewCreateModel reviewModel)
        {
            var isAnyReview = await this.AnyByTitleAsync(reviewModel.Title);
            if (isAnyReview)
                throw new ResourceAlreadyExistsException(string.Format(
                    ErrorMessages.EntityAlreadyExists,
                    typeof(Review).Name, reviewModel.Title));
        }
    }
}
