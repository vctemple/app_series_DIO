using System;

namespace App_series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            Console.WriteLine("Seja bem vindo ao App de séries!!");

            string opcaoUsuario = Menu();

            while(opcaoUsuario.ToUpper() != "7")
            {
                switch(opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    case "6":
                        Console.Clear();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();

                }

                opcaoUsuario = Menu();
            }

            Console.WriteLine("Até a próxima!");
            Console.ReadLine();
        }

        private static void ListarSeries()
        {
            Console.WriteLine("------------------");
            Console.WriteLine("Séries disponíveis");
            Console.WriteLine("------------------");
            Console.WriteLine();

            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada ainda!");
            }

            foreach (var serie in lista)
            {
                var excluido = serie.retornaExcluido();
                Console.WriteLine("#ID [{0}]: - {1} {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "[Não disponível] :(" : ""));
            }
        }

        private static void InserirSerie()
        {
            Console.WriteLine("------------------");
            Console.WriteLine("Adicione uma série");
            Console.WriteLine("------------------");
            Console.WriteLine();

            foreach( int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("#ID [{0}] - {1}", i, Enum.GetName(typeof(Genero), i));
            }
            Console.WriteLine();

            Console.Write("Insira o gênero da série dentre as opções disponíveis: ");
            int leiaGenero = int.Parse(Console.ReadLine());

            Console.Write("Insira o título da série: ");
            string leiaTitulo = Console.ReadLine();

            Console.Write("Insira o ano da série: ");
            int leiaAno = int.Parse(Console.ReadLine());

            Console.Write("Escreva uma sinopse: ");
            string leiaDescricao = Console.ReadLine();

            Series novaSerie = new Series(id: repositorio.ProximoId(),
                                          genero: (Genero)leiaGenero,
                                          titulo: leiaTitulo,
                                          ano: leiaAno,
                                          descrição: leiaDescricao);
            repositorio.Insere(novaSerie);
        }

        private static void AtualizarSerie()
        {
            Console.WriteLine("------------------");
            Console.WriteLine("Atualize uma série");
            Console.WriteLine("------------------");
            Console.WriteLine();
            
            Console.Write("Digite o ID da série para atualizá-la: ");
            int leiaID = int.Parse(Console.ReadLine());

            foreach( int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("#ID {0} - {1}", i, Enum.GetName(typeof(Genero), i));
            }

            Console.Write("Insira o novo gênero da série dentre as opções disponíveis: ");
            int leiaGenero = int.Parse(Console.ReadLine());

            Console.Write("Insira o novo título da série: ");
            string leiaTitulo = Console.ReadLine();

            Console.Write("Insira o novo ano da série: ");
            int leiaAno = int.Parse(Console.ReadLine());

            Console.Write("Escreva uma nova sinopse: ");
            string leiaDescricao = Console.ReadLine();

            Series atualizaSerie = new Series(id: leiaID,
                                          genero: (Genero)leiaGenero,
                                          titulo: leiaTitulo,
                                          ano: leiaAno,
                                          descrição: leiaDescricao);
            repositorio.Atualiza(leiaID, atualizaSerie);
        }

        private static void ExcluirSerie()
        {
            int teste = 0;

            Console.WriteLine("----------------");
            Console.WriteLine("Exclua uma série");
            Console.WriteLine("----------------");
            Console.WriteLine();

            while(teste == 0)
            {
                Console.Write("Digite o ID da série para excluí-la: ");
                int leiaID = int.Parse(Console.ReadLine());
                Console.WriteLine();
                Console.WriteLine("Deseja realmente excluir esta série?");
                Console.Write("Digite [1] p/ sim e [0] para não: ");
                teste = int.Parse(Console.ReadLine());
                Console.WriteLine();

                if(teste == 1)
                {
                    repositorio.Exclui(leiaID);
                }
                else
                {
                Console.WriteLine("Deseja sair sem excluir nenhuma série?");
                Console.Write("Digite [1] p/ sim e [0] para não: ");
                teste = int.Parse(Console.ReadLine());
                Console.WriteLine();
                }

            }      
           
        }

        private static void VisualizarSerie()
        {
            Console.WriteLine("-------------------");
            Console.WriteLine("Vizualize uma série");
            Console.WriteLine("-------------------");
            Console.WriteLine();
            
            Console.Write("Digite o ID da série para vizualizá-la: ");
            int leiaID = int.Parse(Console.ReadLine());

            var serie = repositorio.RetornaPorId(leiaID);

            Console.WriteLine(serie);
        }

        private static string Menu()
        {
            Console.WriteLine();
            Console.WriteLine("------------------------------");
            Console.WriteLine("Escolha uma das opções abaixo:");
            Console.WriteLine("------------------------------");
            Console.WriteLine();
            Console.WriteLine("[1] - Listar séries");
            Console.WriteLine("[2] - Inserir nova série");
            Console.WriteLine("[3] - Atualizar série");
            Console.WriteLine("[4] - Excluir série");
            Console.WriteLine("[5] - Visualizar série");
            Console.WriteLine("[6] - Limpar tela");
            Console.WriteLine("[7] - Sair");
            Console.WriteLine();
            Console.Write("Digite o número correspondente: ");
            
            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }
    }
}
