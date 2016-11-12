using MemeGeneratorProject;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Program
    {
        static void Main(string[] args)
        {

            var text = "- Novo programa da Adobe consegue \n imitar qualquer voz";
          
            var pirqui = new MemeGenerator();
            pirqui.PirquiGenerator(text, "C:\\gerador\\meme.jpg", "C:\\gerador\\resultado.jpg");

        }
    }
}
