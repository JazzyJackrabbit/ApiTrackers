using ApiTrackers.Objects;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTrackers.DTO_ApiParameters.Module
{
    public class ModulesConvertDTO
    {
        public List<IFormFile> files { get; set; }
        public int idUser { get; set; }

        public List<Module> toModules(User _user)
        {
            List<Module> modules = new List<Module>();
            foreach (IFormFile file in files)
                modules.Add(new Module(file, _user));
            
            return modules;
        }
    }
}
