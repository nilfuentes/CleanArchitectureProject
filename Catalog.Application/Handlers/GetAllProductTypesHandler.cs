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
    public class GetAllProductTypesHandler : IRequestHandler<GetAllProductTypesQuery, IList<ProductTypeResponse>>
    {
        private readonly IProductTypeRepository _repository;

        public GetAllProductTypesHandler(IProductTypeRepository repository)
        {
            _repository = repository;
        }
        public async Task<IList<ProductTypeResponse>> Handle(GetAllProductTypesQuery request, CancellationToken cancellationToken)
        {
            var productTypes =await _repository.GetProductTypes();
            var productTypesResponse = 
                ProductMapper.Mapper.Map<IList<ProductType>,IList<ProductTypeResponse>>(productTypes.ToList());

            return productTypesResponse;
        }
    }
}
