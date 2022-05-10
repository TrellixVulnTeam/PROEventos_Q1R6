using ProEventos.Application.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProEventosAPI.Application.Dtos
{
    public class EventoDto
    {
        public int ID { get; set; }
        public string Local { get; set; }
        public string DataEvento { get; set; }

        [Required(ErrorMessage ="O Campo {0} é obrigatório."),
        MinLength(3, ErrorMessage ="{0} Deve ter no minimo 4 caratceres"),
        MaxLength(50, ErrorMessage = "{0} Deve ter no máximo 50 caratceres")]
        public string Tema { get; set; }

        [Range(1, 120000, ErrorMessage ="{0} não pode ser menor que 1 e não pode ser maior que 120.000")]
        public int QuantidadePessoas { get; set; }

        [RegularExpression(@".*\.(gif|jpe?g|png|bmp)$", ErrorMessage ="Não é uma imagem valuda")]
        public string ImagemURL { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório.")]
        [Phone(ErrorMessage = "O Campo {0} esta com o numero invalido.")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O Campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage ="O Campo {0} precisa ser um E-mail valido.")]
        public string Email { get; set; }
        public int UserId { get; set; }
        public UserDto UserDto { get; set; }


        public IEnumerable<LoteDto> Lotes { get; set; }
        public IEnumerable<RedeSocialDto> RedesSociais { get; set; }

        public IEnumerable<PalestranteDto> PalestrantesEventos { get; set; }
    }
}
