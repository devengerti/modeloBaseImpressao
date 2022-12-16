using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GenCode128;
using System.Data.Common;
using LATROMI.Extensions;

namespace teste_etiqueta
{
    public partial class Form1 : Form
    {     
        public Form1()
        {
            InitializeComponent();
            listaImpressoras();
        }

            private void btnImprimir_Click(object sender, EventArgs e)
        {
            using (var printDocument = new System.Drawing.Printing.PrintDocument())
            {
                printDocument.PrintPage += printDocument_PrintPage;
                printDocument.PrinterSettings.PrinterName = comboBoxImpressora.SelectedItem.ToString();
                printDocument.Print();
            }
        }

        //metodos
        private void listaImpressoras()
        {
            comboBoxImpressora.Items.Clear();
            foreach (var printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                comboBoxImpressora.Items.Add(printer);
            }
        }

        void printDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            //Objeto Etiqueta
            Etiqueta etiqueta = new Etiqueta();
            int altura = 1; //para altura da etiqueta
            Point pontoInicial = new Point(35, 13); //ponto no plano cartesiano onde inicia o titulo
            Point pontoBloco1 = new Point(11, 40); //produto
            Point pontoBloco2 = new Point(11, 65); //fornecedor
            Point pontoBloco3 = new Point(11, 90); //documento
            Point pontoBloco4 = new Point(77, 90); //data e hora
            Point pontoBloco5 = new Point(144, 90); //operador
            Point pontoBloco6 = new Point(11, 130); //tara
            Point pontoBloco7 = new Point(77, 130); //peso bruto
            Point pontoBloco8 = new Point(144, 130); //peso liquido
            Point pontoBloco9 = new Point(11, 170); //campo obs
            Point pontoImagem = new Point(35, 200);  //ponto inicial da imagem        

            var printDocument = sender as System.Drawing.Printing.PrintDocument;
            Pen blackPen = new Pen(Color.Black, 1); //configura espessura da borda e cor
            var fonteCorpoTexto = new Font("Arial", 6, FontStyle.Bold); //fonte para o corpo do texto com negrito setado
            var brush = new SolidBrush(Color.Black);
            var font = new Font("Arial", 8, FontStyle.Bold);
            //Image newImage = Image.FromFile("logo.jpg"); //carregar uma imagem
            etiqueta.codigoBarrasCode = "089765432"; //valor setado para teste apenas
            Image codigoBarras = Code128Rendering.MakeBarcodeImage(etiqueta.codigoBarrasCode, altura, false); //imagem gerada do codigo de barras a partir da variavel codigoBarras

            Rectangle rect = new Rectangle(7, 7, 205, 220); //retangulo principal ou quadrado
            Rectangle rectMinor1 = new Rectangle(10, 10, 198, 25); //retangulo do titulo
            Rectangle rectMinor2 = new Rectangle(10, 39, 198, 25);
            Rectangle rectMinor3 = new Rectangle(10, 59, 198, 25);
            Rectangle rectMinor4 = new Rectangle(10, 89, 63, 35); 
            Rectangle rectMinor5 = new Rectangle(76, 89, 63, 35);
            Rectangle rectMinor6 = new Rectangle(143, 89, 64, 35); //aqui
            Rectangle rectMinor7 = new Rectangle(10, 129, 63, 35);
            Rectangle rectMinor8 = new Rectangle(76, 129, 63, 35); 
            Rectangle rectMinor9 = new Rectangle(143, 129, 64, 35); 
            Rectangle rectMinor10 = new Rectangle(10, 169, 198, 25); //esse
            Rectangle rectMinor11 = new Rectangle(10, 199, 198, 25);
           
            if (printDocument != null)
            {
                //desenha o layout                
                e.Graphics.DrawRectangle(blackPen, rect);
                e.Graphics.DrawRectangle(blackPen, rectMinor1);
                e.Graphics.DrawRectangle(blackPen, rectMinor2);
                e.Graphics.DrawRectangle(blackPen, rectMinor3);
                e.Graphics.DrawRectangle(blackPen, rectMinor4);
                e.Graphics.DrawRectangle(blackPen, rectMinor5);
                e.Graphics.DrawRectangle(blackPen, rectMinor6);
                e.Graphics.DrawRectangle(blackPen, rectMinor7);
                e.Graphics.DrawRectangle(blackPen, rectMinor8);
                e.Graphics.DrawRectangle(blackPen, rectMinor9);
                e.Graphics.DrawRectangle(blackPen, rectMinor10);
                e.Graphics.DrawRectangle(blackPen, rectMinor11);

                e.Graphics.DrawString(
                etiqueta.titulo,
                font,
                brush,
                pontoInicial);

                e.Graphics.DrawString(
                "Produto: " + etiqueta.produto,
                fonteCorpoTexto,
                brush,
                pontoBloco1);

                e.Graphics.DrawString(
                "Fornecedor: " + "\n" + etiqueta.forncedor,
                fonteCorpoTexto,
                brush,
                pontoBloco2);

                e.Graphics.DrawString(
                "Documento: " + "\n" + etiqueta.documento,
                fonteCorpoTexto,
                brush,
                pontoBloco3);

                e.Graphics.DrawString(
               "Data / Hora: " + "\n" + DateTime.Now.ToShortDateString() + "\n" + DateTime.Now.ToShortTimeString(),
               fonteCorpoTexto,
               brush,
               pontoBloco4);

                e.Graphics.DrawString(
               "Operador: " + "\n" + etiqueta.operador,
               fonteCorpoTexto,
               brush,
               pontoBloco5);

                e.Graphics.DrawString(
               "Tara: " + "\n" + etiqueta.tara + " KG",
               fonteCorpoTexto,
               brush,
               pontoBloco6);

                e.Graphics.DrawString(
               "Peso Bruto: " + "\n" + etiqueta.pesoBruto + " KG",
               fonteCorpoTexto,
               brush,
               pontoBloco7);

                e.Graphics.DrawString(
               "Peso Liquido: " + "\n" + etiqueta.pesoLiquido + " KG",
               fonteCorpoTexto,
               brush,
               pontoBloco8);

                e.Graphics.DrawString(
               "Obs: " + "\n" + etiqueta.campoObs,
               fonteCorpoTexto,
               brush,
               pontoBloco9);

               e.Graphics.DrawImage(codigoBarras, pontoImagem); //imprime a imagem
            }

        }

    }
}
    

