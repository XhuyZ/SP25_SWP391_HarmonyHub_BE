using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Repository.Interfaces;
using Service.Interfaces;

namespace Service.Implementations
{
    public class GenericService<T, D> : IGenericService<T, D> where T : class
    {
        protected readonly IGenericRepository<T> _genericRepository;
        protected readonly IMapper _mapper;

        public GenericService(IMapper mapper, IGenericRepository<T> genericRepository)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<T> Add(D dto)
        {
            try
            {
                var entity = _mapper.Map<T>(dto);
                await _genericRepository.AddAsync(entity);
                return entity;
            }
            catch (DbUpdateException ex)
            {
                Exception innerEx = ex;
                while (innerEx != null)
                {
                    if (innerEx is MySqlException sqlEx)
                    {
                        // Check the SQL error number for specific constraint violations
                        if (sqlEx.Number == 547) // Foreign key constraint violation
                        {
                            throw new Exception("Foreign key not found");
                        }
                        else if (sqlEx.Number == 2627 || sqlEx.Number == 2601) // Unique constraint violation
                        {
                            throw new Exception("Id is duplicated");
                        }
                        else
                        {
                            // Handle other types of SQL exceptions
                            throw new Exception("An error occurred while saving changes. Please try again later.");
                        }
                    }
                    innerEx = innerEx.InnerException;
                }
                throw new Exception("An error occurred while saving changes. Please try again later.");
            }
        }

        public async Task<T> Delete(params int[] keys)
        {
            // Lặp qua từng khóa và xóa
            foreach (var key in keys)
            {
                var entity = await _genericRepository.GetByIdAsync(key); // Chuyển đổi từ int[] sang int
                if (entity == null) return null;
                await _genericRepository.DeleteAsync(entity); // Chuyển đổi từ int[] sang int
            }
            return null; // Hoặc trả về một giá trị phù hợp
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var entities = await _genericRepository.GetAllAsync();
            return entities;
        }

        public async Task<T> Get(params int[] keys)
        {
            // Lặp qua từng khóa và lấy
            foreach (var key in keys)
            {
                var entity = await _genericRepository.GetByIdAsync(key); // Chuyển đổi từ int[] sang int
                if (entity != null) return entity; // Trả về entity đầu tiên tìm thấy
            }
            return null; // Hoặc trả về một giá trị phù hợp
        }

        public async Task<T> Update(D dto)
        {
            var entity = _mapper.Map<T>(dto);
            await _genericRepository.UpdateAsync(entity);
            return entity;
        }
    }
}
