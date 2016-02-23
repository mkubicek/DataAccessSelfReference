namespace DataAccessSelfReference
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Telerik.OpenAccess;

    public class Program
    {
        private static  List<Code> codeCache;
         
        public static void Main(string[] args)
        {
            Db.connectionStringName = "data source=RITV3;initial catalog=CodeSelfReference;integrated security=True";

            using (var db = new Db())
            {
                var codes = db.GetAll<Code>().Include(x => x.RestrictedFor).Include(x => x.Restricts);

                codeCache = codes.ToList();

                Console.WriteLine("RestrictedFor count: " + codeCache.SelectMany(x => x.RestrictedFor).Count());
                Console.WriteLine("Restricts count: " + codeCache.SelectMany(x => x.Restricts).Count());

                //foreach (var code in codes)
                //{
                //    Console.WriteLine("Code " + code.Name + " has " + code.RestrictedFor.Count + " restrictions and restricts " + code.Restricts.Count + " codes.");
                //}
            }

            //foreach (var code in codeCache.Where(x=>x.RestrictedFor.Any() || x.Restricts.Any()))
            //{
            //    Console.WriteLine("Code " + code.Name + " has " + code.RestrictedFor.Count + " restrictions and restricts " + code.Restricts.Count + " codes.");
            //} 

            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
        }
    }



}
