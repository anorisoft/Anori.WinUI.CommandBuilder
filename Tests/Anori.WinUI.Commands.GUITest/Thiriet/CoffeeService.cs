// -----------------------------------------------------------------------
// <copyright file="CoffeeService.cs" company="AnoriSoft">
// Copyright (c) AnoriSoft. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Anori.WinUI.Commands.GUITest.Thiriet
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class CoffeeService : ViewModelBase
    {
        public async Task PrepareCoffeeAsync2()
        {
            // Asynchronously prepare an awesome coffee
            var start = DateTime.Now;
            var end = start.AddSeconds(2);
            await Task.Run(
                () =>
                    {
                        while (DateTime.Now <= end)
                        {
                            //await Task.Delay(1);
                        }
                    });
        }

        public async Task PrepareCoffeeAsync3()
        {
            await Task.Run(() => { Thread.Sleep(TimeSpan.FromSeconds(2)); });
        }

        public async Task PrepareCoffeeAsync1()
        {
            // Asynchronously prepare an awesome coffee
            await Task.Delay(2000);
        }

        public async Task PrepareCoffeeSync()
        {
            Thread.Sleep(TimeSpan.FromSeconds(2));
        }

        public async Task PrepareCoffeeAsync4(CancellationToken token)
        {
            // Asynchronously prepare an awesome coffee
            var start = DateTime.Now;
            var end = start.AddSeconds(2);
            //await Task.Run(
            //    LoadAction(token, end),
            //    token);

            //start = DateTime.Now;
            //end = start.AddSeconds(2);
            await new TaskFactory(TaskScheduler.Default).StartNew(t => LoadAction((CancellationToken)t, end), token, TaskCreationOptions.DenyChildAttach);
        }

        private static Action LoadAction(CancellationToken token, DateTime end)
        {
            return () =>
                {
                    while (DateTime.Now <= end && token.IsCancellationRequested)
                    {
                        Thread.Sleep(TimeSpan.FromMilliseconds(1));
                    }
                };
        }

        public async Task PrepareCoffeeAsync()
        {
            await this.PrepareCoffeeAsync3();
            //await this.PrepareCoffeeSync();
        }
    }
}