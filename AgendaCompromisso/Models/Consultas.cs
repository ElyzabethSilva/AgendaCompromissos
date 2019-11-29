using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgendaCompromissos.Models {
    public class Consultas {
        [Key]
        public int ConsultaId { get; set; }

        [Required]
        [Column(TypeName = "varchar(250)")]
        public string NomeDoPaciente { get; set; }

        [Required]
        [Column(TypeName = "varchar(10)")]
        public DateTime DataNascimentoPaciente { get; set; } // Data de Nascimento: "dd/mm/aaaa"

        [Required]
        [Column(TypeName = "varchar(16)")]
        public DateTime DataHoraInicial { get; set; } // Data Inicial da Consulta: "dd/mm/aaaa hh:mm"

        [Required]
        [Column(TypeName = "varchar(16)")]
        public DateTime DataHoraFinal { get; set; } // Data Final da Consulta: "dd/mm/aaaa hh:mm"

        [Column(TypeName = "varchar(800)")]
        public string Observacoes { get; set; }

        public Consultas() {
        }

        public Consultas(int id, string paciente, DateTime dataNascimento, DateTime dataHoraInicial, DateTime dataHoraFinal, string obs) {
            ConsultaId = id;
            NomeDoPaciente = paciente;
            DataNascimentoPaciente = dataNascimento;
            DataHoraInicial = dataHoraInicial;
            DataHoraFinal = dataHoraFinal;
            Observacoes = obs;
        }
    }
}