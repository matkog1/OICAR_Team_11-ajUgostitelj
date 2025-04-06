﻿// TableService.cs
using AutoMapper;
using WebAPI.DTOs;
using WebAPI.Models;
using WebAPI.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace WebAPI.Services
{
    public class TableService : ITableService
    {
        private readonly IRepositoryFactory _repositoryFactory;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public TableService(IRepositoryFactory repositoryFactory, IMapper mapper, AppDbContext context)
        {
            _repositoryFactory = repositoryFactory;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<TableDto>> GetAllTablesAsync()
        {
            var repo = _repositoryFactory.GetRepository<Table>();
            var tables = await repo.GetAllAsync();
            return _mapper.Map<IEnumerable<TableDto>>(tables);
        }

        public async Task<TableDto?> GetTableByIdAsync(int id)
        {
            var repo = _repositoryFactory.GetRepository<Table>();
            var table = await repo.GetByIdAsync(id);
            return _mapper.Map<TableDto>(table);
        }

        public async Task<TableDto> CreateTableAsync(TableDto tableDto)
        {
            var repo = _repositoryFactory.GetRepository<Table>();
            var table = _mapper.Map<Table>(tableDto);

            await repo.AddAsync(table);
            await repo.SaveChangesAsync();

            return _mapper.Map<TableDto>(table);
        }

        public async Task UpdateTableAsync(int id, TableDto tableDto)
        {
            var repo = _repositoryFactory.GetRepository<Table>();
            var existingTable = await repo.GetByIdAsync(id);

            if (existingTable == null)
                throw new KeyNotFoundException("Table not found");

            // Osiguravamo da ID u URL-u i DTO-u matchaju
            tableDto.Id = id;
            _mapper.Map(tableDto, existingTable);

            repo.Update(existingTable);
            await repo.SaveChangesAsync();
        }

        public async Task DeleteTableAsync(int id)
        {
            var repo = _repositoryFactory.GetRepository<Table>();
            var table = await repo.GetByIdAsync(id);

            if (table == null)
                throw new KeyNotFoundException("Table not found");

            repo.Remove(table);
            await repo.SaveChangesAsync();
        }

        public async Task<bool> TableExistsAsync(int id)
        {
            var repo = _repositoryFactory.GetRepository<Table>();
            return await repo.GetByIdAsync(id) != null;
        }

        // Services/TableService.cs
        public async Task<(List<TableDto> Tables, int TotalCount)> GetTablesPagedAsync(TableQueryDto query)
        {
            query ??= new TableQueryDto();
            query.Page = Math.Max(1, query.Page);
            query.PageSize = new[] { 5, 10, 15 }.Contains(query.PageSize) ? query.PageSize : 5;

            var baseQuery = _context.Tables.AsQueryable();

            // Filtriranje po statusu zauzetosti
            if (query.IsOccupied.HasValue)
            {
                baseQuery = baseQuery.Where(t => t.IsOccupied == query.IsOccupied.Value);
            }

            // Paginacija
            var totalCount = await baseQuery.CountAsync();
            var tables = await baseQuery
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync();

            return (_mapper.Map<List<TableDto>>(tables), totalCount);
        }

        public List<string> GetAvailableSortColumns()
        {
            return new List<string> { "Id", "Name", "IsOccupied" };
        }
    }
}
