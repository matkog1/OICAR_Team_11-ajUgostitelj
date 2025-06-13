﻿using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.DTOs;

namespace WPF.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetAllAsync(string token);
        Task<ProductDto?> GetProductByIdAsync(string token, int id);
        Task<ProductDto> CreateAsync(string token, ProductDto product);
        Task UpdateAsync(string token, int id, ProductDto product);
        Task DeleteAsync(string token, int id);
        Task<IEnumerable<ProductDto>> GetByCategoryAsync(string token, int categoryId);

    }
}
