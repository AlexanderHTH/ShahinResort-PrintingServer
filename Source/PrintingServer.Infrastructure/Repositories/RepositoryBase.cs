using Microsoft.EntityFrameworkCore;
using PrintingServer.Application.Common;
using PrintingServer.Domain.Common;
using PrintingServer.Domain.Repositories;
using PrintingServer.Infrastructure.Presistence;

namespace PrintingServer.Infrastructure.Repositories
{
    internal class RepositoryBase<T>(Print_DBContext print_DBContext) : IRepository<T> where T : class
    {
        protected readonly Print_DBContext _dbContext = print_DBContext;
        public async Task<T> Create(T obj, CancellationToken cancellationToken)
        {
            _dbContext.Set<T>().Add(obj);
            await SaveChangesAsync(cancellationToken);
            return obj;
        }
        public async Task Create(IEnumerable<T> entities, CancellationToken cancellationToken)
        {
            _dbContext.Set<T>().AddRange(entities);
            await SaveChangesAsync(cancellationToken);
        }
        public async Task Delete(T obj, CancellationToken cancellationToken)
        {
            _dbContext.Set<T>().Remove(obj);
            await SaveChangesAsync(cancellationToken);
        }
        public async Task Delete(IEnumerable<T> list, CancellationToken cancellationToken)
        {
            _dbContext.Set<T>().RemoveRange(list);
            await SaveChangesAsync(cancellationToken);
        }
        public async Task Update(T entity, CancellationToken cancellationToken)
        {
            _dbContext.Set<T>().Update(entity);
            await SaveChangesAsync(cancellationToken);
        }
        public async Task<IEnumerable<T>> ListAsync(CancellationToken cancellationToken)
        {
            var toreturn = await _dbContext.Set<T>().ToListAsync(cancellationToken);
            return toreturn;
        }
        public async Task<bool> IsEmpty() => !await _dbContext.Set<T>().AnyAsync();
        public async Task SaveChangesAsync(CancellationToken cancellationToken) => await _dbContext.SaveChangesAsync(cancellationToken);
        public async Task<IPagedResult<T>> WhereAsync(ISearchDTO<T> searchDTO, CancellationToken cancellationToken)
        {
            IQueryable<T>? list;
            SearchDTOValidator<T>.SearchDTOValid(searchDTO);
            #region Filter
            if (searchDTO.Conditions != null && searchDTO.Conditions.Count != 0)
            {
                list = _dbContext.Set<T>().Filter(searchDTO.Conditions);
            }
            else
            {
                list = _dbContext.Set<T>();
            }
            #endregion
            #region Sort
            if (list != null)
            {
                if (searchDTO.Sorts == null || searchDTO.Sorts.Count == 0)
                {
                    searchDTO.Sorts =
                    [
                        new ResultSort
                        {
                            Sort_FieldName="Id",
                            Sort_Direction = SortDirection.ASC
                        }
                    ];
                }
                list = list.Sort(searchDTO.Sorts);
            }
            #endregion
            #region Pagenation
            int totalItemCount = list == null ? 0 : list.Count();
            int totalPages = searchDTO.PageSize <= 0 ? 1 : (int)Math.Ceiling(totalItemCount / (double)searchDTO.PageSize);
            if (list != null && searchDTO.PageSize > 0 && searchDTO.PageNumber > 0)
            {
                searchDTO.PageNumber = searchDTO.PageSize > totalItemCount ? 1 : searchDTO.PageNumber;
                searchDTO.PageNumber = searchDTO.PageNumber > totalPages ? totalPages : searchDTO.PageNumber;
                list = list.Skip(searchDTO.PageSize * (searchDTO.PageNumber - 1)).Take(searchDTO.PageSize);
            }
            #endregion
            #region Preparing result
            if (list != null)
            {
                var toreturn = await list.ToListAsync(cancellationToken);
                return new PagedResult<T>(list, totalItemCount, searchDTO.PageSize, searchDTO.PageNumber);
            }
            else
            {
                return new PagedResult<T>();
            }
            #endregion
        }
    }
}