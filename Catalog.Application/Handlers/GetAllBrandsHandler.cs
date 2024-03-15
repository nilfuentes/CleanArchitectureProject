using AutoMapper;
using Catalog.Application.Mappers;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers
{
    public class GetAllBrandsHandler : IRequestHandler<GetAllBrandsQuery, IList<BrandResponse>>
    {
        private readonly IBrandRespository _brandRepository;       

        public GetAllBrandsHandler(IBrandRespository brandRepository)
        {
            _brandRepository = brandRepository;          
        }

        public async Task<IList<BrandResponse>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var brandList = await _brandRepository.GetBrands();
            var brandsResponseList = 
                ProductMapper.Mapper.Map<IList<Brand>,IList<BrandResponse>>(brandList.ToList());

            return brandsResponseList;
        }
    }
}
