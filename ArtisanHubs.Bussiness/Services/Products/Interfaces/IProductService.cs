﻿using ArtisanHubs.API.DTOs.Common;
using ArtisanHubs.DTOs.DTO.Reponse.Products;
using ArtisanHubs.DTOs.DTO.Request.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtisanHubs.Bussiness.Services.Products.Interfaces
{
    public interface IProductService
    {      
        Task<ApiResponse<IEnumerable<ProductResponse>>> GetMyProductsAsync(int artistId);     
        Task<ApiResponse<ProductResponse?>> GetMyProductByIdAsync(int productId, int artistId);        
        Task<ApiResponse<ProductResponse>> CreateProductAsync(int artistId, CreateProductRequest request);       
        Task<ApiResponse<ProductResponse?>> UpdateProductAsync(int productId, int artistId, UpdateProductRequest request);
        Task<ApiResponse<bool>> DeleteProductAsync(int productId, int artistId);
    }
}
