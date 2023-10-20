﻿
using Domain.Entities;
using Domain.Repositories;
using Domain.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class GraphicCardRepository : RepositoryBase<GraphicCard>, IGraphicCardRepository
    { 
        public GraphicCardRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }

        public async Task<PagedList<GraphicCard>> GetAllGraphicCardsAsync(ProductParameters productParameters, bool trackChanges)
        {
            var graphicCards = await FindAll(trackChanges)
            .OrderBy(c => c.Id)
            .Skip((productParameters.PageNumber - 1) * productParameters.PageSize)
            .Take(productParameters.PageSize)
            .ToListAsync();

            var count = await FindAll(trackChanges).CountAsync();

            return new PagedList<GraphicCard> (graphicCards, count, productParameters.PageNumber, productParameters.PageSize);
                
        }
           
        public async Task<GraphicCard> GetGraphicCardAsync(int graphicCardId, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(graphicCardId), trackChanges).SingleOrDefaultAsync();
    }
}