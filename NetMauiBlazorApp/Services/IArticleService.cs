using NetMauiBlazorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMauiBlazorApp.Services
{
    public interface IArticleService
    {
        public Task<List<ArticleModel>> GettAllArticle();
        public Task<ArticleModel> GetById(int id);
        public Task<bool> AddUpdateArticle(ArticleModel art);
        public Task<bool> DeleteArticle(int id);
        public Task<List<ArticleModel>> GetArticleByCategoryId(int categoryId);

        //Task<bool> GetArticleByUserID(int id);

    }
}
