using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace waCubos
{
    public class DatosTbl
    {
        public Decimal Presupuesto { get; set; }

        public Decimal PorSobreVta { get; set; }

        public Decimal Sem1F { get; set; }

        public Decimal Sem1Nf { get; set; }

        public Decimal Sem2F { get; set; }

        public Decimal Sem2Nf { get; set; }

        public Decimal Sem3F { get; set; }

        public Decimal Sem3Nf { get; set; }

        public Decimal Sem4F { get; set; }

        public Decimal Sem4Nf { get; set; }

        public Decimal Semaforo { get; set; }

        public Decimal TotalFiscal { get; set; }

        public Decimal TotalNoFiscal { get; set; }

        public DatosTbl()
        {
            Presupuesto   = 0;
            PorSobreVta   = 0;
            Sem1F         = 0;
            Sem1Nf        = 0;
            Sem2F         = 0;
            Sem2Nf        = 0;
            Sem3F         = 0;
            Sem3Nf        = 0;
            Sem4F         = 0;
            Sem4Nf        = 0;
            Semaforo      = 0;
            TotalFiscal   = 0;
            TotalNoFiscal = 0;
        }

        public DatosTbl(object presupuesto, object porSobreVta, object sem1F, object sem1Nf, object sem2F, object sem2Nf, object sem3F, object sem3Nf, object sem4F, object sem4Nf, object semaforo, object totalFiscal, object totalNoFiscal)
        {
            Presupuesto   = Convert.ToDecimal(presupuesto);
            PorSobreVta   = Convert.ToDecimal(porSobreVta);
            Sem1F         = Convert.ToDecimal(sem1F);
            Sem1Nf        = Convert.ToDecimal(sem1Nf);
            Sem2F         = Convert.ToDecimal(sem2F);
            Sem2Nf        = Convert.ToDecimal(sem2Nf);
            Sem3F         = Convert.ToDecimal(sem3F);
            Sem3Nf        = Convert.ToDecimal(sem3Nf);
            Sem4F         = Convert.ToDecimal(sem4F);
            Sem4Nf        = Convert.ToDecimal(sem4Nf);
            Semaforo      = Convert.ToDecimal(semaforo);
            TotalFiscal   = Convert.ToDecimal(totalFiscal);
            TotalNoFiscal = Convert.ToDecimal(totalNoFiscal);
        }

        public void Suma(DatosTbl tbl)
        {
            Presupuesto   += tbl.Presupuesto;
            PorSobreVta   += tbl.PorSobreVta;
            Sem1F         += tbl.Sem1F;
            Sem1Nf        += tbl.Sem1Nf;
            Sem2F         += tbl.Sem2F;
            Sem2Nf        += tbl.Sem2Nf;
            Sem3F         += tbl.Sem3F;
            Sem3Nf        += tbl.Sem3Nf;
            Sem4F         += tbl.Sem4F;
            Sem4Nf        += tbl.Sem4Nf;
            Semaforo      += tbl.Semaforo;
            TotalFiscal   += tbl.TotalFiscal;
            TotalNoFiscal += tbl.TotalNoFiscal;
        }
    }
}