using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
  public  class ObjectMappingProfile : AutoMapper.Profile
    {
        public ObjectMappingProfile()
        {
            CreateMap<ClassesDTO,classes>();
            CreateMap<classes,ClassesDTO>();
            CreateMap<studentDTO, studens>();
            CreateMap<studens, studentDTO>();
            CreateMap<instationDTO, instation>();
            CreateMap<instation, instationDTO>();
            CreateMap<parentsDTO, parents>();
            CreateMap<parents, parentsDTO>();
            CreateMap<requestDTO, request>();
            CreateMap<request, requestDTO>();
            CreateMap<schedulingDTO, scheduling>();
            CreateMap<scheduling, schedulingDTO>();
            CreateMap<teacherDTO, teachers>();
            CreateMap<teachers, teacherDTO>();
            CreateMap<timesDTO, times>();
            CreateMap<times, timesDTO>();
        }
    }
}
