using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace C007.使用執行緒做到非同步處理作業
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"建立兩個執行緒物件");
            //Thread thread1 = new Thread(() =>
            Task task1 = Task.Run(() =>
            {
                Console.WriteLine($"執行緒1 的 ID={Thread.CurrentThread.ManagedThreadId} ( 輸出 X )");
                Thread.Sleep(900);
                for (int i = 0; i < 20; i++)
                {
                    Thread.Sleep(100); Console.Write("X");
                }
                //Console.WriteLine();
            });
            //{
            //    IsBackground = true //加入後此程式就會立刻關掉
            //};
            //Thread thread2 = new Thread(() =>
            Task task2 = Task.Run(() =>
            {
                Console.WriteLine($"執行緒2 的 ID={Thread.CurrentThread.ManagedThreadId} ( 輸出 - )");
                Thread.Sleep(900);
                for (int i = 0; i < 20; i++)
                {
                    Thread.Sleep(150); Console.Write("-");
                }
                //Console.WriteLine();
            })
            //{
            //    IsBackground = true //加入後此程式就會立刻關掉
            //};
            ;
            Console.WriteLine($"啟動執行兩個執行緒");

            //thread1.Start();    //這裡開始跑
            //thread2.Start();

            //thread1.Join(); // *必須等待 執行緒1 執行完畢
            //thread2.Join(); // 等待 執行緒2 執行完畢

            //task練習 task就是背景執行序
            //task1.Wait();
            //task2.Wait();

            //前景執行序 - main thread 一結束就會關閉, 如果有背景thread 就會被kill
            //new thread 就是新的前景執行序, 所以部會被關掉
            //背景執行序 : task 就是背景執行序 如果不join就會被關掉
            //一般常用task 就不會去寫thread, thread是基本功

            Console.WriteLine();    //會不會等執行序跑完才結束?

            //Console.WriteLine("Press any key to continue...");
            //Console.ReadKey();
        }
    }
}
