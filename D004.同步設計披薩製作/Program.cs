﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace D004.同步設計披薩製作
{
    class Program
    {
        static void Main(string[] args)
        {
            var watch = Stopwatch.StartNew();
            Console.WriteLine("開始進行製作披薩...");
            
            var t1 = Task.Run(() => 烤箱預熱());
            var t2 = Task.Run(() => {
                    製作麵團();
                    發麵();
                });
            var t3 = Task.Run(() => 準備醬料());
            var t4 = Task.Run(() => 準備配料());
            Task.WaitAll(t1, t2, t3, t4);

            var t5 = Task.Run(() => 製作披薩餅皮與塗抹醬料和放置配料());
            var t6 = Task.Run(() => 準備餐具與飲料());
            var t7 = Task.Run(() => 烤製披薩());
            Task.WaitAll(t5,t6,t7);

            var t8 = Task.Run(() => 披薩完成_開始食用());
            Task.WaitAll(t8);

            watch.Stop();

            Console.WriteLine($"同步設計披薩製作共花費:{watch.Elapsed.TotalMilliseconds} 秒");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        
        }
        static void 烤箱預熱()
        {
            Console.WriteLine($"烤箱預熱中，預計 30 秒鐘 [Thread:{Thread.CurrentThread.ManagedThreadId}]");
            Thread.Sleep(3000);
            Console.WriteLine($"烤箱預熱 完成");
        }
        static void 製作麵團()
        {
            Console.WriteLine($"製作麵團中，預計 3 秒鐘 [Thread:{Thread.CurrentThread.ManagedThreadId}]");
            Thread.Sleep(300);
            Console.WriteLine($"麵團製作 完成");
        }
        static void 發麵()
        {
            Console.WriteLine($"麵團發麵中，預計 8 秒鐘 [Thread:{Thread.CurrentThread.ManagedThreadId}]");
            Thread.Sleep(800);
            Console.WriteLine($"麵團發麵 完成");
        }
        static void 準備醬料()
        {
            Console.WriteLine($"準備醬料中，預計 2 秒鐘 [Thread:{Thread.CurrentThread.ManagedThreadId}]");
            Thread.Sleep(200);
            Console.WriteLine($"準備醬料 完成");
        }
        static void 準備配料()
        {
            Console.WriteLine($"準備配料中，預計 2 秒鐘 [Thread:{Thread.CurrentThread.ManagedThreadId}]");
            Thread.Sleep(200);
            Console.WriteLine($"準備配料 完成");
        }
        static void 製作披薩餅皮與塗抹醬料和放置配料()
        {
            Console.WriteLine($"製作披薩餅皮與塗抹醬料和放置配料中，預計 3 秒鐘 [Thread:{Thread.CurrentThread.ManagedThreadId}]");
            Thread.Sleep(300);
            Console.WriteLine($"製作披薩餅皮與塗抹醬料和放置配料 完成");
        }
        static void 烤製披薩()
        {
            Console.WriteLine($"烤製披薩中，預計 6 秒鐘 [Thread:{Thread.CurrentThread.ManagedThreadId}]");
            Thread.Sleep(600);
            Console.WriteLine($"烤製披薩 完成");
        }
        static void 準備餐具與飲料()
        {
            Console.WriteLine($"準備餐具與飲料中，預計 2 秒鐘 [Thread:{Thread.CurrentThread.ManagedThreadId}]");
            Thread.Sleep(200);
            Console.WriteLine($"準備餐具與飲料 完成");
        }
        static void 披薩完成_開始食用()
        {
            Console.WriteLine($"披薩完成_開始食用，預計 1 秒鐘 [Thread:{Thread.CurrentThread.ManagedThreadId}]");
            Thread.Sleep(100);
            Console.WriteLine($"披薩完成_開始食用 完成");
        }
    }
}
