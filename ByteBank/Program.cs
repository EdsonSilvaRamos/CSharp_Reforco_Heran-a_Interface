﻿using ByteBank.Funcionarios;
using System;

namespace ByteBank
{
    class Program
    {
        static void Main(string[] args)
        {
            var carlos = new Funcionario(1);

            carlos.Nome = "Carlos";
            carlos.CPF = "123.456.789-50";
            carlos.Salario = 2000;

            Console.WriteLine(carlos.Nome);
            Console.WriteLine(carlos.GetBonificacao());

            Console.ReadLine();
        }
    }
}
