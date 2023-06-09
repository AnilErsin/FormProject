﻿using Case.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.BLL.Services
{
    public interface IFieldService
    {
        IEnumerable<Field> GetAllForm();

        IEnumerable<Field> GetAllFields(int formID);
        void CreateField(Field field);
        void RemoveField(Field field);
        void UpdateField(Field field);
        Field FieldGetById(int id);

        void InsertFields(List<Field> fields);
    }
}
