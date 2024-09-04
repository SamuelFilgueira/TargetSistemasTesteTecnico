using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

public class Program
{
    public static void Main()
    {
        // Primeira funcionalidade: Cálculo de soma usando laço while
        int INDICE = 13;
        int SOMA = 0;
        int K = 0;

        while (K < INDICE)
        {
            K += 1;
            SOMA = SOMA + K;
        }

        Console.WriteLine($"Valor final de soma: {SOMA}");

        // Segunda funcionalidade: Verificação de número na sequência de Fibonacci
        Console.Write("\nInforme um número para verificar se pertence à sequência de Fibonacci: ");
        int numFibo = int.Parse(Console.ReadLine());

        Fibo fibo = new Fibo(numFibo);
        bool pertence = fibo.PertenceFibonacci();

        if (pertence)
        {
            Console.WriteLine($"O número {numFibo} pertence à sequência de Fibonacci.");
        }
        else
        {
            Console.WriteLine($"O número {numFibo} não pertence à sequência de Fibonacci.");
        }

        // Terceira funcionalidade: Cálculo de valores a partir do JSON de faturamento
        // Parte do código que processa o JSON
        string jsonFilePath = "dados.json";

        string jsonString = File.ReadAllText(jsonFilePath);


        List<FaturamentoDiario> faturamentos;

        try
        {
            faturamentos = JsonSerializer.Deserialize<List<FaturamentoDiario>>(jsonString);

            if (faturamentos == null)
            {
                Console.WriteLine("Erro: A desserialização resultou em null.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao desserializar o JSON: {ex.Message}");
            return;
        }


        // Filtra os dias com faturamento maior que 0
        var diasComFaturamento = faturamentos.Where(f => f.valor > 0).ToList();

        // Menor valor de faturamento
        double menorFaturamento = diasComFaturamento.Min(f => f.valor);

        // Maior valor de faturamento
        double maiorFaturamento = diasComFaturamento.Max(f => f.valor);

        // Média mensal de faturamento
        double mediaMensal = diasComFaturamento.Average(f => f.valor);

        // Número de dias com faturamento superior à média
        int diasAcimaDaMedia = diasComFaturamento.Count(f => f.valor > mediaMensal);

        Console.WriteLine($"\nMenor faturamento diário: {menorFaturamento}");
        Console.WriteLine($"Maior faturamento diário: {maiorFaturamento}");
        Console.WriteLine($"Número de dias com faturamento superior à média: {diasAcimaDaMedia}");


        // Quarta funcionalidade: Cálculo do percentual de representação por estado
        string[] estados = { "SP", "RJ", "MG", "ES", "Outros" };
        double[] faturamentoEstados = { 67836.43, 36678.66, 29229.88, 27165.48, 19849.53 };

        double totalFaturamento = faturamentoEstados.Sum();

        Console.WriteLine("\nPercentual de representação por estado:");

        for (int i = 0; i < estados.Length; i++)
        {
            double percentual = (faturamentoEstados[i] / totalFaturamento) * 100;
            Console.WriteLine($"{estados[i]}: {percentual:F2}%");
        }



        // Quinta funcionalidade: Inversão de caracteres de uma string
        Console.Write("\nInforme uma string para inverter: ");
        string inputString = Console.ReadLine();

        string invertedString = InverterString(inputString);
        Console.WriteLine($"String invertida: {invertedString}");
    }

       // Método para inverter 
    public static string InverterString(string str)
    {
        StringBuilder sb = new StringBuilder();

        for (int i = str.Length - 1; i >= 0; i--)
        {
            sb.Append(str[i]);
        }

        return sb.ToString();
    }

}



// Classe para verificar se um número pertence à sequência de Fibonacci
public class Fibo
{
    private readonly int _numeroFib;

    public Fibo(int numeroFib)
    {
        _numeroFib = numeroFib;
    }

    public bool PertenceFibonacci()
    {
        int a = 0;
        int b = 1;

        if (_numeroFib == a || _numeroFib == b)
        {
            return true;
        }

        int c = a + b;

        while (c <= _numeroFib)
        {
            if (c == _numeroFib)
            {
                return true;
            }

            a = b;
            b = c;
            c = a + b;
        }

        return false;
    }
}


// Classe que representa o JSON de faturamento diário
public class FaturamentoDiario
{
    public int dia { get; set; }
    public double valor { get; set; }
}
