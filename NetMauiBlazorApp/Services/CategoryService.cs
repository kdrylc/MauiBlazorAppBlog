﻿using NetMauiBlazorApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace NetMauiBlazorApp.Services
{
    public class CategoryService : ICategoryService
    {
        public async Task<bool> AddUpdateCategory(CategoryModel catModel)
        {
            string json = JsonConvert.SerializeObject(catModel);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            if (catModel.Id == 0)
            {
                var url = $"{Setting.BaseUrl}{APIs.CreateCategory}";
                client.BaseAddress = new Uri(url);
                HttpResponseMessage rp = await client.PostAsync("", content);
                if (rp.IsSuccessStatusCode)
                {
                    return await Task.FromResult(true);
                }
            }
            else
            {
                var url = $"{Setting.BaseUrl}{APIs.UpdateCategory}";
                client.BaseAddress = new Uri(url);
                HttpResponseMessage responseMessage = await client.PostAsync("", content);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return await Task.FromResult(true);
                }
            }
            return await Task.FromResult(true);

        }

        public async Task<bool> DeleteCategory(int id)
        {
            var obj = new DeleteCategoryDTO { Id = id };
            using (HttpClient client = new HttpClient())
            {
                var url = $"{Setting.BaseUrl}{APIs.DeleteCategory}";
                var serializedStr = JsonConvert.SerializeObject(obj);

                var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
                HttpResponseMessage rms = await client.DeleteAsync(url);
                if (rms.IsSuccessStatusCode)
                {
                    return await Task.FromResult(true);
                }
                else
                {
                    return await Task.FromResult(false);

                }
            }
        }

        public async Task<CategoryModel> GetById(int id)
        {

            var returnResponse = new CategoryModel();
            try
            {
                using (var client = new HttpClient())
                {
                    string url = $"{Setting.BaseUrl}{APIs.GetByIdCategory}{id}";
                    var apiResponse = await client.GetAsync(url);

                    if (apiResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var response = await apiResponse.Content.ReadAsStringAsync();
                        var deserilizeResponse = JsonConvert.DeserializeObject<MainResponse>(response);

                        if (deserilizeResponse.IsSuccess)
                        {
                            returnResponse = JsonConvert.DeserializeObject<CategoryModel>(deserilizeResponse.Content.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return returnResponse;



        }

        public async Task<List<CategoryModel>> GettAllCategory()
        {
            var returnResponse = new List<CategoryModel>();
            using (var client = new HttpClient())
            {
                var url = $"{Setting.BaseUrl}{APIs.GetAllCategory}";

                var response = await client.GetAsync(url);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    bool isTokenRefreshed = await RefreshToken();
                    if (isTokenRefreshed) return await GettAllCategory();
                }
                else
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string contentStr = await response.Content.ReadAsStringAsync();
                        var mainResponse = JsonConvert.DeserializeObject<MainResponse>(contentStr);
                        if (mainResponse.IsSuccess)
                        {
                            returnResponse = JsonConvert.DeserializeObject<List<CategoryModel>>(mainResponse.Content.ToString());
                        }
                    }
                }

            }
            return returnResponse;
        }


        public async Task<bool> RefreshToken()
        {
            bool isTokenRefreshed = false;
            using (var client = new HttpClient())
            {
                var url = $"{Setting.BaseUrl}{APIs.RefreshToken}";

                var serializedStr = JsonConvert.SerializeObject(new AuthenticationRequestAndResponse
                {
                    RefreshToken = Setting.UserBasicDetail.RefreshToken,
                    AccessToken = Setting.UserBasicDetail.AccessToken
                });

                try
                {
                    var response = await client.PostAsync(url, new StringContent(serializedStr, Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        string contentStr = await response.Content.ReadAsStringAsync();
                        var mainResponse = JsonConvert.DeserializeObject<MainResponse>(contentStr);
                        if (mainResponse.IsSuccess)
                        {
                            var tokenDetails = JsonConvert.DeserializeObject<AuthenticationRequestAndResponse>(mainResponse.Content.ToString());
                            Setting.UserBasicDetail.AccessToken = tokenDetails.AccessToken;
                            Setting.UserBasicDetail.RefreshToken = tokenDetails.RefreshToken;

                            string userDetailsStr = JsonConvert.SerializeObject(Setting.UserBasicDetail);
                            await SecureStorage.SetAsync(nameof(Setting.UserBasicDetail), userDetailsStr);
                            isTokenRefreshed = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                }


            }
            return isTokenRefreshed;
        }

    }
}
