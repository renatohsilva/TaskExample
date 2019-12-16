using System;
using System.Threading.Tasks;

namespace TaskExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await FirstMethod();

            await SecondMethod();

            await ThirdMethod();

            await FourthMethod();

            await FifthMethod();
        }


        //Execution Time: 23087 ms
        //Linear Execution - Correct answer
        public static async Task FirstMethod()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            await BuildAnEvilLair();
            await HireSomeHenchman();
            await MakeMiniClone();
            await SuperEvilPlan();
            var money = await MakeOneMillionDollars();
            await WorldDomination(money);
            await Congratulations();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Println($"FirstMethod - Execution Time: {elapsedMs} ms");
        }

        //Execution Time: 6047 ms
        //Parallel Execution - Wrong answer
        public static async Task SecondMethod()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var t1 = BuildAnEvilLair();
            var t2 = HireSomeHenchman();
            var t3 = MakeMiniClone();
            var t4 = SuperEvilPlan();
            var t5 = MakeOneMillionDollars();
            var t6 = WorldDomination();
            var t7 = Congratulations();

            await t1;
            await t2;
            await t3;
            await t4;
            await t5;
            await t6;
            await t7;

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Println($"SecondMethod - Execution Time: {elapsedMs} ms");
        }

        //Execution Time: 19075 ms
        //Better Aproach - Correct answer
        public static async Task ThirdMethod()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            // First we need an Evil Lair
            await BuildAnEvilLair();

            // Next, hire Henchman and create a Clone (asynchronously)
            Task.WaitAll(HireSomeHenchman(), MakeMiniClone());

            // Now, come up with the evil plan
            await SuperEvilPlan();

            // And finally, make One Million Dollars (and achieve World Domination)
            Task.WaitAll(MakeOneMillionDollars(), WorldDomination());

            await Congratulations();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Println($"ThirdMethod - Execution Time: {elapsedMs} ms");
        }

        //Execution Time: 21133 ms
        //Better Aproach with parameter - Correct answer
        public static async Task FourthMethod()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            // First we need an Evil Lair
            await BuildAnEvilLair();

            //// Next, hire Henchman and create a Clone (asynchronously)
            Task.WaitAll(HireSomeHenchman(), MakeMiniClone());

            //// Now, come up with the evil plan
            await SuperEvilPlan();

            // And finally, make One Million Dollars (and achieve World Domination)
            await WorldDominationWithDolars();

            await Congratulations();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Println($"FourthMethod - Execution Time: {elapsedMs} ms");
        }

        //Execution Time: 21083 ms
        //Better Aproach with parameter - Correct answer
        public static async Task FifthMethod()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            // First we need an Evil Lair
            await BuildAnEvilLair();

            //// Next, hire Henchman and create a Clone (asynchronously)
            Task.WaitAll(HireSomeHenchman(), MakeMiniClone());

            //// Now, come up with the evil plan
            await SuperEvilPlan();

            // And finally, make One Million Dollars (and achieve World Domination)
            await AnotherWorldDominationWithDolars();

            await Congratulations();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Println($"FifthMethod - Execution Time: {elapsedMs} ms");
        }

        #region Private Methods

        private static async Task BuildAnEvilLair()
        {
            await Task.Delay(5000);
            Console.WriteLine("01 - Evil Lair Built!");
        }

        private static async Task HireSomeHenchman()
        {
            await Task.Delay(2000);
            Console.WriteLine("02 - Henchman Hired!");
        }

        private static async Task MakeMiniClone()
        {
            await Task.Delay(3000);
            Console.WriteLine("03 - Mini Clone Created!");
        }

        private static async Task SuperEvilPlan()
        {
            await Task.Delay(4000);
            Console.WriteLine("04 - Super Evil Plan Completed!");
        }

        private static async Task<int> MakeOneMillionDollars()
        {
            await Task.Delay(2000);
            Console.WriteLine("05 - Million Dollars Made!");
            return 1000000;
        }

        private static async Task WorldDomination()
        {
            await Task.Delay(6000);
            Console.WriteLine($"06 - World Domination Achieved!");
        }

        private static async Task WorldDomination(int money)
        {
            await Task.Delay(6000);
            Console.WriteLine($"06 - World Domination Achieved with {money} dolars");
        }

        private static async Task WorldDominationWithDolars()
        {
            var task = await MakeOneMillionDollars().ContinueWith(async taskMoney =>
            {
                var money = await taskMoney;
                await WorldDomination(money);
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            await task;
        }

        private static async Task AnotherWorldDominationWithDolars()
        {
            var money = await MakeOneMillionDollars();
            await WorldDomination(money);
        }

        private static async Task Congratulations()
        {
            await Task.Delay(1000);
            Console.WriteLine($"07 - Well done!");
        }

        private static void Println(String message)
        {
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(message);
            Console.WriteLine($"---------------------------------------------------");
            Console.WriteLine(Environment.NewLine);
        }

        #endregion
    }
}
