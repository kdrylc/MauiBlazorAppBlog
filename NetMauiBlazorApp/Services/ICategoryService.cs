using NetMauiBlazorApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMauiBlazorApp.Services
{
	public interface ICategoryService
	{
		public Task<List<CategoryModel>> GettAllCategory();
		public Task<CategoryModel> GetById (int id);
		public Task<bool> AddUpdateCategory(CategoryModel catModel);
		public Task<bool> DeleteCategory(int id);

	}
}
