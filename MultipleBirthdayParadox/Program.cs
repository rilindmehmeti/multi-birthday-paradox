using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleBirthdayParadox
{
    class EllysBirthdays
    {
        //double[] factorial_array = new double[100];
        List<double> fa = new List<double>();
        double dynaimc_factorial(double number)
        {
            if (number >= 0)
            {
                if(fa.Count == 0)
                {
                    fa.Add(1);
                }
                //factorial_array[0] = 1;
                if (fa.Count > number)
                    return fa[(int)number];

                for (int j = fa.Count; j <= number; ++j)
                {
                   fa.Add(fa[j-1] * (double)j);
                }
                /*for (int i = 1; i <= number; ++i)
                {
                  factorial_array[i] = (double)i * factorial_array[i - 1];
                } */
            }
            return fa[(int)number];//factorial_array[(int)number];
        }

        double poisson_formula(double no_of_people, double no_of_birthdays, double possibilities)
        {
            return (Math.Pow(no_of_people / possibilities, no_of_birthdays) / dynaimc_factorial(no_of_birthdays));
        }

        double[] results = new double[100];

        double get_chance_low(double no_of_people, double no_of_birthdays, double possibilites)
        {
            results[0] = poisson_formula(no_of_people, 0, possibilites);
            for (int i = 1; i < no_of_birthdays; i++)
            {
                results[i] = results[i - 1] + poisson_formula(no_of_people, i, possibilites);
            }
            return 1 - ((double)1 / Math.Exp(no_of_people) * Math.Pow(results[(int)no_of_birthdays - 1], possibilites));
        }

        double get_chance(double no_of_people, double no_of_birthdays, double possibilities)
        {

            return 1 - Math.Exp(Math.Pow(no_of_people, no_of_birthdays) / (Math.Exp(no_of_people / possibilities) * Math.Pow(possibilities, no_of_birthdays - 1) * (no_of_people / (possibilities * (no_of_birthdays + 1)) - 1) * dynaimc_factorial(no_of_birthdays)));
        }

        double possible_birthdays = 365;

        public int numFriends(int same_birthday_no)
        {
            double result = 1;
            double final_result = get_chance_low(result, same_birthday_no, possible_birthdays);
            while (final_result < 0.5)
            {
                result++;
                if (result < 600)
                {
                    final_result = get_chance_low(result, same_birthday_no, possible_birthdays);
                }
                else
                {
                    final_result = get_chance(result, same_birthday_no, possible_birthdays);
                }

            }
            return (int)result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            EllysBirthdays obj = new EllysBirthdays();
            double same_birthday_no = 1;
            while (same_birthday_no > 0)
            {
                Console.WriteLine("Give the desierd number of same birthdays:");
                double.TryParse(Console.ReadLine(), out same_birthday_no);
                if(same_birthday_no < 1 || same_birthday_no > 20)
                {
                    Console.WriteLine("Same birthday number "+ same_birthday_no+ " not allowed");
                    break;
                }
                Console.WriteLine("You need " + (int)obj.numFriends((int)same_birthday_no) + " people in a room in order to have " + (int)same_birthday_no + " same birthdays");
            }
            Console.ReadLine();
        }

    }
}
