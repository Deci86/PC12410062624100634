using System;
using System.Collections.Generic;
using System.Text;

namespace PC12410062624100634.CORE.Core.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }

    public class CategoryListDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public class CategoryCreateDTO
    {
        public string Description { get; set; }
    }

    public class CategoryUpdateDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public class CategoryDeleteDTO
    {
        public int Id { get; set; }
    }

}
