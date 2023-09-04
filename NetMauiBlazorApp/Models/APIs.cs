using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMauiBlazorApp.Models
{
    public class APIs
    {
        public const string AuthenticateUser = "/api/User/AuthenticateUser";
        public const string RegisterUser = "/api/User/RegisterUser";
        public const string RefreshToken = "/api/User/RefreshToken";


        public const string GetAllCategory = "/api/Category";
        public const string CreateCategory = "/api/Category/AddCategory";
        public const string UpdateCategory = "/api/Category/UpdateCategory";
        public const string DeleteCategory = "/api/Category/DeleteCategory";
        public const string GetByIdCategory = "/api/Category/GetCategoryByCategoryID/";



        public const string GetAllArticle = "/api/Article";
        public const string CreateArticle = "/api/Article/AddArticle";
        public const string UpdateArticle = "/api/Article/UpdateArticle";
        public const string DeleteArticle = "/api/Article/DeleteArticle";
        public const string GetByIdArticle = "/api/Article/GetArticleByArticleID/";
        public const string GetByArticleByCategroyId = "/api/Article/GetArticleByCategoryID/";
        public const string GetByArticleByUserId = "/api/Article/GetArticleByUserID/";
    }
}
