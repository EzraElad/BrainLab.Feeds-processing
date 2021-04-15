using AutoMapper;
using BrainLab.Feeds_processing.Models;
using BrainLab.Feeds_processing.Models.Facebook;
using BrainLab.Feeds_processing.Models.Twitter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainLab.Feeds_processing
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RequestModel, FacebookModel>();
            CreateMap<RequestModel, TwitterModel>();

            CreateMap<FacebookModel, RequestModel>();
            CreateMap<TwitterModel, RequestModel>();
        }
    }
}
