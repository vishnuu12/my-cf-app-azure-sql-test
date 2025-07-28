using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApplication.BLL.Interface;
using TestApplication.DAL.Interface;
using TestApplication.Models.Models;

namespace TestApplication.BLL.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _repository;

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Student> CreateAsync(Student student)
        {
            return await _repository.AddAsync(student);
        }

        public async Task<bool> UpdateAsync(Student student)
        {
            return await _repository.UpdateAsync(student);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
