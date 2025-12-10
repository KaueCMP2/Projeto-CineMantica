using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoCinemanticaMVC.Models
{
    public class PerfilViewModel
    {
        public int id_usuario { get; set; }
        public string nome { get; set; }
        // public string? NomeUsuario {get; set;}
        public string Email { get; set; }
        // public int RegraId {get; set;}
        // public List<RegraPerfil> Regras {get; set;}

        public string? desc_perfil { get; set; }
        public DateOnly data_nascimento { get; set; }
        public string? NovaSenha { get; set; }
        public string? ConfirmarSenha { get; set; }
        public string? FotoBase64 { get; set; }

        public string? FotoFinal =>
                FotoBase64 != null
                ? $"data:image/*;base64,{FotoBase64}"
                : "/assets/img/img-perfil.png";

        public string? BannerBase64 { get; set; }
        public string? BannerFinal =>
                BannerBase64 != null
                ? $"data:image/*;base64,{BannerBase64}"
                : "/assets/img/banner-perfil.jpg";

        public int seguidores_count { get; set; }
        public int seguindo_count { get; set; }
    }
}