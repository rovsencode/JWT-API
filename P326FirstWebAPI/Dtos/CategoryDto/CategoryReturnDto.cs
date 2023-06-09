﻿namespace P326FirstWebAPI.Dtos.CategoryDto
{
    public class CategoryReturnDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int ProductsCount { get; set; }
        public int CreateAge { get; set; }
    }
}
