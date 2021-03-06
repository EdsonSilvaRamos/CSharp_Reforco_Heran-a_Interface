﻿using Parte04_ByteBank;
using System;

namespace ByteBank
{
    /// <summary>
    /// Define uma Conta Corrente do banco ByteBank.
    /// </summary>
    public class ContaCorrente
    {
        public Cliente Titular { get; set; }

        public static double TaxaOperacao { get; private set; }

        public static int TotalDeContasCriadas { get; private set; }

        public static int ContadorDeSaquesNaoPermitidos { get; private set; }

        public static int ContadorDeTransferenciasNaoPermitidas { get; private set; }

        public int Agencia { get; }

        public int Numero { get; }

        private double _saldo = 100;

        public double Saldo
        {
            get
            {
                return _saldo;
            }
            set
            {
                if (value < 0)
                {
                    return;
                }

                _saldo = value;
            }
        }

        /// <summary>
        /// Cria uma instância de ContaCorrente com os argumentos utilizados;
        /// </summary>
        /// <param name="agencia">Representa o valor da propriedade <see cref="Agencia"/> e deve possuir um valor maior que zero.</param>
        /// <param name="numero">Representa o valor da propriedade <see cref="Numero"/> e deve possuir um valor maior que zero.</param>
        public ContaCorrente(int agencia, int numero)
        {
            if (agencia <= 0)
            {
                throw new ArgumentException("A agência não pode ser menor ou igual a zero.", nameof(agencia));
            }

            if (numero <= 0)
            {
                throw new ArgumentException("o número não pode ser menor ou igual a zero.", nameof(numero));
            }

            Agencia = agencia;
            Numero = numero;

            TotalDeContasCriadas++;
            TaxaOperacao = 30 / TotalDeContasCriadas;
        }

        /// <summary>
        /// Realiza o saque e atualiza o valor da propriedade <see cref="Saldo"/>
        /// </summary>
        /// <exception cref="ArgumentException">Exceção lançada quando um valor negativo é utilizado no argumento <paramref name="valor"/>. </exception>
        /// <exception cref="SaldoInsuficienteException">Execção lançada quando o valor de <paramref name="valor"/>é maior que o valor da propriedade <paramref name="valor"/> </exception>
        /// <param name="valor">Representa o valor do saque, deve ser maior que 0 e menor que <see cref="Saldo"/></param>
        public void Sacar(double valor)
        {
            if (valor < 0)
            {
                throw new ArgumentException("Valor inválido para saque, argumento: " + nameof(valor));
            }
            
            if (_saldo < valor)
            {
                ContadorDeSaquesNaoPermitidos++;
                throw new SaldoInsuficienteException(Saldo, valor);
            }

            _saldo -= valor;            
        }

        public void Depositar(double valor)
        {
            _saldo += valor;
        }


        public void Transferir(double valor, ContaCorrente contaDestino)
        {
            if (valor < 0)
            {
                throw new ArgumentException("Valor inválido para transferência, argumento: " + nameof(valor));
            }


            try
            {
                Sacar(valor);
            }
            catch (OperacaoFincanceiraException ex)
            {
                ContadorDeTransferenciasNaoPermitidas++;
                throw new OperacaoFincanceiraException("Operação não realizada!", ex);
            }
            
            
            
            contaDestino.Depositar(valor);
            
        }
    }
}
