using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace D008.利用TAP工作建立大量並行工作練習
{
    class Program
    {
        //io bound 才會這麼快
        static async Task Main(string[] args)
        {
            string host = "https://lobworkshop.azurewebsites.net";
            string path = "/api/RemoteSource/Source3";
            string url = $"{host}{path}";

            // HttpClient client = new HttpClient();   //TAP API  查是不適thread safe?
            //for (int i = 0; i < 10; i++)
            //{
            //    var index = string.Format("{0:D2}", (i + 1));


            //    //非同步但效能很差 前四筆快 後面都慢
            //    //task 背後pool是固定 硬體限制cpu 1 會有四個thread,更多就會content thread 
            //    //但超過四個 threadpool 一秒加一條thread , 導致block thread 不能用
            // 所以要有效利用四個thread

            //    //error 寫法
            //    //1.開多個task效能更慢,因為光開thread就要等很久
            //    //2.把async 寫在main 主程式
            //    Task.Run(() =>
            //    {
            //        var tid = String.Format("{0:D2}", Thread.CurrentThread.ManagedThreadId);

            //        Console.WriteLine($"{index}-1 測試 (TID: {tid}) >>>> {DateTime.Now}");
            //        //var result = client.GetStringAsync(url).Result; //IO bound 靠外部給我東西
            //        var result = client.GetStringAsync(url).Result; //IO bound 靠外部給我東西
            //        Console.WriteLine($"{index}-1 測試 (TID: {tid}) ==== {result}");
            //        Console.WriteLine($"{index}-1 測試 (TID: {tid}) <<<< {DateTime.Now}");

            //        Console.WriteLine($"{index}-2 測試 (TID: {tid}) >>>> {DateTime.Now}");
            //        //result = client.GetStringAsync(url).Result; //會blocking thread
            //        result = await client.GetStringAsync(url); //會blocking thread
            //        Console.WriteLine($"{index}-2 測試 (TID: {tid}) ==== {result}");
            //        Console.WriteLine($"{index}-2 測試 (TID: {tid}) <<<< {DateTime.Now}");
            //    });
            //}

            //// 這整個都是io bound 一條thread 都算
            //// 多使用作業系統的thread 去想辦法有效應用,不用application thread
            //for (int i = 0; i < 10; i++)
            //{
            //    var index = string.Format("{0:D2}", (i + 1));

            //    //修改
            //    Task.Run(async () =>
            //    {
            //        HttpClient client = new HttpClient();   //如果在外面就是共用
            //        var tid = String.Format("{0:D2}", Thread.CurrentThread.ManagedThreadId);

            //        Console.WriteLine($"{index}-1 測試 (TID: {tid}) >>>> {DateTime.Now}");
            //        var result = await client.GetStringAsync(url); //IO bound 靠外部給我東西
            //        //用await 的話下面兩條,有可能是啟用其他thread來執行
            //        //await 可以用non blocking等待, thread不會被占用
            //        Console.WriteLine($"{index}-1 測試 (TID: {tid}) ==== {result}");
            //        Console.WriteLine($"{index}-1 測試 (TID: {tid}) <<<< {DateTime.Now}");

            //    });

            //    Task.Run(async () =>
            //    {
            //        HttpClient client = new HttpClient();   //如果在外面就是共用
            //        var tid = String.Format("{0:D2}", Thread.CurrentThread.ManagedThreadId);

            //        Console.WriteLine($"{index}-2 測試 (TID: {tid}) >>>> {DateTime.Now}");
            //        var result = await client.GetStringAsync(url); //會blocking thread
            //        Console.WriteLine($"{index}-2 測試 (TID: {tid}) ==== {result}");
            //        Console.WriteLine($"{index}-2 測試 (TID: {tid}) <<<< {DateTime.Now}");
            //    });
            //}

            //// 這整個都是io bound 一條thread 都算
            //// 多使用作業系統的thread 去想辦法有效應用,不用application thread
            //for (int i = 0; i < 10; i++)
            //{
            //    var index = string.Format("{0:D2}", (i + 1));

            //    //修改
            //    Task.Run(async () =>
            //    {
            //        HttpClient client = new HttpClient();   //如果在外面就是共用
            //        var tid = String.Format("{0:D2}", Thread.CurrentThread.ManagedThreadId);
            //        var task1 = client.GetStreamAsync(url); //使用task
            //        var task2 = client.GetStreamAsync(url); //使用task

            //        //Task.WaitAll(); //回傳void, 等是用  blocking thread等
            //        await Task.WhenAll(task1, task2);   //用await聰明地等, 所以下面的.Result 已經都等完了

            //        Console.WriteLine($"{index}-1 測試 (TID: {tid}) >>>> {DateTime.Now}");
            //        //用await 的話下面兩條,有可能是啟用其他thread來執行
            //        //await 可以用non blocking等待, thread不會被占用
            //        Console.WriteLine($"{index}-1 測試 (TID: {tid}) ==== {task1.Result}");
            //        Console.WriteLine($"{index}-1 測試 (TID: {tid}) <<<< {DateTime.Now}");

            //    });

            //}

            var tasks = new Task[10];
            for (int i = 0; i < 10; i++)
            {
                var index = string.Format("{0:D2}", (i + 1));

                //修改
                tasks[i] = Task.Run(async () =>
                {
                    HttpClient client = new HttpClient();   //如果在外面就是共用
                    var tid = String.Format("{0:D2}", Thread.CurrentThread.ManagedThreadId);
                    //var task1 = await client.GetStreamAsync(url); //使用task
                    //var task2 = client.GetStreamAsync(url); //使用task

                    //Task.WaitAll(); //回傳void, 等是用  blocking thread等
                    //await Task.WhenAll(task1, task2);   //用await聰明地等, 所以下面的.Result 已經都等完了

                    Console.WriteLine($"{index}-1 測試 (TID: {tid}) >>>> {DateTime.Now}");
                    //用await 的話下面兩條,有可能是啟用其他thread來執行
                    //await 可以用non blocking等待, thread不會被占用
                    var result = await client.GetStringAsync(url); //會blocking thread
                    Console.WriteLine($"{index}-2 測試 (TID: {tid}) ==== {result}");
                    Console.WriteLine($"{index}-1 測試 (TID: {tid}) <<<< {DateTime.Now}");

                });

            }

            var task = Task.WhenAll(tasks);
            while (!task.IsCompleted)
            {
                Console.WriteLine("*");
                await Task.Delay(1000);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();  //為了等thread
        }
    }
}
