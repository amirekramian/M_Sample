using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Sample.Common.ViewModels;
using Sample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Common.Profiles;

public class PersonProfile : Profile
{
        public PersonProfile()
        {
                CreateMap<Person, PersonViewModel>()
                        .ForMember(destination => destination.FullName,
                                option =>
                                        option.MapFrom(source => source.FullName))

                        .ForMember(destination => destination.PhoneNumber,
                                option =>
                                        option.MapFrom(source => source.Phone!.Content))
                        .ReverseMap();
        }
}
