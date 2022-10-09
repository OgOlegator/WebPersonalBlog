using AutoMapper;
using Blog.Services.PostsAPI.Models;
using Blog.Services.PostsAPI.Models.Dto;

namespace Blog.Services.PostsAPI
{
    public class MappingConfig
    {

        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<PostDto, Post>();
                config.CreateMap<Post, PostDto>();
            });

            return mappingConfig;
        }
    }
}
