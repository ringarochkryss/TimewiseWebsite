namespace Salto.Data
{
    public class ArticleRepository
    {
        private readonly ApplicationDbContext _context;

        public ArticleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void SetFeaturedArticle(int articleId, bool featured)
        {
            var featuredArticle = _context.Articles.Find(articleId);
            if (featuredArticle != null)
            {
                featuredArticle.Featured = true;

                // Set all other articles as not featured
                var otherArticles = _context.Articles.Where(a => a.ArticleID != articleId);
                foreach (var article in otherArticles)
                {
                    article.Featured = false;
                }

                _context.SaveChanges();
            }
        }

    }
}

