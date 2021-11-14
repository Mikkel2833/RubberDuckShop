using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using RubberDuckShop.ProductService.Application.Common.Interfaces;
using RubberDuckShop.ProductService.Application.Common.Mappings;
using RubberDuckShop.ProductService.Application.Common.Models;
using RubberDuckShop.ProductService.Application.DTOs;
using RubberDuckShop.ProductService.Infrastructure.Extensions;
using RubberDuckShop.ProductService.Domain.Entities;

namespace RubberDuckShop.ProductService.Application.Queries.Product;

public class GetProductsPaginatedQuery : IRequest<PaginatedList<ProductDTO>>
{
    public Guid? ProductCategoryId { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}


public class GetProductsPaginatedQueryHandler : IRequestHandler<GetProductsPaginatedQuery, PaginatedList<ProductDTO>>
{
    private readonly IProductDBContext _context;
    private readonly IMapper _mapper;

    public GetProductsPaginatedQueryHandler(IProductDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ProductDTO>> Handle(GetProductsPaginatedQuery request, CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.True<Domain.Entities.Product>();

        if(request.ProductCategoryId.HasValue)
        {
            predicate = predicate.And(t => t.ProductCategoryId == request.ProductCategoryId);
        }

        return await _context.Products
            .Where(predicate)
            .OrderBy(x => x.Name) //TODO: Add OrderBy to PaginatedQuery
            .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
