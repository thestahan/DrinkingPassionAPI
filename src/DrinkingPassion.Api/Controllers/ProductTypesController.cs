﻿using AutoMapper;
using DrinkingPassion.Api.Core.Entities;
using DrinkingPassion.Api.Core.Interfaces;
using DrinkingPassion.Api.Core.Specifications.ProductTypes;
using DrinkingPassion.Api.Dtos.Products;
using DrinkingPassion.Api.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrinkingPassion.Api.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductTypesController : BaseApiController
    {
        private readonly IGenericRepository<ProductType> _repo;
        private readonly IMapper _mapper;

        public ProductTypesController(IGenericRepository<ProductType> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductTypeToReturnDto>>> GetProductTypes()
        {
            var spec = new ProductTypesOrderedByNameSpec();

            var types = await _repo.ListAsync(spec);

            var typesToReturn = _mapper.Map<IReadOnlyList<ProductTypeToReturnDto>>(types);

            return Ok(typesToReturn);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductTypeToReturnDto>> GetProductTypeById(int id)
        {
            var type = await _repo.GetByIdAsync(id);

            if (type == null)
            {
                return NotFound(new ApiErrorResponse(404));
            }

            var typeToReturn = _mapper.Map<ProductTypeToReturnDto>(type);

            return Ok(typeToReturn);
        }

        [HttpPost]
        public async Task<ActionResult<ProductTypeToAddDto>> AddProductType(ProductTypeToAddDto typeToAddDto)
        {
            var type = _mapper.Map<ProductType>(typeToAddDto);

            var createdType = await _repo.AddAsync(type);

            var typeToReturn = _mapper.Map<ProductTypeToReturnDto>(createdType);

            return CreatedAtAction(nameof(GetProductTypeById), new { id = typeToReturn.Id }, typeToReturn);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProductType(int id, ProductTypeToUpdateDto typeToUpdate)
        {
            if (id != typeToUpdate.Id)
            {
                return BadRequest(new ApiErrorResponse(400, "Id does not match with entity's id"));
            }

            if (!await _repo.EntityExistsAsync(id))
            {
                return BadRequest(new ApiErrorResponse(400, "Entity does not exist"));
            }

            var type = _mapper.Map<ProductType>(typeToUpdate);

            await _repo.UpdateAsync(type);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProductType(int id)
        {
            if (!await _repo.DeleteByIdAsync(id))
            {
                return NotFound(new ApiErrorResponse(404));
            }

            return NoContent();
        }
    }
}
