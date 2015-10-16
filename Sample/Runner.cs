namespace Sample
{
    using System;
    using System.IO;
    using System.Linq;
    using FluentValidation;
    using Sample.Core;
    using Sample.Core.Commands;

    public class Runner
    {
        readonly IShim _shim;
        readonly TextWriter _output;

        public Runner(IShim shim, TextWriter output)
        {
            _shim = shim;
            _output = output;
        }

        public void Run()
        {
            _output.WriteLine("Press 's' to send a valid message");
            _output.WriteLine("Press 'e' to send a failed message");
            _output.WriteLine("Press any key to exit");

            while (true)
            {
                _output.WriteLine();
                _output.WriteLine();

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.S:
                        SendSmallMessage();
                        break;
                    case ConsoleKey.E:
                        SendLargeMessage();
                        break;
                    default:
                        return;
                }
            }
        }

        void SendLargeMessage()
        {
            var product = new Product
            {
                ProductId = "XJ128",
                // product name too long
                ProductName = "Really long product name",
                // invalid list price
                ListPrice = 15,
                Image = new byte[1024 * 1024 * 2]
            };

            Try(() => _shim.Execute<Product ,CreateProductCommand>(product, DoWork));
        }

        void SendSmallMessage()
        {
            var product = new Product
            {
                ProductId = "XJ128",
                ProductName = "Milk",
                ListPrice = 4,
                Image = new byte[1024 * 1024 * 2]
            };
            Try(() => _shim.Execute<Product, CreateProductCommand>(product, DoWork));
        }

        private void DoWork(Product product)
        {
            //done other processing here
        }

        void Try(Action action)
        {
            try
            {
                action();           
            }
            catch (ValidationException exception)
            {
                exception
                    .Errors
                    .Select(error => string.Format("{1} ({0}: {2})", error.PropertyName, error.ErrorMessage, error.AttemptedValue))
                    .ToList()
                    .ForEach(error => _output.WriteLine(error));
            }
            catch (Exception exception)
            {
                _output.WriteLine();
                _output.WriteLine();
                _output.WriteLine(exception);
                //so the console keeps on running   
            }    
        }
    }
}