﻿using ApiMonkeyMoney.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonkeyMoneyApp.Data;
using MonkeyMoneyApp.Repository.Interface;
using System.Diagnostics.CodeAnalysis;

namespace MonkeyMoneyApp.Repository
{
    public class MetaRepository : IMetaRepository
    {
        private readonly ApplicationDbContext _context;

        public MetaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Meta> Delete(int id)
        {
            var meta = await _context.Metas.FindAsync(id);
            if (meta == null)
            {
                return null;
            }
            _context.Remove(meta);
            await _context.SaveChangesAsync();
            return meta;
        }

        public Task<List<Meta>> GetMetaById(int id)
        {
            return _context.Metas.FromSqlInterpolated($"SELECT * FROM Metas WHERE Id = {id}").ToListAsync();
        }

        public Task<List<Meta>> GetMetas()
        {
            return _context.Metas.FromSqlRaw("SELECT * FROM Metas").ToListAsync();
        }

        public async Task<Meta> Post([FromBody] Meta meta)
        {
                await _context.Metas.AddAsync(meta);
                await _context.SaveChangesAsync();
                return meta;
        }

        public async Task<Meta> Put(int id, [FromBody] Meta meta)
        {
            var existeMeta = await _context.Metas.FindAsync(id);
            if (existeMeta == null)
            {
                return null;
            }
            _context.Entry(existeMeta).CurrentValues.SetValues(meta);
            await _context.SaveChangesAsync();

            var metaAtualizada = await _context.Metas.FindAsync(id);
            return metaAtualizada;
        }
    }
}
