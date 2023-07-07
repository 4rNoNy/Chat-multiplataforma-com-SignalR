using AutoMapper;
using ReactMXHApi6.Core.Entities;
using ReactMXHApi6.Dtos;

namespace ReactMXHApi6.Helper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles(IConfiguration config)
        {
            // Mapeamento entre a entidade LastMessageChat e o DTO LastMessageChatDto
            CreateMap<LastMessageChat, LastMessageChatDto>()
                .ForMember(dest => dest.SenderDisplayName, opt => opt.MapFrom(src => src.Sender.DisplayName))
                .ForMember(dest => dest.SenderImgUrl, opt => opt.MapFrom(src => src.Sender.ImageUrl != null ? $"{config["BaseUrl"]}/{src.Sender.ImageUrl}" : null));

            // Mapeamento entre a entidade AppUser e o DTO MemberDto
            CreateMap<AppUser, MemberDto>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl != null ? $"{config["BaseUrl"]}/{src.ImageUrl}" : null));

            // Mapeamento entre a entidade Message e o DTO MessageDto
            CreateMap<Message, MessageDto>()
                .ForMember(dest => dest.SenderDisplayName, opt => opt.MapFrom(src => src.Sender.DisplayName))
                .ForMember(dest => dest.RecipientDisplayName, opt => opt.MapFrom(src => src.Recipient.DisplayName));

            // Mapeamento entre a entidade Post e o DTO PostDto
            CreateMap<Post, PostDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.User.DisplayName))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.User.ImageUrl != null ? $"{config["BaseUrl"]}/{src.User.ImageUrl}" : null));

            // Mapeamento entre a entidade Comment e o DTO CommentDto
            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.User.DisplayName))
                .ForMember(dest => dest.UserImageUrl, opt => opt.MapFrom(src => src.User.ImageUrl != null ? $"{config["BaseUrl"]}/{src.User.ImageUrl}" : null));

            // Mapeamento entre a entidade ImageOfPost e o DTO ImageOfPostDto
            CreateMap<ImageOfPost, ImageOfPostDto>()
                .ForMember(dest => dest.Path, opt => opt.MapFrom(src => $"{config["BaseUrl"]}/{src.Path}"));
        }
    }
}
