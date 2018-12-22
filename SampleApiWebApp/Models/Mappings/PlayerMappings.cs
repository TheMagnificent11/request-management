﻿using AutoMapper;

namespace SampleApiWebApp.Models.Mappings
{
    public sealed class PlayerMappings : Profile
    {
        public PlayerMappings()
        {
            CreateMap<Domain.Player, Player>();
            CreateMap<Requests.PostPlayerRequest, Domain.Player>();
        }
    }
}
