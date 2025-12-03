using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using mvc.Models;
using mvc.Data;

namespace mvc.Controllers
{
    public class CadastroController : Controller
    {
        private readonly AppDbContext _contexto;

        public CadastroController(AppDbContext contexto)
        {
            _contexto = contexto;
        }
        

    }
}
